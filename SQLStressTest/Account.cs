using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class Account
    {
        private int _accId;
        private string _accountType;
        private string _status;
        private int _balance;
        private int _custId;

        public Account(int _accId,
                       string _accountType,
                       string _status,
                       int _balance,
                       int _custId)
        {
            this._accId = _accId;
            this._accountType = _accountType;
            this._status = _status;
            this._balance = _balance;
            this._custId = _custId;
        }

        public int AccId { get { return _accId; } set { _accId = value; } }
        public string AccountType { get { return _accountType; } set { _accountType = value; } }
        public string Status { get { return _status; } set { _status = value; } }
        public int Balance { get { return _balance; } set { _balance = value; } }
        public int CustId { get { return _custId; } set { _custId = value; } }
    }
}
