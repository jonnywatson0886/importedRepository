using DashBoardDemo.GUI_Handler_Classes;
using DashBoardDemo.Save_Classes;
using SS_GUI_Testing.AbstractClasses;
using SS_GUI_Testing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SS_GUI_Testing.Single_Use_Classes
{
    public class Controller
    {
        #region singleton patten
        private static Controller _instance = null;
        private static readonly object padlock = new object();
        Controller()
        {
            binding_Manager = Binding_Manager.Instance;
            binding_Manager.setController(ref Controller._instance);
            make_canvas();
            binding_Manager.LiveScreen = binding_Manager.Screens[0];
            int index = 0;
            threads = new ThreadManager();
            timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        public static Controller Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Controller();
                    }
                    return _instance;
                }
            }
        }
        #endregion  
        #region vars
        public Binding_Manager binding_Manager;
        public ThreadManager threads;
        private const int scale = 10;
        private Timer timer;
        private int index;
        #endregion
        #region functions
        #region On load Functions
        /// <summary>
        /// starts the creation of a  canvas on a page loadup
        /// </summary>
        public void make_canvas()
        {
            GetListOfClasses(Assembly.GetExecutingAssembly(), "SS_GUI_Testing.wpf.userControls");
        }
        /// <summary>
        /// Goes though a name space  and if a class
        /// called Dyamic_canvas exsits it will create an instance of it
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        private void GetListOfClasses(Assembly assembly, string nameSpace)
        {
            var list = assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.OrdinalIgnoreCase)).ToArray();
            foreach (var i in list)
            {
                if (i.Name == "Dyamic_Canvas")
                {
                    createPage(i.ToString());
                }
            }
            nextPage();
        }
        /// <summary>
        /// creates a instance of the main wpf 
        /// form and gives it a unique name 
        /// </summary>
        /// <param name="ClassName"></param>
        private void createPage(string ClassName)
        {
            //get the class name as an object type    
            var objectType = Type.GetType(ClassName);
            SS_GUI_Testing.AbstractClasses.User_Control_Template UC = Activator.CreateInstance(objectType) as User_Control_Template;
            StringBuilder SB = new StringBuilder();
            SB.Append("Diagram");
            SB.Append(binding_Manager.Screens.Count());
            UC.Name = SB.ToString();
            binding_Manager.Screens.Add(UC);
        }
        /// <summary>
        /// moves to the next screen avaible screen
        /// </summary>
        public void nextPage()
        {
            //checking to see if we are on the last index or not
            if (binding_Manager.Screens.FindIndex(x => x == binding_Manager.LiveScreen) == binding_Manager.Screens.Count -1)
            {
                index = 0;     
            }
            binding_Manager.LiveScreen = binding_Manager.Screens[index];
            if (binding_Manager.LiveScreen.table == null)
            {
                binding_Manager.LiveScreen.createTable(scale);
            }
            index++;
        }

        #endregion
        #region form controls
        /// <summary>
        /// ends the program
        /// </summary>
        public void endProgram()
        {
            timer.Stop();
            System.Environment.Exit(1);
        }
        /// <summary>
        /// loads in the diagram presaved
        /// </summary>
        public void loadInDiagram()
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            binding_Manager.LiveScreen.table.Clear();

            List<SquareDetails> temp = Save_Helper.loadDiagram();
            binding_Manager.LiveScreen.createTable(temp[0].scale);
            //clear the children user control
            binding_Manager.LiveScreen.thisCanvas.Children.Clear();
            foreach (SquareDetails square in temp)
            {
                Point point = new Point(binding_Manager.LiveScreen.table[square.index].startX + 10, binding_Manager.LiveScreen.table[square.index].startY + 10);
                binding_Manager.LiveScreen.createSquare(point, binding_Manager.LiveScreen.table[1].width - 10, binding_Manager.LiveScreen.table[1].height - 10);
            }
            timer.Start();
        }
        #endregion
        #region automatic functions 
        /// <summary>
        /// fires each time the timer elapses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            binding_Manager.LiveScreen.Dispatcher.Invoke(new Action(async () => { await UpdateScreens(); }), System.Windows.Threading.DispatcherPriority.ContextIdle); 
        }
        /// <summary>
        /// goes though all the screens in memory and updates thier states
        /// </summary>
        /// <returns></returns>
        private async Task UpdateScreens()
        {
            //create list if tasks
            List<Task> todo = new List<Task>();
            //fill the tasks list with async task of single scrren update
            foreach (User_Control_Template uc in binding_Manager.Screens)
            {
                todo.Add(SingleScreenUpdate(uc));
            }
            //wait for all the values to return before going on in this thread
            await Task.WhenAll(todo);
        }
        /// <summary>
        /// takes a User_control_template and 
        /// updates all the stages visuals
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private async Task<Task> SingleScreenUpdate(User_Control_Template scope)
        {
            await threads.doWork(scope);
            return Task.CompletedTask;
        }

        #endregion
        #endregion
    }

}



