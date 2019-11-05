using Set_State_Example.BaseControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set_State_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            bool stop = true;
            Steak account = new Steak("me");
            while (stop)
            {
                //get an input
                var input = Console.ReadLine();
                if (input == "")
                {
                    stop = !stop;
                    break;
                }
                //make sure we have a number
                foreach (char c in input)
                {
                    //if not a number
                    if (Char.IsLetter(c))
                    {
                        //kill the loop and then the loop
                        stop = false;
                        break;
                    }
                }
                //do stuff
                if (stop)
                {
                    double inputNum = double.Parse(input);
                    account.AddTemp(inputNum);
                }
                
                

            }
        }
    }
}
