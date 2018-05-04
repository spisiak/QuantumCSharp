namespace QuantumCSharp
{
    public interface IQubit
    {
        int QubitIndex { get; set; }
        void Barrier();
        void CNOT(IQubit ControlQubit);
        void H();
        void Idle();
        void S();
        void Sdg();
        void T();
        void Tdg();
        void U1(string Lambda);
        void U2(string Phi, string Lambda);
        void U3(string Theta, string Phi, string Lambda);
        void X();
        void Y();
        void Z();
    }
}