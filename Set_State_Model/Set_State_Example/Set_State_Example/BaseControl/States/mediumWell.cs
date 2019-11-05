using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class mediumWell : State
    {
        public mediumWell(State state) : this(state.currentTemp, state.streak) { }
        public mediumWell(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.streak = steak;
            Initialize();
            CheckState();
        }
        public void Initialize()
        {
            upperTemp = 170;
            lowerTemp = 160;
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
                streak.State = new Welldone(this);
            }
            else if (currentTemp < lowerTemp)
            {
                streak.State = new Medium(this);
            }
        }

        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
            CheckState();
        }
    }
}
