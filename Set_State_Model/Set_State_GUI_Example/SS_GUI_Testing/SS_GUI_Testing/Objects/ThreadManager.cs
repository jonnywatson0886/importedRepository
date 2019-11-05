using SS_GUI_Testing.Single_Use_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SS_GUI_Testing.Objects
{
    public class ThreadManager
    {
        Binding_Manager binding_Manager;

        public ThreadManager()
        {
            binding_Manager = Binding_Manager.Instance;
        }
        public async Task<Task> doWork(AbstractClasses.User_Control_Template screen)
        {
            if (screen.table != null)
            {
                screen.Dispatcher.Invoke(new Action(async () => { await checkSquare(); }), System.Windows.Threading.DispatcherPriority.ContextIdle);
            }
            return Task.CompletedTask;
        }
        private async Task checkSquare()
        {
            List<Task> toDo = new List<Task>();
            foreach (SnapGridObject gridObject in binding_Manager.LiveScreen.table)
            {
                toDo.Add(updateColours(gridObject));
            }
            await Task.WhenAll(toDo);
        }
        private Task updateColours(SnapGridObject gridObject)
        {
            if (gridObject.State.currentOEE >= 100)
            {
                gridObject.State.currentOEE = 0;
            }
            if (binding_Manager.LiveScreen.table.FindIndex(x => x == gridObject) % 2 == 0)
            {
                gridObject.State.currentOEE += 30;
            }
            else
            {
                gridObject.State.currentOEE += 10;
            }
            gridObject.State.checkMetric();
            foreach (Rectangle R in binding_Manager.LiveScreen.thisCanvas.Children)
            {
                Point recPoint = new Point(Canvas.GetLeft(R) + 10, Canvas.GetTop(R) + 10);
                if (gridObject.Iswithin(recPoint))
                {
                    R.Fill = gridObject.brush;
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}
