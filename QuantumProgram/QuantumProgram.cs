using QuantumCSharp.Commands;
using QuantumCSharp.Ibm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuantumCSharp
{
    public class QuantumProgramOption
    {
        public IbmQXDevices Device { get; set; }
        public int Shots { get; set; } = 1024;
        public bool Cache { get; set; } = false;
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaxCredits { get; set; } = 5;
    }


    public class QuantumProgram
    {
        private IbmQX IbmComputer;
        private List<Qubit> Qubits;
        public List<IQuantumCommand> Commands { get; set; }

        public string AccessToken { get; set; }

        public QuantumProgramOption Options { get; set; }

        public QuantumProgram(QuantumProgramOption options)
        {
            IbmComputer = new IbmQX();
            Qubits = new List<Qubit>();
            Options = options;
            this.Commands = new List<IQuantumCommand>();
        }

        public void QubitRegistration(Qubit _qubit)
        {
            Qubits.Add(_qubit);
        }

        public void Clear()
        {
            Qubits.Clear();
        }

        public IbmQX_SendResult Execute()
        {
            if (Qubits.Count == 0 || Commands.Count < 1)
                return null;
            string qasm_code = "";
            int max_qubit_index = 0;
            foreach (var current_qubit in Qubits)
            {
                if (max_qubit_index < current_qubit.QubitIndex)
                    max_qubit_index = current_qubit.QubitIndex;
            }
            foreach (var command in Commands)
            {
                qasm_code += command.ToString();
            }
            if (qasm_code != "")
            {
                max_qubit_index++;
                qasm_code = string.Format("include \"qelib1.inc\";qreg q[{0}];creg c[{1}];", max_qubit_index, max_qubit_index) + qasm_code;
                foreach (var current_qubit in Qubits)
                    qasm_code += string.Format("measure q[{0}]->c[{0}];", current_qubit.QubitIndex);
            }
            var result = IbmComputer.Send(new IbmQXSendInput()
            {
                Email = Options.Email,
                Password = Options.Password,
                Cache = Options.Cache,
                Device = Options.Device,
                MaxCredits = Options.MaxCredits,
                QasmCode = qasm_code,
                Shots = Options.Shots
            });
            result.Wait();
            return result.Result;
        }

        public List<QubitValueResult> GetQubitResultStates(IbmQX_SendResult qubitValueResult)
        {
            return IbmComputer.GetQubitResultStates(qubitValueResult.JobMeasurmentResult, Qubits);
        }


        public void AddLog(IbmLogDelegate function)
        {
            if (IbmComputer != null)
                IbmComputer.AddLog += function;
        }
    }
}
