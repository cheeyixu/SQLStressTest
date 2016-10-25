using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class Customer
    {
        private int _custId;
        public string _street;
        public string _city;
        public string _state;
        public int _zipcode;
        public int _customerType;
        public string _firstName;
        public string _lastName;
        public Customer(int _custId, 
                         string _firstName = null, 
                         string _lastName = null, 
                         string _street = null, 
                         string _city = null, 
                         string _state = null, 
                         int _zipcode = 0,
                         int _customerType = 0)
        {
            this._custId = _custId;
            this._street = _street;
            this._city = _city;
            this._state = _state;
            this._zipcode = _zipcode;
            this._customerType = _customerType;
            this._firstName = _firstName;
            this._lastName = _lastName;
        }

        public int CustId { get { return _custId; } set { _custId = value; } }
        public string Street { get { return _street; } set { _street = value; } }
        public string City { get { return _city; } set { _city = value; } }
        public string State { get { return _state; } set { _state = value; } }
        public int Zipcode { get { return _zipcode; } set { _zipcode = value; } }
        public int AccountType { get { return _customerType; } set { _customerType = value; } }
        public string First_Name { get { return _firstName; } set { _firstName = value; } }
        public string Last_Name { get { return _lastName; } set { _lastName = value; } }
    }
}
