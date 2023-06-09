﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ASE_Interfaces;
using ASE_DataModels;
using ASE_DataModels.Utils;

namespace ASE_Persistence
{
    public class XMLPersister : IPersist
    {
        private IDataManager _data = default!;

        public void InitializePersister(IDataManager data)
        {
            _data = data;
        }

        public void Persist()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            settings.CloseOutput = true;
            settings.NewLineOnAttributes = true;
            string path = "Persistence/Persisted/Banking_Info_Persistence.xml";
            DirectoryInfo? info = Directory.GetParent(path);
            if (info is not null && !info.Exists)
            {
                info.Create();
            }
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Persistence");
                writer.WriteStartElement("Accounts");
                foreach (var account in _data.GetAccounts())
                {
                    writer.WriteStartElement("Account");
                    writer.WriteAttributeString("AccountName", account.Value.AccountName.ToString());
                    writer.WriteAttributeString("Interest", account.Value.Interest.ToString());
                    writer.WriteAttributeString("SavingsGoal", account.Value.MonthlySavingsGoal.ToString());
                    writer.WriteAttributeString("Slado", account.Value.AccountBalance.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("Transactions");
                foreach (var transaction in _data.GetTransactions())
                {
                    writer.WriteStartElement("Transaction");
                    writer.WriteAttributeString("TransactionID", transaction.Value.TransactionId.ToString());
                    writer.WriteAttributeString("TransactionDate", transaction.Value.TransactionDate.ToString("yyyy MM dd"));
                    writer.WriteAttributeString("TransactionInterval", transaction.Value.TransactionInterval.ToString());
                    writer.WriteAttributeString("TransactionAmount", transaction.Value.Amount.ToString());
                    writer.WriteAttributeString("TransactionWithdrawlAccount", transaction.Value.FromAccount);
                    writer.WriteAttributeString("TransactionTargetAccount", transaction.Value.ToAccount);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();

            }
        }

        public void Load(Dictionary<string, Account> accounts, Dictionary<int, Transaction> transactions)
        {
            string path = "Persistence/Persisted/Banking_Info_Persistence.xml";
            DirectoryInfo? info = Directory.GetParent(path);
            if (info is not null)
            {
                foreach (var file in info.GetFiles())
                {
                    if (file.Name.Equals("Banking_Info_Persistence.xml"))
                    {
                        LoadFromFile(accounts, transactions, path);
                        return;
                    }
                }

                LoadCleanStart(accounts, transactions);
            }
        }

        internal void LoadFromFile(Dictionary<string, Account> accounts, Dictionary<int, Transaction> transactions, string path)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("Accounts"))
                            {
                                CreateAccount(reader.ReadSubtree(), accounts);
                            }
                            else if (reader.Name.Equals("Transactions"))
                            {
                                CreateTransaction(reader.ReadSubtree(), transactions);
                            }
                            break;
                        case XmlNodeType.Attribute:
                            break;
                        case XmlNodeType.EndElement:
                        default:
                            break;
                    }
                }
            }
        }

        internal void LoadCleanStart(Dictionary<string, Account> accounts, Dictionary<int, Transaction> transactions)
        {
            accounts.Add("Init", new Account("init", 0, 0, 0));
            transactions.Add(0, new Transaction(0, 0, DateTime.MinValue, 0, string.Empty, "Init"));

        }

        private void CreateAccount(XmlReader reader, Dictionary<string, Account> accounts)
        {
            string AccountName = string.Empty;
            decimal Intrest = 0;
            decimal SavingsGoal = 0;
            decimal Slado = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    AccountName = StringUtils.FirstCharToUpper(reader.GetAttribute("AccountName") ?? string.Empty);
                    decimal.TryParse(reader.GetAttribute("Interest"), out Intrest);
                    decimal.TryParse(reader.GetAttribute("SavingsGoal"), out SavingsGoal);
                    decimal.TryParse(reader.GetAttribute("Slado"), out Slado);

                }
            }

            Account account = new Account(AccountName, Slado, Intrest, SavingsGoal);
            accounts.Add(AccountName, account);
        }

        internal void CreateTransaction(XmlReader reader, Dictionary<int, Transaction> transactions)
        {
            int TransactionID = 0;
            DateTime TransactionDate = DateTime.MinValue;
            int TransactionInterval = 0;
            decimal TransactionAmount = 0;
            string TransactionFrom = string.Empty;
            string TransactionTo = string.Empty;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    int.TryParse(reader.GetAttribute("TransactionID"), out TransactionID);
                    DateTime.TryParseExact(reader.GetAttribute("TransactionDate"), "yyyy MM dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out TransactionDate);
                    int.TryParse(reader.GetAttribute("TransactionInterval"), out TransactionInterval);
                    decimal.TryParse(reader.GetAttribute("TransactionAmount"), out TransactionAmount);
                    TransactionFrom = StringUtils.FirstCharToUpper(reader.GetAttribute("TransactionWithdrawlAccount") ?? string.Empty);
                    TransactionTo = StringUtils.FirstCharToUpper(reader.GetAttribute("TransactionTargetAccount") ?? string.Empty);
                }
            }

            Transaction transaction = new Transaction(TransactionID, TransactionAmount, TransactionDate, TransactionInterval, TransactionFrom, TransactionTo);
            transactions.Add(TransactionID, transaction);

        }
    }
}
