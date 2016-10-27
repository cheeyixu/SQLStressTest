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
        private static string[] IndividualCustomerColumns = { "Customer_Type", "First_Name", "Last_Name", "Phone", "Age", "Gender", "Street", "City", "State", "Postal_Code" };
        private static string[] EnterpriseCustomerColumns = { "Customer_Type", "First_Name", "Last_Name", "Organization_Name", "Organization_Type", "Street", "City", "State", "Postal_Code" };

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

            using (cnn = new SqlConnection(connetionString))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("Connection Open ! ");
                    IndividualCustomer customer = (IndividualCustomer)CreateCustomer(_CustomerType.Individual);
                    string[] values = { customer.CustomerType, customer.FirstName, customer.LastName, customer.PhoneNo.ToString(), customer.Age.ToString(), customer.Gender.ToString(), customer.Street, customer.City, customer.State, customer.Zipcode.ToString() };
                    string strCommand = SqlInsert("Customer", IndividualCustomerColumns, values);
                    Console.WriteLine(strCommand);
                    using (SqlCommand command = new SqlCommand(strCommand, cnn))
                    {
                        command.ExecuteNonQuery();
                    }
                   

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not open connection ! " + ex.ToString());
                }
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
                    CustomerType = type.ToString(),
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
                    CustomerType = type.ToString(),
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

        private static Account CreateAccount()
        {
            
            return null;
        }

        private static string SqlInsert(string table, string[] columns, string[] values)
        {
            string strColumn = "", strValue = "";

            for(int i = 0; i < Math.Max(columns.Length, values.Length); i++)
            {
                try
                {
                    strColumn += columns[i];
                    if (i != columns.Length - 1)
                        strColumn += ", ";

                    try
                    {
                        int.Parse(values[i]);
                        strValue += values[i];
                        if (i != values.Length - 1)
                            strValue += ", ";
                    }
                    catch
                    {
                        strValue += "'" + values[i] + "'";
                        if (i != values.Length - 1)
                            strValue += ", ";
                    }
                }
                catch
                {
                    if (columns.Length > values.Length)
                    {
                        strColumn += columns[i];
                        if (i != columns.Length - 1)
                            strColumn += ", ";
                    }

                    else if (values.Length > columns.Length)
                    {
                        strValue += values[i];
                        if (i != values.Length - 1)
                            strValue += ", ";
                    }
                }
            }

            return "INSERT INTO " + table + " (" + strColumn + ") VALUES (" + strValue + ")";
        }

        private static string SqlUpdate(string table, string[] assignments, string conditions = null)
        {
            string strAssignment = "";
            
            for(int i = 0; i < assignments.Length; i++)
            {
                strAssignment += assignments[i];
                if (i != assignments.Length - 1)
                    strAssignment += ", ";
            }

            if (conditions == null)
                return "UPDATE " + table + " SET " + strAssignment;
            else
                return "UPDATE " + table + " SET " + strAssignment + " WHERE " + conditions;
        }

        private static string SqlDelete(string table, string conditions = null)
        {
            if (conditions == null)
                return "DELETE FROM " + table;
            else 
                return "DELETE FROM " + table + " WHERE " + conditions;
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
