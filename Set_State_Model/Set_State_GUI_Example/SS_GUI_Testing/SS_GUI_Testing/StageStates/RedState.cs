using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_GUI_Testing.StageStates
{
    class RedState : StageState
    {
        public RedState(StageState state) : this(state.stage, state.currentOEE)
        {

        }
        public RedState(SnapGridObject stage, int current_OEE)
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
            else if (currentOEE < expectedOEE && currentOEE >= miniumOEE)
            {
                stage.State = new OrangeState(this);
                stage.brush = statusColour[1];
            }
      }
    }
}
