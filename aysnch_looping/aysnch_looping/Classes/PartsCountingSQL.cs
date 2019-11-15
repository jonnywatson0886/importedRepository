using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace aysnch_looping.Classes
{
    public class PartsCountingSQL
    {
        public SqlConnection Connection;
        private List<GoodPart> Data;

        public PartsCountingSQL()
        {
            Connection = new SqlConnection();
            Data = new List<GoodPart>();
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
                Console.WriteLine("Connecting to SQL Server ... ");

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
            DateTime dt = new DateTime();
            Data = new List<GoodPart>();

            SqlCommand command = new SqlCommand("SPgoodParts", Connection)
                {CommandType = CommandType.StoredProcedure};

            SqlParameter splParameter = new SqlParameter("@TimeStarted", SqlDbType.DateTime)
                {SqlValue = DateTime.Now};

            command.Parameters.Add(splParameter);

            splParameter = new SqlParameter("@Hours", SqlDbType.Int)
                {Value = (hours)};

            command.Parameters.Add(splParameter);

            SqlParameter outputParameter = new SqlParameter("@TimeOfRun", SqlDbType.DateTime)
            {
                Direction = ParameterDirection.Output
            };
          
            command.Parameters.Add(outputParameter);
            
            Connection.Open();

            using (command)
            {
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GoodPart part = new GoodPart();
                    part.Stage = (int) (reader["Stage"]);
                    part.goodPartsCount = (int) (reader["NumberOfGoodParts"]);
                    Data.Add(part); 
                    
                }
                Connection.Close();

                Connection.Open();
                command.ExecuteNonQuery();
               dt = Convert.ToDateTime(command.Parameters["@TimeOfRun"].Value.ToString());

                Connection.Close();

                Console.WriteLine("Time: " + dt.ToShortTimeString());
                foreach(GoodPart StageData in Data)
                {
                    Console.WriteLine("Stage : "  + StageData.Stage );
                    Console.WriteLine("number of parts: " + StageData.goodPartsCount.ToString() + "\n");
                }
            }
        }
    }
}
