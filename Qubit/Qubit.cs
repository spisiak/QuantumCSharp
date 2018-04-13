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

        /// <summary>Pauli gate: bit-flip
        /// <para></para>
        /// </summary>
        public void X()
        {
            if (Program != null)
                Program.Commands.Add(new PauliX(QubitIndex));
        }

        /// <summary>Pauli gate: bit and phase flip
        /// <para></para>
        /// </summary>
        public void Y()
        {
            if (Program != null)
                Program.Commands.Add(new PauliY(QubitIndex));
        }

        /// <summary>Pauli gate: phase flip
        /// <para></para>
        /// </summary>
        public void Z()
        {
            if (Program != null)
                Program.Commands.Add(new PauliZ(QubitIndex));
        }

        /// <summary>Clifford gate: Hadamard
        /// <para></para>
        /// </summary>
        public void H()
        {
            if (Program != null)
                Program.Commands.Add(new Hadamard(QubitIndex));
        }

        /// <summary>Barrier operation
        /// <para>The operation denies the code optiomalization in the specific part of the code.</para>
        /// </summary>
        public void Barrier()
        {
            this.
            if (Program != null)
                Program.Commands.Add(new Barrier(this));
        }

        /// <summary>Controlled-NOT
        /// <para>THe first parameter is a control qubit</para>
        /// </summary>
        public void CNOT(Qubit ControlQubit)
        {
            if (Program != null)
                Program.Commands.Add(new ControlledNOT(this.QubitIndex, ControlQubit.QubitIndex,Program.Options.Device));
        }

        /// <summary>Clifford gate: sqrt(Z) phase gate
        /// <para></para>
        /// </summary>
        public void S()
        {
            if (Program != null)
                Program.Commands.Add(new S_PhaseGate(QubitIndex));
        }

        /// <summary>Clifford gate: conjugate of sqrt(Z)
        /// <para></para>
        /// </summary>
        public void Sdg()
        {
            if (Program != null)
                Program.Commands.Add(new S_PhaseGateT(QubitIndex));
        }

        /// <summary>C3 gate: sqrt(S) phase gate
        /// <para></para>
        /// </summary>
        public void T()
        {
            if (Program != null)
                Program.Commands.Add(new T_PhaseGate(QubitIndex));
        }

        /// <summary>C3 gate: conjugate of sqrt(S)
        /// <para></para>
        /// </summary>
        public void Tdg()
        {
            if (Program != null)
                Program.Commands.Add(new T_PhaseGateT(QubitIndex));
        }

        /// <summary>1-parameter 0-pulse single qubit gate
        /// <para>1th parameter is a lambda value. The value range is [0,4pi)</para>
        /// </summary>
        public void U1(string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U1(QubitIndex,Lambda));
        }

        /// <summary>2-parameter 1-pulse single qubit gate
        /// <para>1th parameter is a phi value. The value range is [0,4pi)</para>
        /// <para>2th parameter is a lambda value. The value range is [0,4pi)</para>
        /// </summary>
        public void U2(string Phi,string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U2(QubitIndex,Phi,Lambda));
        }

        /// <summary>3-parameter 2-pulse single qubit gate
        /// <para>1th parameter is a theta value. The value range is [0,4pi)</para>
        /// <para>2th parameter is a phi value. The value range is [0,4pi)</para>
        /// <para>3th parameter is a lambda value. The value range is [0,4pi)</para>
        /// </summary>
        public void U3(string Theta, string Phi, string Lambda)
        {
            if (Program != null)
                Program.Commands.Add(new U3(QubitIndex, Theta, Phi, Lambda));
        }

        /// <summary>Idle gate (identity)
        /// <para></para>
        /// </summary>
        public void Idle()
        {
            if (Program != null)
                Program.Commands.Add(new Idle(QubitIndex));
        }
    }
}
