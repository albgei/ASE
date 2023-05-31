using ASE_Core.Data;
using ASE_DataModels.Utils;
using ASE_DataModels;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Savings
{
    public class BalanceCalculator
    {
        private readonly DataManager _data;
        public BalanceCalculator(DataManager tData)
        {
            _data = tData;
        }

        internal decimal GetBalanceTotal(DateTime date)
        {
            LinkedList<Account> accounts = new LinkedList<Account>();
            LinkedList<Transaction> transactions = new LinkedList<Transaction>();
            decimal balance = 0;
            foreach (var account in _data.GetAccounts().Values)
            {
                accounts.AddLast(account);
            }

            foreach (var transaction in _data.GetTransactions().Values)
            {
                if (transaction.TransactionDate <= date)
                {
                    transactions.AddLast(transaction);
                }
            }

            foreach (var account in accounts)
            {
                balance += CalculateAccountBalance(account, transactions, date);
            }

            return balance;
        }

        internal decimal GetBalanceByAccount(DateTime date, Account account)
        {
            LinkedList<Transaction> transactions = new LinkedList<Transaction>();
            decimal balance = 0;

            foreach (var transaction in _data.GetTransactions().Values)
            {
                if (transaction.TransactionDate <= date && (transaction.ToAccount.Equals(account.AccountName) || transaction.FromAccount.Equals(account.AccountName)))
                {
                    transactions.AddLast(transaction);
                }
            }
            balance = CalculateAccountBalance(account, transactions, date);

            return balance;
        }

        internal Dictionary<Account, decimal> GetAllBalancesSeperate(DateTime date)
        {
            LinkedList<Account> accounts = new LinkedList<Account>();
            LinkedList<Transaction> transactions = new LinkedList<Transaction>();

            Dictionary<Account, decimal> allBalances = new Dictionary<Account, decimal>();
            foreach (var account in _data.GetAccounts().Values)
            {
                accounts.AddLast(account);
            }

            foreach (var transaction in _data.GetTransactions().Values)
            {
                if (transaction.TransactionDate <= date)
                {
                    transactions.AddLast(transaction);
                }
            }

            foreach (var account in accounts)
            {
                allBalances.Add(account, CalculateAccountBalance(account, transactions, date));
            }

            return allBalances;
        }


        private decimal CalculateAccountBalance(Account account, LinkedList<Transaction> transactions, DateTime date)
        {
            decimal balance = account.AccountBalance;
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionInterval == 0)
                {
                    balance = processTransaction(account, balance, transaction);
                }
                else if (transaction.TransactionInterval > 0)
                {
                    DateTime tDate = transaction.TransactionDate;
                    while (tDate <= date)
                    {
                        balance = processTransaction(account, balance, transaction);

                        tDate = tDate.AddMonths(transaction.TransactionInterval);
                    }
                }
                else if (transaction.TransactionInterval == -1)
                {
                    DateTime tDate = transaction.TransactionDate;
                    while (tDate <= date)
                    {
                        if (tDate.Month == 12)
                        {
                            balance = processTransaction(account, balance, transaction);
                        }
                        balance = processTransaction(account, balance, transaction);

                        tDate = tDate.AddMonths(1);
                    }

                }


            }


            return balance;
        }

        private decimal processTransaction(Account account, decimal balance, Transaction transaction)
        {
            if (transaction.ToAccount.Equals(account.AccountName))
            {
                balance += transaction.Amount;
            }

            if (transaction.FromAccount.Equals(account.AccountName))
            {
                balance -= transaction.Amount;
            }

            return balance;
        }
    }
}
