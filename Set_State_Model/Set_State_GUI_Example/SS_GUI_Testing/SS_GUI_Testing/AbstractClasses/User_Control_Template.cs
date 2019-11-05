using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SS_GUI_Testing.AbstractClasses
{
    public abstract class User_Control_Template : UserControl
    {
        public string Name;
        private Canvas _thisCanvas;
        public List<SnapGridObject> table;

        #region SquareCreation
        public void createTable(int scale)
        {
            table = TableCreator.CreateSnapToGrid(ref thisCanvas, scale);
        }
        public void createSquare(Point Center, double width, double height)
        {
            Rectangle rec = new Rectangle();

 
            rec.Stroke = Brushes.Black;

            rec.Width = width;
            rec.Height = height;

            Canvas.SetLeft(rec, Center.X);
            Canvas.SetTop(rec, Center.Y);

            _thisCanvas.Children.Add(rec);
        }
        #endregion
        #region general Maintance
        public ref Canvas thisCanvas
        {
            get 
            {
                return ref _thisCanvas;
            }
         }

        private void setCanvas(ref Canvas display) 
        {
            _thisCanvas = display;
        } 
       #endregion
        #region customEvents
        /// <summary>
        /// custom event for system to tell if the user needs to alerted or not
        /// </summary>
        public static readonly RoutedEvent Alert = EventManager.RegisterRoutedEvent
            (
                "AlertRaised",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(User_Control_Template)
            );

        public event RoutedEventHandler AlertRaised
        {
            add { AddHandler(Alert, value); }
            remove { RemoveHandler(Alert, value); }
        }
        #endregion
    }
}
