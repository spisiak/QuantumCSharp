using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class U3 : IQuantumCommand
    {
        private int QubitIndex { get; set; }
        private string Lambda { get; set; }
        private string Phi { get; set; }
        private string Theta { get; set; }

        public U3()
        {

        }

        public U3(int QubitIndex,string Theta, string Phi, string Lambda)
        {
            this.QubitIndex = QubitIndex;
            this.Lambda = Lambda;
            this.Phi = Phi;
            this.Theta = Theta;
        }

        public override string ToString()
        {
            return string.Format("u3({0},{1},{2}) q[{3}];", Theta, Phi, Lambda, QubitIndex);
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
