using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    /// <summary>
    /// if callled should end the software
    /// </summary>
    class burnt : State
    {
        public burnt(State state) : this(state.currentTemp, state.streak) { }

        public burnt(double currentTemp, Steak steak)
        {
            Console.WriteLine("you have burnt the steak. make a new one");
            int useless = Console.Read();
            System.Environment.Exit(1);
        }
        //should never be called
        public override void appTemp(double temp)
        {
            currentTemp += temp;
            CheckState();
        }

        /// <summary>
        /// should never be called
        /// </summary>
        public override void CheckState()
        {
            if (currentTemp > upperTemp)
            {
             
            } 
            else if (currentTemp < lowerTemp)
            {
            
            }
        }

        /// <summary>
        /// should never be called
        /// </summary>
        /// <param name="temp"></param>
        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
            CheckState();
        }
    }
}
