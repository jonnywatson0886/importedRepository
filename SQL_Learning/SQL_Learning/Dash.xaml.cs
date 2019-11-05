using SQL_Learning.Miscellaneous;
using System.Windows;

namespace SQL_Learning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller Controller;
        public MainWindow()
        {
            InitializeComponent();
            Controller = new Controller();
            DataContext = Controller;
         }

    } 
}



