using ASE_Core.Data;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Globalization;

namespace Bank_GUI
{
    public partial class BankingGUI : Form
    {
        public event Action? NewAccountBtnPressedEvt;
        public event Action? NewTransactionBtnPressedEvt;
        public event Action? NewIncomeBtnPressedEvt;
        public event Action? AccountDeleteBtnPressedEvt;
        public event Action? TransactionDeleteBtnPressedEvt;
        public event Action? FormClosingEvt;
        public event Action? AllAccountSavingsEvt;
        public event Action? CurrentAccountSavingsEvt;
        public event Action<DateTime>? FutureAccountSavingsEvt;
        public event Action<string>? NewAccountSelected;
        public event Func<int>? GetNewTransactionId;

        public BankingGUI()
        {
            InitializeComponent();
            FrontController controller = new FrontController(this);
            Refresh();
        }

        private void cbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = cbBank.Text;
            NewAccountSelected?.Invoke(tmp);
            Refresh();

        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            NewAccountBtnPressedEvt?.Invoke();
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewTransactionBtnPressedEvt?.Invoke();
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewIncomeBtnPressedEvt?.Invoke();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AllAccountSavingsEvt?.Invoke();
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AccountDeleteBtnPressedEvt?.Invoke();
            Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TransactionDeleteBtnPressedEvt?.Invoke();
            Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CurrentAccountSavingsEvt?.Invoke();
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParseExact(Interaction.InputBox("Bitte geben sie ein bis zu welchem Datum dieses Konto geprüft werden soll:", "Konto prüfen", "2001 10 20"), "yyyy MM dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            FutureAccountSavingsEvt?.Invoke(date);
            Refresh();

        }

        private void BankingGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClosingEvt?.Invoke();
            Refresh();
        }

        public Account GetNewAccount()
        {
            decimal nAccountBalance = 0;
            decimal nAccountInterest = 0;
            decimal nSavingsGoal = -1;

            String nAccountName = Interaction.InputBox("Bitte geben sie den Kontonamen ein:", "Neus Konto erstellen", "Neues Konto");
            Decimal.TryParse(Interaction.InputBox("Bitte geben sie den initialen Kontostand ein:", "Neus Konto erstellen", "0,00"), out nAccountBalance);
            Decimal.TryParse(Interaction.InputBox("Bitte geben sie die Zinsrate ein (in%):", "Neus Konto erstellen", "0,00"), out nAccountInterest);
            DialogResult result = MessageBox.Show("Wollen sie für dieses Konto ein monatliches Sparziel festlegen?", "Neus Konto erstellen", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Decimal.TryParse(Interaction.InputBox("Bitte geben sie ihr monatliches Sparziel:", "Neus Konto erstellen", "0,00"), out nSavingsGoal);
            }
            Account account = new Account(nAccountName, nAccountBalance, nAccountInterest, nSavingsGoal);
            cbBank.Items.Add(account.AccountName);
            Refresh();
            return account;
        }

        public bool GetNewTransactionType()
        {
            DialogResult result = MessageBox.Show("Ist die Transaktion einmalig?", "Neue Transaktion erstellen", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Transaction GetNewTransaction()
        {
            decimal nTransactionAmount;
            DateTime nTransactionDate;
            int nTransactionIntervall;
            int nTransactionId;

            Decimal.TryParse(Interaction.InputBox("Bitte geben sie den Überweisungsbetrag ein:", "Neu Transaktion erstellen", "0,00"), out nTransactionAmount);
            DateTime.TryParseExact(Interaction.InputBox("Bitte geben sie das Überweisungsdatum ein:", "Neu Transaktion erstellen", "2001 10 20"), "yyyy MM dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out nTransactionDate);
            if (!GetNewTransactionType())
            {
                Int32.TryParse(Interaction.InputBox("Bitte geben sie den Überweisungsintervall ein(in Monaten):", "Neu Transaktion erstellen", "0"), out nTransactionIntervall);

            }
            else
            {
                nTransactionIntervall = 0;
            }
            String nAccountFrom = Interaction.InputBox("Bitte geben sie den Kontonamen ein, von dem das Geld abgehoben werden soll:", "Neu Transaktion erstellen", "Kontoname");
            String nAccountTo = Interaction.InputBox("Bitte geben sie den Kontonamen ein, auf den das Geld überwiesen werden soll:", "Neu Transaktion erstellen", "Kontoname");

            nTransactionId = GetNewTransactionId?.Invoke() ?? -1;

            return new Transaction(nTransactionId, nTransactionAmount, nTransactionDate, nTransactionIntervall, nAccountFrom, nAccountTo);
        }

        public Transaction GetNewIncome()
        {
            decimal nTransactionAmount;
            int nTransactionIntervall;
            int nTransactionId;
            DateTime nTransasctionDate;
            DateTime nTransasctionDateDay;

            Decimal.TryParse(Interaction.InputBox("Bitte geben sie ihr Netto-Gehalt ein:", "Neues Gehalt", "0,00"), out nTransactionAmount);
            DateTime.TryParseExact(Interaction.InputBox("Bitte geben sie das ÜberweisungsTag ein:", "Neu Transaktion erstellen", "03"), "dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out nTransasctionDateDay);
            nTransasctionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, nTransasctionDateDay.Day);

            DialogResult result = MessageBox.Show("Bekommen sie 13 Monatsgehälter?", "Neues Gehalt", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                nTransactionIntervall = -1;
            }
            else
            {

                nTransactionIntervall = 1;
            }
            String nAccountTo = Interaction.InputBox("Bitte geben sie den Kontonamen ein, auf den das Geld überwiesen werden soll:", "Neues Gehalt", "Kontoname");

            nTransactionId = GetNewTransactionId?.Invoke() ?? -1;

            return new Transaction(nTransactionId, nTransactionAmount, nTransasctionDate, nTransactionIntervall, String.Empty, nAccountTo);
        }

        public void SetLbTransactions(LinkedList<Transaction> list)
        {
            lbTransactions.Items.Clear();
            foreach (Transaction t in list)
            {
                lbTransactions.Items.Add(t.ToString());
            }
            Refresh();
        }

        public void SetAccountDetails(string details)
        {
            tBAccout.Text = details;
            Refresh();
        }

        public void SetSavings(LinkedList<string> list)
        {
            lbSavings.Items.Clear();
            foreach (var s in list)
            {
                lbSavings.Items.Add(s);
            }
            Refresh();
        }

        public string GetSelectedAccount()
        {
            return cbBank.Text;
        }

        public string GetSelectedTransaction()
        {
            if (lbTransactions.SelectedItem is not null)
            {
                string? tmp = lbTransactions.SelectedItem.ToString();

                if (!String.IsNullOrWhiteSpace(tmp))
                {
                    return tmp;
                }
            }
            return String.Empty;
        }
        public void AddAccount(string tName)
        {
            cbBank.Items.Add(tName);
            Refresh();
        }


        public void ThrowAccountWarning(string tPrompt)
        {
            Interaction.MsgBox(tPrompt, MsgBoxStyle.OkOnly);
        }
    }

}