using System;
using System.Threading.Tasks;
using aysnch_looping.Classes;

namespace aysnch_looping
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            SqlFucntions sql = new SqlFucntions();
            sql.ConnectToDb();
            sql.GetTables();
            Console.WriteLine("all tables names");
            for(int index = 0; index < sql.Names.Count; index++)
            {
                
                Console.WriteLine("Table: " + sql.Names[index]);
                await sql.GetMultipleColumnsAsync(sql.Names);
                foreach(string s in sql.Columns[index])
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine('\n');
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }


}
