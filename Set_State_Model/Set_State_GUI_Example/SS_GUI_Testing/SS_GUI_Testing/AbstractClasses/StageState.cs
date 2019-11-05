using SS_GUI_Testing.Objects;
using SS_GUI_Testing.StageStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SS_GUI_Testing.AbstractClasses
{
    public abstract class StageState
    {
        #region vars
        protected SnapGridObject _stage;
        protected int _current_OEE_Reading; 
        public int currentOEE
        {
            get { return _current_OEE_Reading; }
            set { _current_OEE_Reading = value; }
        }

        public SnapGridObject stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        protected int expectedOEE;
        protected int miniumOEE;
        /// <summary>
        /// array of the colours that can be in the system
        /// made as array as far more efficient than list and number of elements is known
        /// cannot be changed
        /// </summary>
        protected static readonly Brush[] statusColour = new Brush[4] { Brushes.Green, Brushes.Orange, Brushes.Red, Brushes.Gray };
        #endregion
        #region functions 
        /// <summary>
        /// set the default values of the state
        /// </summary>
        public abstract void setValues();
        /// <summary>
        /// sets a stage to inactive (planned downtime)
        /// </summary>
        public void setOff()
        {
            stage.State = new OffState(this);
            stage.brush = statusColour[3];
        }
        /// <summary>
        /// class used to see if the stage is performing as expected
        /// </summary>
        public abstract void checkMetric();

        #endregion
    }
}
