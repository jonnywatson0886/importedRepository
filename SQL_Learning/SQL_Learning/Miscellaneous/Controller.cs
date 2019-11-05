using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace SQL_Learning.Miscellaneous
{
    public class Controller
    {
        #region vars
        int buffer = 10;
        BindingList<Customer> customers;
        BindingList<string> OutputBox_list;
        BindingList<string> EventsLog;
        DataAccess db;
        DataEditor manager;

        private string _SearchName_Txt;
        public string SearchName_Txt
        {
            get { return _SearchName_Txt; }
            set { _SearchName_Txt = value; }
        }

        private string _SearchLastName_Txt;
        public string SearchLastName_Txt
        {
            get { return _SearchLastName_Txt; }
            set { _SearchLastName_Txt = value; }
        }


        private string _SearchCountry_Txt;
        public string SearchCountry_Txt
        {
            get { return _SearchCountry_Txt; }
            set { _SearchCountry_Txt = value; }
        }

        private string _SearchCustomerID_Txt;
        public string SearchCustomerID_Txt
        {
            get { return _SearchCustomerID_Txt; }
            set { _SearchCustomerID_Txt = value; }
        }


        #endregion
        #region constructor
        public Controller()
        {
            db = new DataAccess();
            customers = new BindingList<Customer>();
            manager = new DataEditor();
            OutputBox_list = new BindingList<string>();
            EventsLog = new BindingList<string>();
        }
        #endregion
        public void SQLQuery(object sender)
        {
            string output_Message = "";
            OutputBox_list.Clear();
            try
            {
                Button _sender = (Button)sender;
                switch (_sender.Name)
                {
                    //search all
                    case ("Get_All_Button"):
                        //connect to database and do sql select query
                        customers = db.getAll();
                        //refill the listbox in the bottom
                        foreach (Customer c in customers)
                        {
                            OutputBox_list.Add(c.displayInfo);
                        }
                        output_Message = "sucess database returned " + customers.Count + " results";
                        break;

                    case ("Name_but"):
                        OutputBox_list.Clear();
                        customers = db.FirstNameSearch(SearchName_Txt);
                        if (customers.Count > 0)
                        {
                            foreach (Customer c in customers)
                            {
                                OutputBox_list.Add(c.displayInfo);
                            }
                            output_Message = "sucess search returned " + customers.Count + " results";
                        }
                        else
                        {
                            output_Message = "failed no records match input";
                        }
                        break;
                    case ("Country_but"):
                        customers = db.CountrySearch(SearchCountry_Txt);
                        if (customers.Count > 0)
                        {
                            foreach (Customer c in customers)
                            {
                                OutputBox_list.Add(c.displayInfo);
                            }

                            output_Message = "sucess search returned " + customers.Count + " results";
                        }
                        else
                        {
                            output_Message = "failed no records match input";
                        }
                        break;

                    case ("lastName_but"):

                        customers = db.LastNameSearch(SearchLastName_Txt);
                        if (customers.Count > 0)
                        {
                            foreach (Customer c in customers)
                            {
                                OutputBox_list.Add(c.displayInfo);
                            }
                            output_Message = "sucess search returned " + customers.Count + " results";
                        }
                        else
                        {
                            output_Message = "failed no records match input";
                        }
                        break;

                    case ("ID_but"):
                        int index = -1;

                        int.TryParse(SearchCustomerID_Txt, out index);
                        if (index > 0)
                        {
                            customers = db.IDSearch(index);
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                        if (customers.Count > 0)
                        {
                            foreach (Customer c in customers)
                            {
                                OutputBox_list.Add(c.displayInfo);
                            }
                            output_Message = "sucess search returned " + customers.Count + " results";
                        }
                        else
                        {
                            output_Message = "failed no records match input";
                        }
                        break;
                    case ("Delete_but"):
                        if (int.Parse(SearchCustomerID_Txt) > 0)
                        {
                            if (manager.removeEvent(int.Parse(SearchCustomerID_Txt)))
                            {
                                LogEvent("Removed record " + SearchCustomerID_Txt, DateTime.Now);
                            }
                            else
                            {
                                LogEvent(manager.error, DateTime.Now);
                            } 
                        }
                        break;
                    case ("Add_But"):
                        db.AddCustomer(SearchName_Txt, SearchLastName_Txt, SearchCountry_Txt);
                        output_Message = "Sucess data added to database";
                        break;

                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                LogEvent(e.Message, DateTime.Now);
            }
        }

        #region form val handlers
        /// <summary>
        /// formates a message for the log box and gives it a timestamp
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private void LogEvent(string message, DateTime timestamp)
        {
            //used to store message whislt being created
            StringBuilder sb = new StringBuilder();
            //add timestamp first
            sb.Append(timestamp.ToString("HH:mm:ss"));

            //add buffer space
            for (int i = 0; i < buffer; i++)
            {
                sb.Append(" ");
            }
            //add message 
            sb.Append(message);
            //display in the logbox
            EventsLog.Add(sb.ToString());
        }

        /// <summary>
        /// makes sure that value in text box is valid for type functions
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nameOfSender"></param>
        /// <returns></returns>
        private bool vaildateData(string input, string nameOfSender)
        {
            //check to see if textbox has text first
            if (input.Length == 0)
            {
                return false;
            }
            //used switch as to be able to add vaildation for any textbox with little effort
            switch (nameOfSender)
            {
                //if ID textbox needs to varify it is text
                case ("SearchCustomerID_Txt"):
                    {
                        //go though go all the chars in the string passed though
                        for (int i = 0; i < input.Length; i++)
                        {
                            //check if nor digit as needs to be whole number for id
                            if (!char.IsDigit(input[i]))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                //if no vaildtion needed return true;
                default:
                    return true;
            }
        }
        #endregion
    }
}
