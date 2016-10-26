using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class EnterpriseCustomer : Customer
    {
        private string _organizationName;
        private string _organizationType;

        public EnterpriseCustomer() { }

        public EnterpriseCustomer(int _custId,
                                  string _firstName,
                                  string _lastName,
                                  string _organizationName,
                                  string _organizationType,
                                  string _street = null,
                                  string _city = null,
                                  string _state = null,
                                  int _zipcode = 0,
                                  int _customerType = 0) : base(_custId, _firstName, _lastName, _street, _city, _state, _zipcode, _customerType)
        {
            this._organizationName = _organizationName;
            this._organizationType = _organizationType;
        }

        public string OrganizationName { get { return _organizationName; } set { _organizationName = value; } }
        public string OrganizationType { get { return _organizationType; } set { _organizationType = value; } }
    }
}
