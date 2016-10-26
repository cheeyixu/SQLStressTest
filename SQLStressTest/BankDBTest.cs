using Bogus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class BankDBTest
    {
        public static string connetionString = "Data Source=SQL-SRV-2016\\SQLSRV2016;Initial Catalog=BankTestDB;User ID=sa;Password=";
        public static SqlConnection cnn = null;

        public enum Gender
        {
            Male,
            Female
        }

        public enum _CustomerType
        {
            Individual,
            Enterprise
        }

        public enum _OrganizationType
        {
            Electronics,
            FoodAndBeverage,
            Business,
            Property,
            Investment,
            Financa,
            HealthCare,
            Government
        }

        public static void Main(string[] args)
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

        private static Customer CreateCustomer(_CustomerType type)
        {
            var name = new Bogus.DataSets.Name();
            var address = new Bogus.DataSets.Address();
            var random = new Randomizer();

            if (type == _CustomerType.Individual)
            {
                var phoneNo = new Bogus.DataSets.PhoneNumbers();

                return new IndividualCustomer()
                {
                    FirstName = name.FirstName(),
                    LastName = name.LastName(),
                    CustomerType = (int)type,
                    Street = address.StreetName(),
                    City = address.City(),
                    State = address.State(),
                    Zipcode = int.Parse(address.ZipCode("######")),
                    PhoneNo = int.Parse(phoneNo.PhoneNumber("########")),
                    Age = random.Number(18, 100),
                    Gender = (int)random.Enum<Gender>()
                };
            }
            else if (type == _CustomerType.Enterprise)
            {
                var companyName = new Bogus.DataSets.Company();

                return new EnterpriseCustomer()
                {
                    FirstName = name.FirstName(),
                    LastName = name.LastName(),
                    CustomerType = (int)type,
                    Street = address.StreetName(),
                    City = address.City(),
                    State = address.State(),
                    Zipcode = int.Parse(address.ZipCode("######")),
                    OrganizationName = companyName.CompanyName() + companyName.CompanySuffix(),
                    OrganizationType = random.Enum<_OrganizationType>().ToString()
                };
            }
            else return null;
        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}, {1}",
                        reader[0], reader[1]));
                }
            }
        }
    }
}
