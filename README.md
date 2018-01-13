# QuantumCSharp

Example program:
```csharp
using QuantumCSharp;
using QuantumCSharp.Ibm;
using System;

namespace QuantumCSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            QuantumProgram program = new QuantumProgram(new QuantumProgramOption()
            {
                Email = "", // put your email
                Password = "", // put your password
                Device = IbmQXDevices.ibmqx4,
            });
            program.AddLog(PrintLog);
            Qubit q1 = new Qubit(program, 0);
            Qubit q2 = new Qubit(program, 1);

            q2.H();
            q1.CNOT(q2);

            Console.WriteLine("Getting resutls...");
            var resutls = program.Execute();
            if (resutls.State == IbmQXSendState.Ok)
            {
                foreach (var item in resutls.Results)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }

            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }

        public static void PrintLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}
```
