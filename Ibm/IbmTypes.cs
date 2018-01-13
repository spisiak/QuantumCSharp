using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QuantumCSharp.Ibm
{
    public class IbmQX_SendResult
    {
        public IbmQXSendState State { get; set; }
        public string ErrorMessage { get; set; }
        public IbmJobResult JobResult { get; set; }
        public IbmJobMeasurmentResult JobMeasurmentResult { get; set; }
        public Dictionary<string,int> Results { get; set; }

    }

    [DataContract]
    public class IbmQx_UserLogin
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "ttl")]
        public int Ttl { get; set; }
        [DataMember(Name = "created")]
        public string Created { get; set; }
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "TokenEndTime")]
        public DateTime TokenEndTime { get; set; } = DateTime.MinValue;
    }

    public class IbmQXSendInput
    {
        public IbmQXDevices Device { get; set; }
        public int Shots { get; set; }
        public bool Cache { get; set; } = false;
        public string QasmCode { get; set; } = "";
        public int MaxCredits { get; set; } = 5;
        public string Email { get; set; }
        public string Password { get; set; }
        public int NumRetries { get; set; } = 300;
        public int SecInterval { get; set; } = 1;
    }

    [DataContract]
    public class DeviceOnlineState
    {
        [DataMember(Name = "state")]
        public bool State { get; set; }
        [DataMember(Name = "busy")]
        public bool Busy { get; set; }
        [DataMember(Name = "lengthQueue")]
        public int LengthQueue { get; set; }
    }

    public class IbmQXSendResult
    {
        public IbmQXSendState State { get; set; }
    }

    [DataContract]
    public class IbmJobResultBackend
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
    [DataContract]
    public class IbmJobResultQasms
    {
        [DataMember(Name = "qasm")]
        public string Qasm { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "executionId")]
        public string ExecutionId { get; set; }
    }

    [DataContract]
    public class IbmJobResultQueue
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "position")]
        public int Position { get; set; }
        [DataMember(Name = "estimatedTimeInQueue")]
        public int EstimatedTimeInQueue { get; set; }
    }

    [DataContract]
    public class IbmJobResult
    {
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }
        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }
        [DataMember(Name = "usedCredits")]
        public int UsedCredits { get; set; }
        [DataMember(Name = "maxCredits")]
        public int MaxCredits { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "shots")]
        public int Shots { get; set; }
        [DataMember(Name = "backend")]
        public IbmJobResultBackend Backend { get; set; }
        [DataMember(Name = "qasms")]
        public List<IbmJobResultQasms> Qasms { get; set; }
        [DataMember(Name = "infoQueue")]
        public IbmJobResultQueue InfoQueue { get; set; }
    }

    [DataContract]
    public class MeasurmenDatetResult
    {
        [DataMember(Name = "time")]
        public decimal time { get; set; }
        [DataMember(Name = "counts")]
        public Dictionary<string,int> Counts { get; set; }
    }

    [DataContract]
    public class MeasurmentResult
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }
        [DataMember(Name = "data")]
        public MeasurmenDatetResult Data { get; set; }
    }

    [DataContract]
    public class IbmJobMeasurmentQasmsResult
    {
        [DataMember(Name = "qasm")]
        public string Qasm { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "executionId")]
        public string ExecutionId { get; set; }
        [DataMember(Name = "result")]
        public MeasurmentResult result { get; set; }
    }

    [DataContract]
    public class IbmJobMeasurmentResult
    {
        [DataMember(Name = "shots")]
        public int Shots { get; set; }
        [DataMember(Name = "backend")]
        public IbmJobResultBackend Backend { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "usedCredits")]
        public int UsedCredits { get; set; }
        [DataMember(Name = "maxCredits")]
        public int MaxCredits { get; set; }
        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }
        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "qasms")]
        public List<IbmJobMeasurmentQasmsResult> Qasms { get; set; }
    }



    [DataContract]
    public class IbmErrorCodeMessage
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }

    [DataContract]
    public class IbmErrorMessage
    {
        [DataMember(Name = "error")]
        public IbmErrorCodeMessage Error { get; set; }
    }

    public class QubitValueResult
    {
        public int Index { get; set; }
        public int Zero { get; set; }
        public int One { get; set; }
    }
}
