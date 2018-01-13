using QuantumCSharp.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp
{
    public class Qubit
    {
        private QuantumProgram Program;
        
        public int QubitIndex { get; private set; }

        public Qubit(QuantumProgram program,int QubitIndex)
        {
            this.QubitIndex = QubitIndex;
            this.Program = program;
            if(Program != null)
             this.Program.QubitRegistration(this);
            
        }

        public void X()
        {
            if (Program != null)
                Program.Commands.Add(new PauliX(QubitIndex));
        }

        public void Y()
        {
            if (Program != null)
                Program.Commands.Add(new PauliY(QubitIndex));
        }

        public void Z()
        {
            if (Program != null)
                Program.Commands.Add(new PauliZ(QubitIndex));
        }

        public void H()
        {
            if (Program != null)
                Program.Commands.Add(new Hadamard(QubitIndex));
        }

        public void Barrier()
        {
            if (Program != null)
                Program.Commands.Add(new Barrier(this));
        }

        public void CNOT(Qubit ControlQubit)
        {
            if (Program != null)
                Program.Commands.Add(new ControlledNOT(this.QubitIndex, ControlQubit.QubitIndex,Program.Options.Device));
        }

        public void S()
        {
            if (Program != null)
                Program.Commands.Add(new S_PhaseGate(QubitIndex));
        }
        public void Sdg()
        {
            if (Program != null)
                Program.Commands.Add(new S_PhaseGateT(QubitIndex));
        }
        public void T()
        {
            if (Program != null)
                Program.Commands.Add(new T_PhaseGate(QubitIndex));
        }
        public void Tdg()
        {
            if (Program != null)
                Program.Commands.Add(new T_PhaseGateT(QubitIndex));
        }

        public void U1(string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U1(QubitIndex,Lambda));
        }
        public void U2(string Phi,string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U2(QubitIndex,Phi,Lambda));
        }
        public void U3(string Theta, string Phi, string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U3(QubitIndex, Theta, Phi, Lambda));
        }
        public void Idle()
        {
            if (Program != null)
                Program.Commands.Add(new Idle(QubitIndex));
        }
    }
}
