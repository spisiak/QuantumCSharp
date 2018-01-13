using QuantumCSharp.Commands;
using QuantumCSharp.Ibm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumCSharp.HistoryResult
{
    public class HistoryResult
    {
        private IbmQX IbmComputer;
        public QuantumProgramOption Options { get; set; }

        public HistoryResult(QuantumProgramOption options)
        {
            IbmComputer = new IbmQX();
            Options = options;
        }

        public async Task<List<IbmJobMeasurmentResult>> GetHistoryResults()
        {
            return await IbmComputer.GetHistoryResults(new IbmQXSendInput()
            {
                Email = Options.Email,
                Password = Options.Password,
                Cache = Options.Cache,
                Device = Options.Device,
                MaxCredits = Options.MaxCredits,
                QasmCode = "",
                Shots = Options.Shots
            });
        }

        public List<QubitValueResult> GetQubitResultStates(IbmJobMeasurmentResult qubitValueResult)
        {
            if (qubitValueResult.Qasms.Count == 0)
                return new List<QubitValueResult>();
            ProgramParser _parser = new ProgramParser();
            var commands = _parser.GetSyntaxList( _parser.GetCommandList(qubitValueResult.Qasms[0].Qasm));
            List<Qubit> Qubits = new List<Qubit>();
            foreach (var cmd in commands)
            {
                Measurment casted_cmd = cmd.CommandType as Measurment;
                if(casted_cmd != null)
                {
                    int qubit_index;
                    if(int.TryParse(cmd.Args[1],out qubit_index))
                    {
                        Qubits.Add(new Qubit(null, qubit_index));
                    }
                    
                }
            }
            return IbmComputer.GetQubitResultStates(qubitValueResult, Qubits);
        }


        public List<QubitValueResult> GetQubitResultStatesById(List<IbmJobMeasurmentResult> qubitValueResults,string ResultId)
        {
            var qubitValueResult = (from ms in qubitValueResults where ms.Id == ResultId select ms).FirstOrDefault();
            if (qubitValueResult == null)
                return new List<QubitValueResult>();
            if (qubitValueResult.Qasms.Count == 0)
                return new List<QubitValueResult>();
            ProgramParser _parser = new ProgramParser();
            var commands = _parser.GetSyntaxList(_parser.GetCommandList(qubitValueResult.Qasms[0].Qasm));
            List<Qubit> Qubits = new List<Qubit>();
            foreach (var cmd in commands)
            {
                Measurment casted_cmd = cmd.CommandType as Measurment;
                if (casted_cmd != null)
                {
                    int qubit_index;
                    if (int.TryParse(cmd.Args[1], out qubit_index))
                    {
                        Qubits.Add(new Qubit(null, qubit_index));
                    }

                }
            }
            return IbmComputer.GetQubitResultStates(qubitValueResult, Qubits);
        }

    }
}
