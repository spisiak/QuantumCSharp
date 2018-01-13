using QuantumCSharp.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.ProgramTypes
{


    public class QuantumCommand
    {
        public IQuantumCommand CommandType { get; set; }
        public List<string> Args { get; set; }
    }
}
