using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Learning
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public string displayInfo
        {
            get
            {
                return $" Name: {FirstName} {LastName}  \n Country: {Country} \n ID: {CustomerID}";
            }
        }
    }
}
