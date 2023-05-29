using ASE_Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Data
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionInterval { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }

        public Transaction(int id, decimal amount, DateTime transactionDate, int transactionInterval, string fromAccount, string toAccount)
        {
            TransactionId = id;
            Amount = amount;
            TransactionDate = transactionDate;
            TransactionInterval = transactionInterval;
            FromAccount = StringUtils.FirstCharToUpper(fromAccount);
            ToAccount = StringUtils.FirstCharToUpper(toAccount);
        }

        public override string ToString()
        {
            if (TransactionInterval == 0)
            {
                return string.Format("ID: {2} Am: {0}, Betrag: {1:0,0.##}", TransactionDate.ToString("yyyy MM dd"), Amount, TransactionId);
            }
            else if (TransactionInterval == -1)
            {
                return string.Format("ID: {3} Am: {0}, monatlich +1, Betrag: {2:0,0.##}", TransactionDate.ToString("yyyy MM dd"), TransactionInterval, Amount, TransactionId);

            }
            else if (TransactionInterval == 1)
            {
                return string.Format("ID: {3} Am: {0}, monatlich, Betrag: {2:0,0.##}", TransactionDate.ToString("yyyy MM dd"), TransactionInterval, Amount, TransactionId);

            }
            else
            {
                return string.Format("ID: {3} Am: {0}, alle {1} Monate, Betrag: {2:0,0.##}", TransactionDate.ToString("yyyy MM dd"), TransactionInterval, Amount, TransactionId);
            }
        }
    }

}
