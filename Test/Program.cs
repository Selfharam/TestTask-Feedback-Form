using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
namespace Test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
           string connectionString = "Server=DESKTOP-24S5SPT\\MSSQLSERVER1;Database=test;User Id=test;Password=test;Encrypt=False;Trusted_Connection=False";
            
           SqlConnection connection = new SqlConnection(connectionString);
            
           ApplicationConfiguration.Initialize();
           Application.Run(new Form1(connection));
        }
    }
}
