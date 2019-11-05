using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl
{
    abstract class State
    {
        #region vars
        #region private
        protected Steak _steak;
        protected double _currentTemp;

        protected double upperTemp;
        protected double lowerTemp;
        protected bool editble;
        #endregion
        #region public vars
        public Steak streak
        {
            get
            {
                return _steak;
            }
            set
            {
                _steak = value;
            }
        }
        public double currentTemp
        {
            get { return _currentTemp; }

            set
            {
                _currentTemp = value;
            }
        }
        #endregion
        #endregion
        #region funcitons 
        public abstract void appTemp(double temp);
        public abstract void RemoveTemp(double temp);
        public abstract void CheckState();
        #endregion
    }
}
