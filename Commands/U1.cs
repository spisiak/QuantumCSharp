using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class U1 : IQuantumCommand
    {
        private int QubitIndex { get; set; }
        private string Lambda { get; set; }

        public U1()
        {

        }

        public U1(int QubitIndex,string Lambda)
        {
            this.QubitIndex = QubitIndex;
            this.Lambda = Lambda;
        }

        public override string ToString()
        {
            return string.Format("u1({0}) q[{1}];", Lambda, QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
