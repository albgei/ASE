using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_DataModels.Utils;

namespace ASE_DataModels
{
    public class Account
    {
        public string AccountName { get; set; }
        public decimal Interest { get; set; }
        public decimal MonthlySavingsGoal { get; set; }
        public decimal AccountBalance
        {
            get
            {
                return _AccountBalance;
            }
            set
            {
                if (_AccountBalance + value >= 0)
                {
                    _AccountBalance += value;
                }
            }
        }

        private decimal _AccountBalance = 0;


        public Account(string accountname, decimal initialaccountbalance, decimal interest, decimal monthlySavingsGoal)
        {
            AccountName = StringUtils.FirstCharToUpper(accountname);
            _AccountBalance = initialaccountbalance;
            Interest = interest;
            MonthlySavingsGoal = monthlySavingsGoal;
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Zinssatz: {1:0,0.##}%, Monatliches Sparziel: {2:0,0.##}", AccountName, Interest, MonthlySavingsGoal);
        }
    }
}
