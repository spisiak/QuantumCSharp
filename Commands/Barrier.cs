using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Commands
{
    public class Barrier : IQuantumCommand
    {
        private int StartIndex { get; set; }
        private int EndIndex { get; set; }

        public Barrier()
        {

        }

        public Barrier(Qubit StartQubit)
        {
            this.StartIndex = this.EndIndex = StartQubit.QubitIndex;
        }

        public Barrier(Qubit StartQubit, Qubit EndQubit)
        {
            this.StartIndex = StartQubit.QubitIndex;
            this.EndIndex = EndQubit.QubitIndex;
        }

        public override string ToString()
        {
            if(this.StartIndex == this.EndIndex)
                return string.Format("barries q[{0}];", this.StartIndex);
            else
            {
                string tmp_barrier = "barries ";
                for(int i = this.StartIndex;i < this.EndIndex;i++)
                {
                    tmp_barrier += string.Format("q[{0}],", i);
                }
                tmp_barrier += string.Format("q[{0}];", this.EndIndex);
                return tmp_barrier;
            }
            
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
