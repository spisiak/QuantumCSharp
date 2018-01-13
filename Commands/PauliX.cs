using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class PauliX : IQuantumCommand
    {
        private int QubitIndex { get; set; }

        public PauliX()
        {

        }

        public PauliX(int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
        }

        public override string ToString()
        {
            return string.Format("x q[{0}];", QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
