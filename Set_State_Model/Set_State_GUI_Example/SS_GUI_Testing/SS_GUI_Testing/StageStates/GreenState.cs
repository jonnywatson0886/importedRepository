using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.Objects;

namespace SS_GUI_Testing.StageStates
{
    class GreenState : StageState
    {
        public GreenState(StageState state) : this(state.stage, state.currentOEE)
        {

        }
        public GreenState(SnapGridObject stage, int currentOEE)
        {
            this.stage = stage;
            this._current_OEE_Reading = currentOEE;
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

