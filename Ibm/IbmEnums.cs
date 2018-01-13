using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Ibm
{
    public enum IbmQXSendState
    {
        Device_is_offline,
        Exception,
        CommunicationError,
        AuthentificationFailed,
        MeasurmentTimeOut,
        IbmError,
        Ok
    }

    public enum IbmQXDevices
    {
        Simulator,
        ibmqx2,
        ibmqx4,
        ibmqx5,
        QS1_1
    }

    public enum IbmQXSimulator
    {
        ibmqx_qasm_simulator,
        ibmqx_hpc_qasm_simulator
    }

}
