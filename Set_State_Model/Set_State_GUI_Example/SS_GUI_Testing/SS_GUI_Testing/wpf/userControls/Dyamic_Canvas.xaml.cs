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

namespace SS_GUI_Testing.wpf.userControls
{
    /// <summary>
    /// Interaction logic for Dyamic_Canvas.xaml
    /// </summary>
    public partial class Dyamic_Canvas 
        {
        public Dyamic_Canvas()
        {
            InitializeComponent();
            thisCanvas = MainCavas;
            Name = "";
        }

   }
}
