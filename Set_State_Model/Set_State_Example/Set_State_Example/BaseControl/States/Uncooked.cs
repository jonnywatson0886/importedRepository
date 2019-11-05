using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class NotCooked : State
    {
        public NotCooked(Steak state)
        {
            this.currentTemp = 0;
            this.streak = state;
            Initialize();
            CheckState();
        }

        private void Initialize()
        {
            lowerTemp = 0;
            upperTemp = 130;
            editble = false;
        }

        public override void appTemp(double temp)
        {
            currentTemp += temp;
            CheckState();
        }

        public override void RemoveTemp(double temp)
        {
            currentTemp -= temp;
            CheckState();
        }
        public override void CheckState()
        {
            if (currentTemp < 0)
            {
                currentTemp = 0;
            }
            else if (currentTemp > upperTemp)
            {
                streak.State = new Rare(this);
            }
        }
    }
}
