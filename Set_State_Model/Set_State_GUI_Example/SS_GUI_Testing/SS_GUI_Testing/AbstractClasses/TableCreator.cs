using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SS_GUI_Testing.AbstractClasses
{
    public static class TableCreator
    {
        public static List<SnapGridObject> CreateSnapToGrid(ref Canvas canvas, double scale)
        {
            List<SnapGridObject> table = new List<SnapGridObject>();
            double length = (canvas.ActualWidth / scale) * 0.5;
            double height = (canvas.ActualHeight / scale) * 0.8;
            for (int row = 0; row < scale * 1.25; row++)
            {
                for (int column = 0; column < scale * 2; column++)
                {
                    table.Add(new SnapGridObject(height, length, row, column));
                }
            }
            return table;
        }
    }
}
