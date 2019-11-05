using SS_GUI_Testing.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SS_GUI_Testing.Single_Use_Classes
{
    public sealed class Binding_Manager : INotifyPropertyChanged
    {
        private static Binding_Manager _instance = null;
        private static readonly object padlock = new object();
        private List<User_Control_Template> _Screens;
        private User_Control_Template _LiveScreen;
        private Controller controller;
        public User_Control_Template LiveScreen
        {
            get 
            {
                return _LiveScreen;
            }
            set
            {
                if (value != _LiveScreen)
                {
                    _LiveScreen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<User_Control_Template> Screens
        {
            get 
            {
                return _Screens;
            }
            set
            {
                if (_Screens != value)
                {
                    _Screens = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #region eventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        Binding_Manager()
        {
            Screens = new List<User_Control_Template>();
        }
        public static Binding_Manager Instance
        {
            get
            {
                lock (padlock) ;
                {
                    if (_instance == null)
                    {
                        _instance = new Binding_Manager();
                    }
                    return _instance;
                }
            }
        }
        public void setController(ref Controller _controller)
        {
            controller = _controller;
        }
    }
}
