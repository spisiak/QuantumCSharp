using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QuantumCSharp.Commands
{
    public class Measurment : IQuantumCommand
    {
        public Measurment()
        {
        }

        public override string ToString()
        {
            return "";
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            Match match = Regex.Match(raw_command, @"(measure)\s[a-z]+\[(\d+)\](->)[a-z]+\[(\d+)\]");
            if(match.Success && match.Groups.Count == 5)
            {
                QuantumCommand result = new QuantumCommand();
                result.CommandType = this;
                result.Args = new List<string>();
                result.Args.Add(match.Groups[2].Value);
                result.Args.Add(match.Groups[4].Value);
                return result;
            }
            return null;
        }
    }
}
