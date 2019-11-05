using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class Medium : State
    {
        public Medium(State state) : this(state.currentTemp, state.streak) { }
        public Medium(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.streak = steak;
            Initialize();
            CheckState();
        }
        public void Initialize()
        {
            upperTemp = 160;
            lowerTemp = 150;
        }
        public override void appTemp(double temp)
        {
            currentTemp += temp;
            CheckState();
        }

        public override void CheckState()
        {
            if (currentTemp < lowerTemp)
            {
                streak.State = new MediumRare(this);
            }
            else if (currentTemp > upperTemp)
            {
                streak.State = new mediumWell(this);
            }
        }

        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
        }
    }
}
