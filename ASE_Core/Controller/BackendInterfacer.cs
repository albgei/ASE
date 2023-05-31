using ASE_Interfaces;
using ASE_Persistence;
using ASE_DataModels;
using ASE_Core.Savings;
using ASE_DataModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Data
{
    public class BackendInterfacer
    {
        private readonly IFrontend _frontend;
        private readonly IPersist _persister;
        private readonly BalanceCalculator _balance;
        private readonly SavingsCalculator _savings;
        private readonly DataManager _manager;

        public event Action<Account, DateTime>? TransactionHarmful;

        public BackendInterfacer(IFrontend frontend)
        {
            _frontend = frontend;
            _manager = new DataManager();
            _balance = new BalanceCalculator(_manager);
            _persister = new XMLPersister();
            _savings = new SavingsCalculator(_balance, _manager);

        }
        public void LoadInitial()
        {
            _persister.Load(_manager.GetAccounts(), _manager.GetTransactions());
        }

        public LinkedList<string> GetAccountNames()
        {
            return _manager.GetAccountNames();
        }

        public LinkedList<Transaction> GetTransacctionsFromAccountName(string tName)
        {
            return _manager.GetTransacctionsFromAccountName(tName);
        }

        public Account? GetAccountFromAccountName(string tName)
        {
            return _manager.GetAccountFromAccountName(tName);
        }

        public void AddNewAccount()
        {
            Account tAccount = _frontend.GetNewAccount();
            _manager.AddNewAccount(tAccount);
        }

        public void RemoveAccount(string tName)
        {
            _manager.RemoveAccount(tName);
        }

        public void AddNewTransaction()
        {
            Transaction tTransaction = _frontend.GetNewTransaction();
            _manager.AddNewTransaction(tTransaction);

            DateTime inOneYear = DateTime.Now;
            inOneYear = inOneYear.AddMonths(12);

            Account? account = _manager.GetAccountFromAccountName(tTransaction.FromAccount);

            if (account != null)
                if (!_savings.IsAccountAboveSavingThreashhold(account, inOneYear))
                {
                    DateTime date = _savings.GetFirstFailingMonth(account, inOneYear);
                    TransactionHarmful?.Invoke(account, date);
                }
        }

        public void AddNewIncome()
        {
            Transaction tTransaction = _frontend.GetNewIncome();
            _manager.AddNewTransaction(tTransaction);
        }


        public void RemoveTransaction(int tId)
        {
            _manager.RemoveTransaction(tId);
        }

        public void Persist()
        {
            _persister.Persist();
        }
        public int GetNewTransactionId()
        {
            return _manager.GetNewTransactionId();
        }

        public decimal GetAccountBalance(Account account, DateTime date)
        {
            return _balance.GetBalanceByAccount(date, account);
        }

        public decimal GetAccountSavings(Account account, DateTime date)
        {
            return _savings.GetSavingsByAccount(date, account);
        }

        public decimal GetTotalAccountBalance(DateTime date)
        {
            return _balance.GetBalanceTotal(date);
        }

        public decimal GetTotalAccountSavings(DateTime date)
        {
            return _savings.GetTotalSavings(date);
        }

        public Dictionary<Account, decimal> GetAllAccountBalances(DateTime date)
        {
            return _balance.GetAllBalancesSeperate(date);
        }

        public Dictionary<Account, decimal> GetAllAccountSavings(DateTime date)
        {
            return _savings.GetAllSavingsSeperate(date);
        }

        public DateTime GetFirstFailingMonth(Account account, DateTime date)
        {
            return _savings.GetFirstFailingMonth(account, date);
        }

        public bool AreAllAccountsAboveSavingsThreashhold(DateTime date)
        {
            return _savings.AreAllAccountsAboveSavingsThreashhold(date);
        }

        public bool IsAccountAboveSavingThreashhold(Account account, DateTime date)
        {
            return _savings.IsAccountAboveSavingThreashhold(account, date);
        }
    }
}
