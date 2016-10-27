using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SQLStressTest
{
    public class Customer
    {
        private int _custId;
        private string _street;
        private string _city;
        private string _state;
        private int _zipcode;
        private string _customerType;
        private string _firstName;
        private string _lastName;
        private static int _numCust = 0;

        public Customer()
        {
            _numCust++;
        }

        public Customer(int _custId, 
                         string _firstName = null, 
                         string _lastName = null, 
                         string _street = null, 
                         string _city = null, 
                         string _state = null, 
                         int _zipcode = 0,
                         string _customerType = null)
        {
            this._custId = _custId;
            this._street = _street;
            this._city = _city;
            this._state = _state;
            this._zipcode = _zipcode;
            this._customerType = _customerType;
            this._firstName = _firstName;
            this._lastName = _lastName;
            _numCust++;
        }

        public int CustId { get { return _custId; } set { _custId = value; } }
        public string Street { get { return _street; } set { _street = value; } }
        public string City { get { return _city; } set { _city = value; } }
        public string State { get { return _state; } set { _state = value; } }
        public int Zipcode { get { return _zipcode; } set { _zipcode = value; } }
        public string CustomerType { get { return _customerType; } set { _customerType = value; } }
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public static int NumCust { get { return _numCust; } }
    }
}
