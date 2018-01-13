using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QuantumCSharp.Ibm
{

    public delegate void IbmLogDelegate(string message);

    public sealed class IbmQX 
    {
        public static string ApiUrl = "https://quantumexperience.ng.bluemix.net/api/";
        public static string ApiIsDeviceOnline = "Backends/{0}/queue/status";
        public static string ApiJobs = "Jobs?access_token={0}&deviceRunType={1}&fromCache={2}&shots={3}";
        public static string ApiUserLogin = "users/login";
        public static string ApiJobResult = "Jobs/{0}?access_token={1}";
        public static string ApiHistoryJobs = "Jobs/?access_token={0}";

        private IbmQx_UserLogin AuthenticationData;
        private Storage _storage;

        public event IbmLogDelegate AddLog;

        public IbmQX()
        {
            _storage = new Storage();
            AuthenticationData = _storage.LoadAuthenticationData();
        }

        public async Task<bool> Login(string UserName,string Password)
        {
            var client = new HttpClient();
            var PostDataString = "email=" + UserName + "&password=" + Password;
            var response = (await client.PostAsync(ApiUrl+ ApiUserLogin, new StringContent(PostDataString, Encoding.UTF8, "application/x-www-form-urlencoded")));
            using (HttpContent content = response.Content)
            {
                var serializer = new DataContractJsonSerializer(typeof(IbmQx_UserLogin));
                var data = serializer.ReadObject(await content.ReadAsStreamAsync()) as IbmQx_UserLogin;
                if(data != null)
                {
                    AuthenticationData = data;
                    data.TokenEndTime = DateTime.Now.AddSeconds(data.Ttl-10);
                    return _storage.SaveAuthenticationData(data);
                }
                return false;
            }
        }

        public async Task<bool> IsDeviceOnline(IbmQXDevices Device)
        {
            var client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(DeviceOnlineState));

            var streamTask = client.GetStreamAsync(string.Format(ApiUrl+ ApiIsDeviceOnline,Device.ToString()));
            var respense = serializer.ReadObject(await streamTask) as DeviceOnlineState;
            if (respense == null)
                return false;
            else if (respense.State == true)
                return true;
            else
            {
                return false;
            }
        }

        public void GetJobResult(IbmQX_SendResult job_result, IbmQXSendInput options)
        {
            try
            {
                if (job_result.JobResult.Qasms.Count < 1)
                    return;
                if(job_result.JobResult.InfoQueue != null)
                {
                    Task.Delay(job_result.JobResult.InfoQueue.EstimatedTimeInQueue * 1000).Wait();
                }
                var client = new HttpClient();
                string uri = string.Format(ApiUrl + ApiJobResult, job_result.JobResult.Id, AuthenticationData.Id);
                for(int i = 0;i < options.NumRetries;i++)
                {
                    using (var streamTask = client.GetStreamAsync(uri))
                    {
                        DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                        settings.UseSimpleDictionaryFormat = true;
                        var serializer = new DataContractJsonSerializer(typeof(IbmJobMeasurmentResult), settings);
                        var data = serializer.ReadObject(streamTask.Result) as IbmJobMeasurmentResult;
                        if (data != null && data.Status.ToUpper() == "COMPLETED" && data.Qasms.Count > 0 && data.Qasms[0].Status.ToUpper() == "DONE")
                        {
                            
                            job_result.JobMeasurmentResult = data;
                            job_result.Results = data.Qasms[0].result.Data.Counts;
                            job_result.State = IbmQXSendState.Ok;
                            return;
                        }
                    }

                    Task.Delay(options.SecInterval*1000).Wait();
                }
                job_result.State = IbmQXSendState.MeasurmentTimeOut;
            }
            catch(Exception en)
            {
                job_result.ErrorMessage = en.Message;
                job_result.State = IbmQXSendState.Exception;
            }
        }

        public async Task<List<IbmJobMeasurmentResult>> GetHistoryResults(IbmQXSendInput options)
        {
            try
            {
                if (AuthenticationData.TokenEndTime <= DateTime.Now)
                {
                    CallLogEvent("Getting a new authentificaton token");
                    bool response_state = await Login(options.Email, options.Password);
                    if (response_state == false)
                    {
                        CallLogEvent("Process of getting a new authentication token failed");
                        return new List<IbmJobMeasurmentResult>();
                    }
                }
                var client = new HttpClient();
                string uri = string.Format(ApiUrl + ApiHistoryJobs, AuthenticationData.Id);
                using (var streamTask = client.GetStreamAsync(uri))
                {
                    DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                    settings.UseSimpleDictionaryFormat = true;
                    var serializer = new DataContractJsonSerializer(typeof(List<IbmJobMeasurmentResult>), settings);
                    var data = serializer.ReadObject(streamTask.Result) as List<IbmJobMeasurmentResult>;
                    if (data == null)
                    {
                        return new List<IbmJobMeasurmentResult>();
                    }
                    else
                        return data;
                }
            }
            catch (Exception en)
            {
                return new List<IbmJobMeasurmentResult>();
            }
        }

        public async Task<IbmQX_SendResult> Send(IbmQXSendInput Options)
        {
            IbmQX_SendResult result = new IbmQX_SendResult();
            try
            {
                CallLogEvent("Ibm send function has started ");
                if (Options.Device != IbmQXDevices.Simulator && await IsDeviceOnline(Options.Device) == false)
                {
                    CallLogEvent("Target device is currenlty offline");
                    result.State = IbmQXSendState.Device_is_offline;
                    return result;
                }
                if(AuthenticationData.TokenEndTime <= DateTime.Now)
                {
                    CallLogEvent("Getting a new authentificaton token");
                    bool response_state = await Login(Options.Email, Options.Password);
                    if(response_state == false)
                    {
                        CallLogEvent("Process of getting a new authentication token failed");
                        result.State = IbmQXSendState.AuthentificationFailed;
                        return result;
                    }
                }
                var client = new HttpClient();
                string uri = string.Format(ApiUrl + ApiJobs, AuthenticationData.Id, Options.Device.ToString(), Options.Cache.ToString().ToLower(), Options.Shots);
                var PostDataString = "{ \"qasms\": [{ \"qasm\":\"" + Options.QasmCode.Replace("\"", "\\\"") + "\"}], \"shots\":" + Options.Shots + ", \"maxCredits\":" + Options.MaxCredits + ", \"backend\":{ \"name\": \""+ Options.Device.ToString().ToLower()+ "\" }}";
                var response = (await client.PostAsync(uri, new StringContent(PostDataString, Encoding.UTF8, "application/json")));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(IbmJobResult));
                        result.JobResult = serializer.ReadObject(await content.ReadAsStreamAsync()) as IbmJobResult;
                        if (result.JobResult == null)
                        {
                            result.State = IbmQXSendState.CommunicationError;
                            return result;
                        }
                        CallLogEvent("Program is executing");
                        if(result.JobResult.InfoQueue != null)
                        {
                            CallLogEvent("Queue position: "+ result.JobResult.InfoQueue.Position + ", Estimated time in queue: "+ result.JobResult.InfoQueue.EstimatedTimeInQueue +"s");
                            CallLogEvent("Estimated finish time: " + DateTime.Now.AddSeconds(result.JobResult.InfoQueue.EstimatedTimeInQueue).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                }
                else
                {
                    result.State = IbmQXSendState.IbmError;
                    using (HttpContent content = response.Content)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(IbmErrorMessage));
                        var error_msg = serializer.ReadObject(await content.ReadAsStreamAsync()) as IbmErrorMessage;
                        if (error_msg != null && error_msg.Error != null)
                        {
                            result.ErrorMessage = error_msg.Error.Code + ":" + error_msg.Error.Message;
                            CallLogEvent("Ibm send function has finished with error:" + result.ErrorMessage);
                            return result;
                        }
                    }
                    result.ErrorMessage = "Unknow error";
                    CallLogEvent("Ibm send function has finished with error:" + result.ErrorMessage);
                }
                GetJobResult(result, Options);
                CallLogEvent("Ibm send function has finished succesfuly ");
                return result;
            }
            catch(Exception en)
            {
                CallLogEvent("Ibm send function threw exception, message:" + en.Message);
                result.State = IbmQXSendState.Exception;
                result.ErrorMessage = en.Message;
                return result;
            }
        }

        private void CallLogEvent(string message)
        {
            AddLog?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", "+message);
        }

        public List<QubitValueResult> GetQubitResultStates(IbmJobMeasurmentResult qubitValueResult, List<Qubit> UsedQubits)
        {
            if (qubitValueResult.Qasms.Count < 1 && qubitValueResult.Qasms[0].result.Data.Counts.Count < 1)
                return new List<QubitValueResult>();
            List<QubitValueResult> retults = new List<QubitValueResult>(qubitValueResult.Qasms[0].result.Data.Counts.Count);
            for (int i = 0; i < UsedQubits.Count; i++)
            {
                QubitValueResult current_qubit = new QubitValueResult();
                current_qubit.Index = UsedQubits[i].QubitIndex;
                current_qubit.One = current_qubit.Zero = 0;
                foreach (var item in qubitValueResult.Qasms[0].result.Data.Counts)
                {
                    string inv_key = StringReverse(item.Key);
                    if (inv_key[current_qubit.Index] == '0')
                        current_qubit.Zero += item.Value;
                    if (inv_key[current_qubit.Index] == '1')
                        current_qubit.One += item.Value;
                }
                retults.Add(current_qubit);
            }
            return retults;
        }

        public static string StringReverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
