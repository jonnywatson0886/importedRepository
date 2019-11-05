using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.StageStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SS_GUI_Testing.Objects
{
    public class SnapGridObject
    {
        public double height;
        public double width;
        public double startX
        { get { return (width * column); } }
        public double startY
        { get { return (height * row); } }
        private int row;
        private int column;
        public Brush brush;
        private StageState _State;
        public string name;

        public StageState State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                }
            }
        }
        public SnapGridObject(double xScale, double yScale, int Row, int Column)
        {
            width = yScale;
            height = xScale;
            row = Row;
            column = Column;
            brush = Brushes.Green;
            State = new OffState(this);
        }
        public bool Iswithin(Point Mouse)
        {
            if (Mouse.X > startX && Mouse.X < (startX + width))
            {
                if (Mouse.Y > startY && Mouse.Y < (startY + height))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
