using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{

    public class IndividualCustomer : Customer
    {
        private int _phoneNo;
        private int _age;
        private int _gender;

        public IndividualCustomer() { }

        public IndividualCustomer(int _custId,
                                  string _firstName,
                                  string _lastName, 
                                  int _phoneNo,
                                  int _age,
                                  int _gender,
                                  string _street = null, 
                                  string _city = null, 
                                  string _state = null, 
                                  int _zipcode = 0,
                                  int _customerType = 0) : base(_custId, _firstName, _lastName, _street, _city, _state, _zipcode, _customerType)
        {
            this._phoneNo = _phoneNo;
            this._age = _age;
            this._gender = _gender;
        }

        public int PhoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public int Age { get { return _age; } set { _age = value; } }
        public int Gender { get { return _gender; } set { _gender = value; } }
    }
}
