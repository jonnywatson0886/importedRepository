using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class Welldone : State
    {
        public Welldone(State state) : this(state.currentTemp, state.streak) { }
        public Welldone(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.streak = steak;
            Initialize();
            CheckState();
        }
        public void Initialize()
        {
            upperTemp = 200;
            lowerTemp = 170;
        }
        public override void appTemp(double temp)
        {
            currentTemp += temp;
            CheckState();
        }

        public override void CheckState()
        {
            if (currentTemp > upperTemp)
            {
                streak.State = new burnt(this);
            }
            else if (currentTemp < lowerTemp)
            {
                streak.State = new mediumWell(this);
            }
        }

        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
            CheckState();
        }
    }
}
