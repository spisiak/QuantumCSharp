using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class Hadamard : IQuantumCommand
    {
        private int QubitIndex { get; set; }

        public Hadamard()
        {

        }

        public Hadamard(int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
        }

        public override string ToString()
        {
            return string.Format("h q[{0}];", QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
