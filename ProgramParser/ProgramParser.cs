using QuantumCSharp.Commands;
using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp
{
    public class ProgramParser
    {
        private List<IQuantumCommand> RegistredQuantumCommands;

        public ProgramParser()
        {
            RegistredQuantumCommands = new List<IQuantumCommand>();
            RegistredQuantumCommands.Add(new Measurment());
            RegistredQuantumCommands.Add(new Barrier());
            RegistredQuantumCommands.Add(new ControlledNOT());
            RegistredQuantumCommands.Add(new Idle());
            RegistredQuantumCommands.Add(new Hadamard());
            RegistredQuantumCommands.Add(new PauliX());
            RegistredQuantumCommands.Add(new PauliY());
            RegistredQuantumCommands.Add(new PauliZ());
            RegistredQuantumCommands.Add(new S_PhaseGate());
            RegistredQuantumCommands.Add(new S_PhaseGateT());
            RegistredQuantumCommands.Add(new T_PhaseGate());
            RegistredQuantumCommands.Add(new T_PhaseGateT());
            RegistredQuantumCommands.Add(new U1());
            RegistredQuantumCommands.Add(new U2());
            RegistredQuantumCommands.Add(new U3());
        }


        public string[] GetCommandList(string ProgramCode)
        {
            return ProgramCode.Split(';');
        }

        private QuantumCommand GetCommandType(string command)
        {
            foreach(var cmd in RegistredQuantumCommands)
            {
                var parsed_cmd = cmd.ParseCommand(command);
                if (parsed_cmd != null)
                    return parsed_cmd;
            }
            return null;
        }

        public List<QuantumCommand> GetSyntaxList(string[] commands)
        {
            List<QuantumCommand> results = new List<QuantumCommand>();
            foreach(var cmd in commands)
            {
                var parsed_cmd = GetCommandType(cmd);
                if (parsed_cmd != null)
                    results.Add(parsed_cmd);
            }
            return results;
        }
    }
}
