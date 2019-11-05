using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace aysnch_looping.Classes
{
    class SqlFucntions
    {
        #region vars
        public List<string> Names = new List<string>();
        public List<string>[] Columns;
        public SqlConnection Connection = new SqlConnection();

        #endregion
        #region functions

        private async Task<List<string>> GetColumnsAsync(string table)
        {
            List<string> asyncCol = new List<string>();
            try
            {
                asyncCol = await Task.Run(() => FillList(table));
            }
            //for any issues
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return asyncCol;
        }
        private async Task<List<string>> FillList(string table)
        {
            List<string> asyncCol = new List<string>();
            using (SqlCommand command = new SqlCommand("SPGetColumns", Connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Input", table));

                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    asyncCol.Add(reader["COLUMN_NAME"].ToString());
                }
                Connection.Close();
            }
            return asyncCol;
        }
        public void ConnectToDb()
        {
            try
            { 
                //create a connection string using a connection string builder
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                //the DataSouce is the sever of the DB
                builder.DataSource = @"DEVELOPMENT-L19\TESTSQL";

                //the initialCatalog is the the database on the sever you want to connect to
                builder.InitialCatalog = "FooBarProductionCopy";

                // Turn on integrated security:
                //this replaces the user name and password for the sever
                builder.IntegratedSecurity = true;

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");

                //create the connection using the connection string we made above 
                Connection = new SqlConnection(builder.ConnectionString);
            }
            //for any issues
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetTables()
        {
            try
            {
                //use a using statement that using a new sql command using the SP that i wish to use and the string builder that we set up above
                using (SqlCommand cmd = new SqlCommand("spFooBarGetTables", Connection))
                {
                    //tell the command it is going to use a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //open the connection to the database
                    Connection.Open();
                    //execute the SP
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //use a reader to go though the output
                    while (dataReader.Read())
                    {
                        Names.Add(dataReader["TABLE_NAME"].ToString());
                    }
                    //close the connection to the database so another process can use it
                    Connection.Close();
                }
            }
            //for any issues with db connection 
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public async Task GetMultipleColumnsAsync(List<string> names)
        {
            List<Task<List<string>>> tasks = new List<Task<List<string>>>();
            foreach (string VARIABLE in names)
            {
                tasks.Add(FillList(VARIABLE));
            }

            Columns = await Task.WhenAll(tasks);
        }
    }


    #endregion
}

