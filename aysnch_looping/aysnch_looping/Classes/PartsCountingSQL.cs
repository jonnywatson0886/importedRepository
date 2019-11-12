using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aysnch_looping.Classes
{
    public class PartsCountingSQL
    {
        public SqlConnection Connection;
        private List<GoodPart> _parts;

        public PartsCountingSQL()
        {
            Connection = new SqlConnection();
            _parts = new List<GoodPart>();
        }

        /// <summary>
        /// connects to the db
        /// </summary>
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

        /// <summary>
        /// runs and gathers info from the SPgoodParts SP 
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="stage"></param>
        public void runPartsSP(int hours, int stage)
        {
            _parts = new List<GoodPart>();

            SqlCommand command = new SqlCommand("SPgoodParts", Connection)
                {CommandType = CommandType.StoredProcedure};

            SqlParameter splParameter = new SqlParameter("@TimeStarted", SqlDbType.DateTime)
                {SqlValue = DateTime.Now};

            command.Parameters.Add(splParameter);

            splParameter = new SqlParameter("@Hours", SqlDbType.Int)
                {Value = (hours)};

            command.Parameters.Add(splParameter);

            splParameter = new SqlParameter("@Stage", SqlDbType.Int)
                {Value = (stage)};

            command.Parameters.Add(splParameter);

            using (command)
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GoodPart part = new GoodPart();
                    part.RecordDate = (DateTime) (reader["RecordDate"]);
                    part.TimeOfRun = (DateTime) (reader["TimeOfRun"]);
                    part.Stage = (int) (reader["Stage"]);
                    part.tool = (int) (reader["tool"]);

                    _parts.Add(part);
                }
                Connection.Close();
                Console.WriteLine("number of results at :" + _parts[_parts.Count-1].TimeOfRun + " " + _parts.Count + "''\n");
            }
        }
    }
}
