using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl.States
{
    class Rare : State
    {
        #region constuctor
        public Rare(State state) : this(state.currentTemp, state.streak) { }

        public Rare(double currentTemp, Steak state)
        {
            this.currentTemp = currentTemp;
            this.streak = state;
            Initialize();
            CheckState();
        }
        private void Initialize()
        {
            this.upperTemp = 140;
            this.lowerTemp = 130;
        }
        #endregion

        public override void appTemp(double temp)
        {
            currentTemp += temp;
            CheckState();
        }

        public override void CheckState()
        {
            if (currentTemp < lowerTemp)
            {
                streak.State = new NotCooked(this.streak);
            }
            else if (currentTemp > upperTemp)
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
