using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class MediumRare : State
    {
        public MediumRare(State state) : this(state.currentTemp, state.streak) { }
        public MediumRare(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.streak = steak;
            Initialize();
            CheckState();
        }
        public void Initialize()
        {
            upperTemp = 150;
            lowerTemp = 140;
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
                streak.State = new Medium(this);
            }
            else if (currentTemp < lowerTemp)
            {
                streak.State = new MediumRare(this);
            }
        }

        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
            CheckState();
        }
    }
}
