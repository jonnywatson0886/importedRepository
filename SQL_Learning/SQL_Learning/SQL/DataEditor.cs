using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Learning
{
    public class DataEditor
    {
        #region Vars
        private DataAccess SQL_Query;
        public string error;
        #endregion
        #region constuctor
        public DataEditor()
        {
            SQL_Query = new DataAccess();
            error = "";
        }
        #endregion

        /// <summary>
        /// removes an entery though sql class based on id number passed though
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool removeEvent(int ID)
        {
            //reset error message
            error = "";
            try
            {
                //attempt to remove the user based on ID field
                SQL_Query.RemoveCustomer(ID);
                return true;
            }
            //if fails make sure error can get back to the user
            catch (Exception e)
            {
                error = "failure " + e.Message;
                return false;
            }
        }
    }
}
