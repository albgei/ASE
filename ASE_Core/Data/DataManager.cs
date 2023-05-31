using ASE_Interfaces;
using ASE_DataModels.Utils;
using ASE_DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASE_Core.Data
{
    public class DataManager : IDataManager
    {
        private readonly AccountGroup _group;
        public DataManager()
        {
            _group = new AccountGroup();
        }

        internal LinkedList<string> GetAccountNames()
        {
            return new LinkedList<string>(_group._accounts.Keys.ToList());
        }

        internal LinkedList<Transaction> GetTransacctionsFromAccountName(string tName)
        {
            LinkedList<Transaction> tList = new LinkedList<Transaction>();

            foreach (Transaction transaction in _group._transactions.Values)
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
            foreach (Account account in _group._accounts.Values)
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
            _group._accounts.Add(tAccount.AccountName, tAccount);
        }

        internal void RemoveAccount(string tName)
        {
            _group._accounts.Remove(tName);
            LinkedList<int> tListRemove = new LinkedList<int>();
            foreach (var transactionKVP in _group._transactions)
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
                    _group._transactions.Remove(transactionName);
                }
            }
        }

        internal void AddNewTransaction(Transaction tTransaction)
        {
            _group._transactions.Add(tTransaction.TransactionId, tTransaction);
        }


        internal void RemoveTransaction(int tId)
        {
            _group._transactions.Remove(tId);
        }

        internal int GetNewTransactionId()
        {
            if (_group._transactions.ContainsKey(_group._transactions.Count))
            {
                int i = 0;
                foreach (var transaction in _group._transactions)
                {
                    if (!_group._transactions.ContainsKey(i))
                    {
                        return i;
                    }
                    i++;
                }
                return -1;

            }
            else
            {
                return _group._transactions.Count;
            }
        }

        public Dictionary<int, Transaction> GetTransactions()
        {
            return _group._transactions;
        }
        public Dictionary<string, Account> GetAccounts()
        {
            return _group._accounts;
        }



    }
}
