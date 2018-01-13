using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public interface IQuantumCommand
    {
        string ToString();
        QuantumCommand ParseCommand(string raw_command);
    }
}
