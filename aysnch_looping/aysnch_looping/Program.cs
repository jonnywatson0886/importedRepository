using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using aysnch_looping.Classes;
using System.Threading;

namespace aysnch_looping
{
    class Program
    {
        public SqlConnection Connection;
        private List<GoodPart> parts;

        public Program()
        {
            Connection = new SqlConnection();
            parts = new List<GoodPart>();
        }

        /// <summary>
        /// creates a connection to the the foobar database via a connection string
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task Main(string[] args)
        {
            PartsCountingSQL sql = new PartsCountingSQL();
            sql.ConnectToDb();
            for (int i = 0; i < 50; i++)
            {
                sql.runPartsSP(1, 6);
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }


       
       
    }
}
