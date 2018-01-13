using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class T_PhaseGateT : IQuantumCommand
    {
        private int QubitIndex { get; set; }

        public T_PhaseGateT()
        {

        }

        public T_PhaseGateT(int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
        }

        public override string ToString()
        {
            return string.Format("tdg q[{0}];", QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
