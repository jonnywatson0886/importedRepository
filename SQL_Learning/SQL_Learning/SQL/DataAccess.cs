using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Dapper;

namespace SQL_Learning
{
    public class DataAccess
    {
        #region Gathering data
        /// <summary>
        /// get all data from the table
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public BindingList<Customer> getAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomers_GetAll").ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }
        }
        /// <summary>
        /// looks for all data from all entries where name is equal to string passed though
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public BindingList<Customer> LastNameSearch(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomers_GetByLastName @Name", new { Name = name }).ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }

        }
        public BindingList<Customer> FirstNameSearch(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomers_GetByFirstName @Name", new { Name = name }).ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }
        }
        /// <summary>
        /// looks for all data from all entries where Country is equal to string passed though
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public BindingList<Customer> CountrySearch(string country)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomers_GetByCounty @Country", new { Country = country }).ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }
        }
        /// <summary>
        /// ooks for all data from all entries where CustomerID is equal to value passed though
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BindingList<Customer> IDSearch(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomer_GetByID @CustomerId", new { CustomerId = id }).ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }
        }
        public BindingList<Customer> GetAllCustomers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                var output = connection.Query<Customer>("dbo.spCustomers_getAll").ToList();
                BindingList<Customer> itemHolderList = new BindingList<Customer>();
                foreach (Customer c in output)
                {
                    itemHolderList.Add(c);
                }
                return itemHolderList;
            }
        }

        #endregion
        #region Editing Data
        /// <summary>
        /// will attempt to add in new enetry with data passed though
        /// but will also return if data has gone though or not 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public void AddCustomer(string firstName, string lastName, string country)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("FirstDB")))
            {
                connection.Execute("dbo.spCustomers_CreateNewCustomer @FirstName, @LastName, @Country", new { FirstName = firstName, LastName = lastName, Country = country });
            }  
        }
         public void RemoveCustomer(int Id)
        {
            //creating the connnection to the database displayed here as a string. helper class function uses the database name to get the connection string for the user
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(helper.CnnVal("firstDB")))
            {
                //connection.Excxute fires a stored Procedure passed though as a string
                //the new statement creates an object for the connection fucntion to use. the name of the var being changed inside should match the var set in the SP
                connection.Execute("dbo.sp_Customers_RemoveByID @ID", new { ID = Id });
            }
        }
        #endregion
    }
}
