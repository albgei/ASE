using ASE_Core.Data;
using ASE_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Controller
{
    public class DataManager
    {

        private Dictionary<string, Account> _accounts = new Dictionary<string, Account>();
        private Dictionary<int, Transaction> _transactions = new Dictionary<int, Transaction>();


        internal LinkedList<string> GetAccountNames()
        {
            return new LinkedList<string>(_accounts.Keys.ToList());
        }

        internal LinkedList<Transaction> GetTransacctionsFromAccountName(string tName)
        {
            LinkedList<Transaction> tList = new LinkedList<Transaction>();

            foreach (Transaction transaction in _transactions.Values)
            {
                if (tName.Equals(transaction.FromAccount) || tName.Equals(transaction.ToAccount))
                {
                    tList.AddLast(transaction);
                }
            }
            return tList;
        }

        internal Account? GetAccountFromAccountName(string tName)
        {
            foreach (Account account in _accounts.Values)
            {
                if (tName.Equals(account.AccountName))
                {
                    return account;
                }
            }
            return null;
        }

        internal void AddNewAccount(Account tAccount)
        {
            _accounts.Add(tAccount.AccountName, tAccount);
        }

        internal void RemoveAccount(string tName)
        {
            _accounts.Remove(tName);
            LinkedList<int> tListRemove = new LinkedList<int>();
            foreach (var transactionKVP in _transactions)
            {
                Transaction transaction = transactionKVP.Value;
                if (transaction.ToAccount.Equals(tName) || transaction.FromAccount.Equals(tName))
                {
                    tListRemove.AddLast(transactionKVP.Key);
                }
            }
            if (tListRemove.Count > 0)
            {
                foreach (var transactionName in tListRemove)
                {
                    _transactions.Remove(transactionName);
                }
            }
        }

        internal void AddNewTransaction(Transaction tTransaction)
        {
            _transactions.Add(tTransaction.TransactionId, tTransaction);
        }


        internal void RemoveTransaction(int tId)
        {
            _transactions.Remove(tId);
        }

        internal int GetNewTransactionId()
        {
            if (_transactions.ContainsKey(_transactions.Count))
            {
                int i = 0;
                foreach (var transaction in _transactions)
                {
                    if (!_transactions.ContainsKey(i))
                    {
                        return i;
                    }
                    i++;
                }
                return -1;

            }
            else
            {
                return _transactions.Count;
            }
        }

        internal Dictionary<int, Transaction> GetTransactions()
        {
            return _transactions;
        }
        internal Dictionary<string, Account> GetAccounts()
        {
            return _accounts;
        }



    }
}
