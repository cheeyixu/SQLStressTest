using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class Customers
    {
        public int Cust_Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }

        public Customers(int Cust_Id, string Street = null, string City = null, string State = null, int Zipcode = 0)
        {
            this.Cust_Id = Cust_Id;
            this.Street = Street;
            this.City = City;
            this.State = State;
            this.Zipcode = Zipcode;
        }
    }
}
