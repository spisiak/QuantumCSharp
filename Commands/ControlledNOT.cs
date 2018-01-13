using QuantumCSharp.Ibm;
using QuantumCSharp.ProgramTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCSharp.Commands
{

    public class ControlledNOT : IQuantumCommand
    {
        private class EntagmentPair
        {
            public int TargetIndex { get; set; }
            public int ControlIndex { get; set; }

            public EntagmentPair(int ControlIndex,int TargetIndex)
            {
                this.TargetIndex = TargetIndex;
                this.ControlIndex = ControlIndex;
            }
        }

        private int ControlIndex;
        private int TargetIndex;

        private EntagmentPair[] Ibmqx2 = { new EntagmentPair(0,1), new EntagmentPair(0, 2), new EntagmentPair(1, 2),new EntagmentPair(3, 2),new EntagmentPair(3, 4), new EntagmentPair(4, 2) };
        private EntagmentPair[] Ibmqx4 = { new EntagmentPair(1, 0), new EntagmentPair(2, 0), new EntagmentPair(2, 1), new EntagmentPair(2, 4), new EntagmentPair(3,2), new EntagmentPair(3, 4) };
        private EntagmentPair[] Ibmqx5 = { new EntagmentPair(1, 0), new EntagmentPair(1, 2), new EntagmentPair(2, 3), new EntagmentPair(3, 4), new EntagmentPair(3, 14), new EntagmentPair(5, 4)
                    , new EntagmentPair(6, 5), new EntagmentPair(6, 7), new EntagmentPair(6, 11), new EntagmentPair(7, 10), new EntagmentPair(8, 7), new EntagmentPair(9, 8), new EntagmentPair(9, 10)
                    , new EntagmentPair(11, 10), new EntagmentPair(12, 11), new EntagmentPair(12, 5), new EntagmentPair(12, 13), new EntagmentPair(13, 4), new EntagmentPair(13, 14), new EntagmentPair(15,0)
                    , new EntagmentPair(15, 2), new EntagmentPair(15, 14)
        };

        public ControlledNOT()
        {

        }

        public ControlledNOT(int TargetIndex,int ControlIndex, IbmQXDevices device)
        {
            if(IsValidConfiguration(TargetIndex,ControlIndex,device) == false)
                throw new System.ArgumentException(string.Format("CX gate between q[{0}], q[{1}] is not allowed in this topology", TargetIndex, ControlIndex), "CNOT gate error");
            this.ControlIndex = ControlIndex;
            this.TargetIndex = TargetIndex;
        }

        public override string ToString()
        {
            return string.Format("cx q[{0}],q[{1}];", ControlIndex,TargetIndex);
        }

        private bool IsValidConfiguration(int TargetIndex, int ControlIndex, IbmQXDevices device)
        {
            if(device ==  IbmQXDevices.ibmqx2)
            {
                var item = (from i in Ibmqx2 where i.TargetIndex == TargetIndex && i.ControlIndex == ControlIndex select i).FirstOrDefault();
                if (item == null)
                    return false;
                else
                    return true;
            }

            else if (device == IbmQXDevices.ibmqx4)
            {
                var item = (from i in Ibmqx4 where i.TargetIndex == TargetIndex && i.ControlIndex == ControlIndex select i).FirstOrDefault();
                if (item == null)
                    return false;
                else
                    return true;
            }

            else if (device == IbmQXDevices.ibmqx5)
            {
                var item = (from i in Ibmqx5 where i.TargetIndex == TargetIndex && i.ControlIndex == ControlIndex select i).FirstOrDefault();
                if (item == null)
                    return false;
                else
                    return true;
            }
            return true;
        }

        public QuantumCommand ParseCommand(string raw_command)
        {
            return null;
        }
    }
}
