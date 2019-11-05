using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_GUI_Testing.StageStates
{
    class OrangeState : StageState
    {
        public OrangeState(StageState state) : this(state.stage , state.currentOEE)
        {
                
        }
        public OrangeState(SnapGridObject stage, int current_OEE)
        {
            this.stage = stage;
            this.currentOEE = current_OEE;
            setValues();
        }          
        public override void setValues()
        {
            miniumOEE = 50;
            expectedOEE = 80;
        }
        public override void checkMetric()
        {
            if (currentOEE >= expectedOEE)
            {
                stage.State = new GreenState(this);
                stage.brush = statusColour[0];
            }
            else if (currentOEE < miniumOEE)
            {
                stage.State = new RedState(this);
                stage.brush = statusColour[2];
            }
        }


    }
}
