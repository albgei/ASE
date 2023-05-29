using ASE_Core.Controller;
using ASE_Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ASE_Core.Savings
{
    public class SavingsCalculator
    {
        private readonly DataManager _data;
        private readonly BalanceCalculator _balance;
        public SavingsCalculator(BalanceCalculator balance, DataManager data)
        {
            _balance = balance;
            _data = data;
        }

        internal decimal GetTotalSavings(DateTime date)
        {
            decimal totalSavings = 0;
            Dictionary<Account, decimal> balancebyAccount = new Dictionary<Account, decimal>();
            balancebyAccount = _balance.GetAllBalancesSeperate(date);

            foreach (var account in balancebyAccount)
            {
                totalSavings += account.Value - account.Key.AccountBalance;
            }


            return totalSavings;
        }

        internal decimal GetSavingsByAccount(DateTime date, Account account)
        {
            decimal balance = 0;
            decimal savings = 0;

            balance = _balance.GetBalanceByAccount(date, account);
            savings = balance - account.AccountBalance;

            return savings;
        }

        internal Dictionary<Account, decimal> GetAllSavingsSeperate(DateTime date)
        {

            Dictionary<Account, decimal> allSavings = new Dictionary<Account, decimal>();
            Dictionary<Account, decimal> allBalances = new Dictionary<Account, decimal>();
            decimal savings = 0;

            allBalances = _balance.GetAllBalancesSeperate(date);

            foreach (var account in allBalances)
            {
                savings = GetSavingsByAccount(date, account.Key);
                allSavings.Add(account.Key, savings);
            }


            return allSavings;
        }

        internal bool IsAccountAboveSavingThreashhold(Account account, DateTime date)
        {

            DateTime tDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            decimal savingsPerMonth = 0;

            while (tDate <= date)
            {
                savingsPerMonth = GetSavingsByAccount(tDate, account) - savingsPerMonth;
                if (savingsPerMonth < account.MonthlySavingsGoal)
                {
                    return false;
                }
                tDate = tDate.AddMonths(1);
            }

            return true;
        }

        internal bool AreAllAccountsAboveSavingsThreashhold(DateTime date)
        {
            bool testpassed = true;
            foreach (var account in _data.GetAccounts().Values)
            {
                if (!IsAccountAboveSavingThreashhold(account, date))
                {

                    testpassed = false;
                    return testpassed;
                }
            }

            return testpassed;

        }

        internal DateTime GetFirstFailingMonth(Account account, DateTime date)
        {

            DateTime tDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);

            while (tDate <= date)
            {
                if (IsAccountAboveSavingThreashhold(account, tDate))
                {
                    return tDate;
                }
                tDate = tDate.AddMonths(1);
            }
            return DateTime.MinValue;
        }
    }
}
