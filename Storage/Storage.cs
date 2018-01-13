using QuantumCSharp.Ibm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace QuantumCSharp
{
    class Storage
    {
        public bool SaveAuthenticationData(IbmQx_UserLogin data)
        {
            try
            {
                using (var writer = new FileStream(@"auth.data", FileMode.Create))
                {
                    var ser = new DataContractSerializer(typeof(IbmQx_UserLogin));
                    ser.WriteObject(writer, data);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public IbmQx_UserLogin LoadAuthenticationData()
        {
            try
            {
                using (var reader = new FileStream(@"auth.data", FileMode.Open))
                {
                    var ser = new DataContractSerializer(typeof(IbmQx_UserLogin));
                    IbmQx_UserLogin new_object = ser.ReadObject(reader) as IbmQx_UserLogin;
                    if (new_object == null)
                        return new IbmQx_UserLogin();
                    else
                        return new_object;
                }
            }
            catch (Exception)
            {
                return new IbmQx_UserLogin();
            }
        }
    }
}
