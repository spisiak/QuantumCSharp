using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class U2 : IQuantumCommand
    {
        private int QubitIndex { get; set; }
        private string Lambda { get; set; }
        private string Phi { get; set; }

        public U2()
        {

        }

        public U2(int QubitIndex,string Phi, string Lambda)
        {
            this.QubitIndex = QubitIndex;
            this.Lambda = Lambda;
            this.Phi = Phi;
        }

        public override string ToString()
        {
            return string.Format("u2({0},{1}) q[{2}];", Phi,Lambda, QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
