using Set_State_Example.BaseControl.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example.BaseControl
{
    class Steak
    {
        private State _state;
        private string _cook;

        // Constructor
        public Steak(string cook)
        {
            _cook = cook;
            _state = new NotCooked(this);
        }

        // Properties
        public double CurrentTemp
        {
            get { return _state.currentTemp; }
        }

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void AddTemp(double amount)
        {
            _state.appTemp(amount);
            Console.WriteLine("Increased temperature by {0} degrees.", amount);
            Console.WriteLine(" Current temp is {0}", this.CurrentTemp);
            Console.WriteLine(" Status is {0}", this.State.GetType().Name + "\n");
      
        }

        public void RemoveTemp(double amount)
        {
            _state.RemoveTemp(amount);
            Console.WriteLine("Decreasd temperature by {0} degrees.", amount);
            Console.WriteLine(" Current temp is {0}", this.CurrentTemp);
            Console.WriteLine(" Status is {0}", this.State.GetType().Name + "\n");

        }
    }
}

