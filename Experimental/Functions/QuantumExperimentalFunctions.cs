using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCSharp.Experimental.Functions
{
    public class QuantumExperimentalFunctions<T> where T : IQubit
    {
        public void if_else_statement(T ControlQubit, double ConditionRotation, T IfQubit, T ElseQubit)
        {
            IfQubit.U3((-ConditionRotation).ToString(), "0", "0");
            IfQubit.CNOT(ControlQubit);
            IfQubit.U3((ConditionRotation).ToString(), "0", "0");
            ControlQubit.U3("-pi","0","0");
            ControlQubit.CNOT(IfQubit);
            ControlQubit.U3("pi", "0", "0");
            ElseQubit.CNOT(ControlQubit);
            ControlQubit.U3("-pi", "0", "0");
            ControlQubit.CNOT(IfQubit);
            ControlQubit.U3("pi", "0", "0");
            ControlQubit.CNOT(IfQubit);
        }

        public void if_else_statement(T ControlQubit, T IfQubit, T ElseQubit)
        {
            IfQubit.CNOT(ControlQubit);
            ElseQubit.X();
            ElseQubit.CNOT(IfQubit);
        }
    }
}
