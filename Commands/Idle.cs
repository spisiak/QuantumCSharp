using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class Idle : IQuantumCommand
    {
        private int QubitIndex { get; set; }

        public Idle()
        {

        }

        public Idle(int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
        }

        public override string ToString()
        {
            return string.Format("id q[{0}];", QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
