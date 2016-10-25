using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStressTest
{
    public class Transaction
    {
        private int _txnId;
        private int _timeStamp;
        private int _amount;
        private int _balance;
        private string _description;
        private int _accIdTo;
        private int _accIdFrom;

        public Transaction(int _txnId,
                           int _timeStamp,
                           int _amount,
                           int _balance,
                           string _description,
                           int _accIdTo,
                           int _accIdFrom)
        {
            this._txnId = _txnId;
            this._timeStamp = _timeStamp;
            this._amount = _amount;
            this._balance = _balance;
            this._description = _description;
            this._accIdTo = _accIdTo;
            this._accIdFrom = _accIdFrom;
        }

        public int TxnId { get { return _txnId; } set { _txnId = value; } }
        public int TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public int Balance { get { return _balance; } set { _balance = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public int AccIdTo { get { return _accIdTo; } set { _accIdTo = value; } }
        public int AccIdFrom { get { return _accIdFrom; } set { _accIdFrom = value; } }
    }
}
