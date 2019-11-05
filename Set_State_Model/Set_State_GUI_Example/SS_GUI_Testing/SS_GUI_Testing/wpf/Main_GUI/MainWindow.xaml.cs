using SS_GUI_Testing.Single_Use_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SS_GUI_Testing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region vars
        Controller controller;
        #endregion
        #region ctor
        public MainWindow()
        {
            InitializeComponent();
            controller = Controller.Instance;
            DataContext = controller.binding_Manager;
        }
        #endregion
        #region button event handlers
        private void New_but_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Save_but_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_but_Click(object sender, RoutedEventArgs e)
        {
            controller.loadInDiagram();
        }
        private void Exit_but_Click(object sender, RoutedEventArgs e)
        {
            controller.endProgram();
        }
        #endregion
    }
}
