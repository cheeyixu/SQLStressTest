﻿using Bogus;
using System;
using System.Collections;
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
        private static string[] IndividualCustomerColumns = { "Customer_Type", "First_Name", "Last_Name", "Street", "City", "State", "Postal_Code", "Phone", "Age", "Gender" };
        private static string[] EnterpriseCustomerColumns = { "Customer_Type", "First_Name", "Last_Name", "Street", "City", "State", "Postal_Code", "Organization_Name", "Organization_Type" };
        private static string[] AccountColumns = { "Account_Type", "Status", "Balance", "Customer_Id" };

        public enum _Gender
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

        public enum _AccountType
        {
            Savings,
            Current,
            BasicChecking,
            InterestBearingChecking,
            MoneyMarketDeposit
        }

        public enum _AccountStatus
        {
            Active,
            Pending
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

                    InsertCustomersToDatabase(cnn, 3);

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not open connection ! " + ex.ToString());
                }
            }
            /*Console.ReadLine();*/
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
                    Gender = (int)random.Enum<_Gender>()
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

            for(int i = 0; i < Math.Max(columns.Length, values.Length); i++)    //Length of columns and values should always be the same, but just in case.
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
       
        private static string[] ToValueArray(object o)
        {
            ArrayList temp = new ArrayList();

            if (o is Customer)
            {
                Customer _cust = (Customer)o;
                temp.Add(_cust.CustomerType);
                temp.Add(_cust.FirstName);
                temp.Add(_cust.LastName);
                temp.Add(_cust.Street);
                temp.Add(_cust.City);
                temp.Add(_cust.State);
                temp.Add(_cust.Zipcode.ToString());

                if (_cust.CustomerType == _CustomerType.Individual.ToString())
                {
                    IndividualCustomer tempCust = (IndividualCustomer)o;
                    temp.Add(tempCust.PhoneNo.ToString());
                    temp.Add(tempCust.Age.ToString());
                    temp.Add(tempCust.Gender.ToString());
                }
                else if (_cust.CustomerType == _CustomerType.Enterprise.ToString())
                {
                    EnterpriseCustomer tempCust = (EnterpriseCustomer)o;
                    temp.Add(tempCust.OrganizationName);
                    temp.Add(tempCust.OrganizationType);
                }
            }

            return (string[])temp.ToArray(typeof(string));
        }

        private static void InsertCustomersToDatabase(SqlConnection cnn, int numCust = 1)
        {
            Randomizer rand = new Randomizer();
            string strCommand = "";

            for(int i = 0; i < numCust; i++)
            {
                var randCustType = rand.Enum<_CustomerType>();
                var cust = CreateCustomer(randCustType);
                if (randCustType == _CustomerType.Individual)
                    strCommand = SqlInsert("Customer", IndividualCustomerColumns, ToValueArray(cust));
                else if (randCustType == _CustomerType.Enterprise)
                    strCommand = SqlInsert("Customer", EnterpriseCustomerColumns, ToValueArray(cust));

                using(SqlCommand command = new SqlCommand(strCommand, cnn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
