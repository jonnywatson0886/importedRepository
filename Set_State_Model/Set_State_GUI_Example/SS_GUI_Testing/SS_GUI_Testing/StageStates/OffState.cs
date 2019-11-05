using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_GUI_Testing.StageStates
{
    class OffState : StageState
    {
        public OffState(StageState state) : this(state.stage, state.currentOEE) { }

        public OffState(SnapGridObject stage, int current)
        {
            this.currentOEE = current;
            this.stage = stage;
            stage.brush = statusColour[3];
            setValues();
        }
        public OffState(SnapGridObject gridObject)
        {
            this.stage = gridObject;
            this.currentOEE = 0;
            setValues();
        }
        public override void setValues()
        {
            miniumOEE = 50;
            expectedOEE = 80;
        }
        public override void checkMetric()
        {
            if (currentOEE < expectedOEE && currentOEE >= miniumOEE)
            {
                stage.State = new GreenState(this);
                stage.brush = statusColour[0];
            }
            else if (currentOEE < expectedOEE && currentOEE >= miniumOEE)
            {
                stage.State = new OrangeState(this);
                stage.brush = statusColour[1];
            }
            else if (currentOEE < miniumOEE)
            {
                stage.State = new RedState(this);
                stage.brush = statusColour[2];
            }

        }
    }
}
