using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ASE_Core.Data;
using ASE_Core.Interfaces;

namespace Bank_GUI
{
    internal class FrontController : IFrontend
    {
        private readonly BankingGUI _gui;
        private readonly BackendInterfacer _backend;
        public FrontController(BankingGUI bankingGUI)
        {
            _gui = bankingGUI;
            _gui.NewAccountBtnPressedEvt += AddNewAccount;
            _gui.NewTransactionBtnPressedEvt += AddNewTransaction;
            _gui.NewAccountSelected += NewAccountSelected;
            _gui.NewIncomeBtnPressedEvt += AddNewIncome;
            _gui.AccountDeleteBtnPressedEvt += DeleteAccount;
            _gui.TransactionDeleteBtnPressedEvt += DeleteTransaction;
            _gui.FormClosingEvt += OnClose;
            _gui.GetNewTransactionId += GetNewTransactionId;
            _gui.AllAccountSavingsEvt += GetAllSavings;
            _gui.CurrentAccountSavingsEvt += CurrentAccountSavings;
            _gui.FutureAccountSavingsEvt += FutureAccountSavings;

            _backend = new BackendInterfacer(this);
            _backend.TransactionHarmful += TransactionHarmful;
            _backend.LoadInitial();

            Initialize();
        }

        private void FutureAccountSavings(DateTime date)
        {
            LinkedList<string> list = new LinkedList<string>();

            string accountS = _gui.GetSelectedAccount();
            Account? account = _backend.GetAccountFromAccountName(accountS);

            decimal balance = 0;
            decimal savings = 0;
            bool saved = true;
            DateTime savingFailed = DateTime.MinValue;

            if (account != null)
            {
                balance = _backend.GetAccountBalance(account, date);
                savings = _backend.GetAccountSavings(account, date);
                saved = _backend.IsAccountAboveSavingThreashhold(account, date);

                list.AddLast(account.ToString());
                list.AddLast(String.Format("Momentan besitzen sie {0:0,0.##} auf ihrem Konto.", balance));
                list.AddLast(String.Format("Bis jetzt haben sie {0:0,0.##}€ gespart.", savings));

                if (saved == false)
                {
                    savingFailed = _backend.GetFirstFailingMonth(account, date);
                    list.AddLast(String.Format("Am {0: yyyy MM dd} haben sie ihr Sparziel von : {1:0,0.##} nicht erreicht.", savingFailed, account.MonthlySavingsGoal));
                }
            }

            _gui.SetSavings(list);
        }

        private void CurrentAccountSavings()
        {
            LinkedList<string> list = new LinkedList<string>();

            string accountS = _gui.GetSelectedAccount();
            Account? account = _backend.GetAccountFromAccountName(accountS);

            decimal balance = 0;
            decimal savings = 0;
            bool saved = true;
            DateTime savingFailed = DateTime.MinValue;

            if (account != null)
            {
                balance = _backend.GetAccountBalance(account, DateTime.Now);
                savings = _backend.GetAccountSavings(account, DateTime.Now);
                saved = _backend.IsAccountAboveSavingThreashhold(account, DateTime.Now);

                list.AddLast(account.ToString());
                list.AddLast(String.Format("Momentan besitzen sie {0:0,0.##} auf ihrem Konto.", balance));
                list.AddLast(String.Format("Bis jetzt haben sie {0:0,0.##}€ gespart.", savings));

                if (saved == false)
                {
                    savingFailed = _backend.GetFirstFailingMonth(account, DateTime.Now);
                    list.AddLast(String.Format("Am {0: yyyy MM dd} haben sie ihr Sparziel von : {1:0,0.##} nicht erreicht.", savingFailed, account.MonthlySavingsGoal));
                }
            }

            _gui.SetSavings(list);
        }

        private void GetAllSavings()
        {
            LinkedList<string> list = new LinkedList<string>();


            decimal savings = 0;
            DateTime savingFailed = DateTime.MinValue;

            foreach (var accountKVP in _backend.GetAllAccountBalances(DateTime.Now))
            {
                Account account = accountKVP.Key;

                if (account != null)
                {
                    savings = _backend.GetAccountBalance(account, DateTime.Now);

                    list.AddLast(account.ToString());
                    list.AddLast(String.Format("Momentan besitzen sie {0:0,0.##} auf ihrem Konto.", accountKVP.Value));
                    list.AddLast(String.Format("Bis jetzt haben sie {0:0,0.##}€ gespart.", savings));


                    list.AddLast(String.Empty);
                }
            }

            if (!_backend.AreAllAccountsAboveSavingsThreashhold(DateTime.Now))
            {
                foreach (var accountKVP in _backend.GetAllAccountSavings(DateTime.Now))
                {
                    Account account = accountKVP.Key;

                    if (!_backend.IsAccountAboveSavingThreashhold(account, DateTime.Now))
                    {
                        list.AddLast(account.ToString());
                        savingFailed = _backend.GetFirstFailingMonth(account, DateTime.Now);
                        list.AddLast(String.Format("Am {0: yyyy MM dd} haben sie ihr Sparziel von: {1:0,0.##} zum ersten mal nicht erreicht.", savingFailed, account.MonthlySavingsGoal));
                        decimal savingsfailed = _backend.GetAccountSavings(account, savingFailed);
                        list.AddLast(String.Format("Sie haben in diesen Monat nur {1:0,0.##} gespart.", savingsfailed));
                        list.AddLast(String.Empty);
                    }
                }
            }

            _gui.SetSavings(list);
            throw new NotImplementedException();
        }

        private void GetNegativeSavingsDate()
        {


        }

        private void TransactionHarmful(Account account, DateTime dateTime)
        {
            string warning;
            warning = String.Format("Am {2: yyyy MM dd} wird der Account: {0} unter das Monatliche Sparziel von {1:0,0.##} fallen!", account.AccountName, account.MonthlySavingsGoal, dateTime);

            _gui.ThrowAccountWarning(warning);
        }

        private void Initialize()
        {
            foreach (var account in _backend.GetAccountNames())
            {
                _gui.AddAccount(account);
            }
        }


        private void OnClose()
        {
            _backend.Persist();
        }

        private void DeleteTransaction()
        {
            int tId;
            string tName = _gui.GetSelectedTransaction();
            string[] strings = tName.Split(' ');
            Int32.TryParse(strings[0], out tId);
            _backend.RemoveTransaction(tId);
        }

        private void DeleteAccount()
        {
            string tName = _gui.GetSelectedAccount();
            _backend.RemoveAccount(tName);
        }

        private void NewAccountSelected(string tName)
        {
            LinkedList<Transaction> list = _backend.GetTransacctionsFromAccountName(tName);
            _gui.SetLbTransactions(list);
            Account? acc = _backend.GetAccountFromAccountName(tName);
            string details = string.Empty;
            if (acc != null)
            {
                details = acc.ToString();
            }
            _gui.SetAccountDetails(details);
        }

        private int GetNewTransactionId()
        {
            return _backend.GetNewTransactionId();
        }

        private void AddNewIncome()
        {
            _backend.AddNewIncome();

            NewAccountSelected(_gui.GetSelectedAccount());
        }

        private void AddNewTransaction()
        {
            _backend.AddNewTransaction();
            NewAccountSelected(_gui.GetSelectedAccount());
        }

        private void AddNewAccount()
        {
            _backend.AddNewAccount();
        }

        public Account GetNewAccount()
        {
            return _gui.GetNewAccount();
        }

        public Transaction GetNewTransaction()
        {
            return _gui.GetNewTransaction();
        }
        public Transaction GetNewIncome()
        {
            return _gui.GetNewIncome();
        }
    }
}
