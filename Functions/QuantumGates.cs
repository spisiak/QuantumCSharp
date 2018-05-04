using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Functions
{
    public class QuantumGates<T> where T : IQubit
    {
        public static readonly double Pi = 3.141592653511;

        public void ControlledZ(T ControlQubit,T TargetQubit)
        {
            TargetQubit.H();
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.H();
        }

        public void ControlledY(T ControlQubit, T TargetQubit)
        {
            TargetQubit.Sdg();
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.S();
        }

        public void ContolledH(T ControlQubit, T TargetQubit)
        {
            TargetQubit.H();
            TargetQubit.Sdg();
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.H();
            TargetQubit.T();
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.T();
            TargetQubit.H();
            TargetQubit.S();
            TargetQubit.X();
            ControlQubit.S();
        }

        public void Toffoli(T ControlQubit1,T ControlQubit2, T TargetQubit)
        {
            TargetQubit.H();
            TargetQubit.CNOT(ControlQubit2);
            TargetQubit.Tdg();
            TargetQubit.CNOT(ControlQubit1);
            TargetQubit.T();
            TargetQubit.CNOT(ControlQubit2);
            TargetQubit.Tdg();
            TargetQubit.CNOT(ControlQubit1);
            ControlQubit2.T();
            TargetQubit.T();
            TargetQubit.H();
            ControlQubit2.CNOT(ControlQubit1);
            ControlQubit1.T();
            ControlQubit2.Tdg();
            ControlQubit2.CNOT(ControlQubit1);
        }

        public void ControlledZRotation(T ControlQubit, T TargetQubit,double Lambda)
        {
            TargetQubit.U1((Lambda / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.U1((-Lambda / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
        }

        public void ControlledYRotation(T ControlQubit, T TargetQubit, double Lambda)
        {
            ControlQubit.U1((Lambda / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.U1((-Lambda / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.U1((Lambda / 2).ToString());
        }

        public void ControlledU3(T ControlQubit, T TargetQubit,double Theta, double Phi, double Lambda)
        {
            TargetQubit.U1((Lambda / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.U3((-Theta / 2).ToString(), "0", (-(Phi + Lambda) / 2).ToString());
            TargetQubit.CNOT(ControlQubit);
            TargetQubit.U3((Theta / 2).ToString(),Phi.ToString(), "0");
        }
    }
}
