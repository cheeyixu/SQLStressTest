using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    class BankDBTest
    {
        public static string connetionString = "Data Source=SQL-SRV-2016\\SQLSRV2016;Initial Catalog=BankTestDB;User ID=sa;Password=";
        public static SqlConnection cnn = null;

        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            string pwd = Console.ReadLine();
            connetionString += pwd;

            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! " + ex.ToString());
            }
        }
    }
}
