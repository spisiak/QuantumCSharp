using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class PauliY : IQuantumCommand
    {
        private int QubitIndex { get; set; }

        public PauliY()
        {

        }

        public PauliY(int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
        }

        public override string ToString()
        {
            return string.Format("y q[{0}];", QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
