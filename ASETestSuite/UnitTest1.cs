using ASE_Core;
using ASE_Core.Data;
using ASE_DataModels.Utils;
using ASE_DataModels;
using NUnit.Framework.Internal;

namespace ASETestSuite
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StringUtilsTest()
        {
            string testToUpper = "hallo Welt!";
            string testToUpperLSG = "Hallo Welt!";
            string testToUpperResult = StringUtils.FirstCharToUpper(testToUpper);
            Assert.That(testToUpperResult, Is.EqualTo(testToUpperLSG));
        }

        [Test]
        public void AddAccount()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();

            Account account = new Account("Test", 100, (decimal)0.01, 10);

            _interfacer.AddNewAccount();

            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Not.Null);
            Assert.That(test.ToString(), Is.EqualTo(account.ToString()));
        }

        [Test]
        public void AddTransaction()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            Transaction transactionactual = new Transaction(300, 200, DateTime.Now, 1, "Test", String.Empty);
            Transaction transaction;

            _interfacer.AddNewAccount();
            _interfacer.AddNewTransaction();

            transaction = _interfacer.GetTransacctionsFromAccountName("Test").First();

            Assert.That(transaction, Is.Not.Null);
            Assert.That(transactionactual.ToString(), Is.EqualTo(transaction.ToString()));
        }

        [Test]
        public void AddIncome()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            Transaction incomeactual = new Transaction(200, 200, DateTime.Now, -1, String.Empty, "Test");
            Transaction income;

            _interfacer.AddNewAccount();
            _interfacer.AddNewIncome();

            income = _interfacer.GetTransacctionsFromAccountName("Test").First();
            Assert.That(incomeactual.ToString(), Is.EqualTo(income.ToString()));
        }

        [Test]
        public void RemoveAccount()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();

            Account account = new Account("Test", 100, (decimal)0.01, 10);

            _interfacer.AddNewAccount();
            _interfacer.RemoveAccount("Test");

            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Null);
        }

        [Test]
        public void RemoveTransaction()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            LinkedList<Transaction> transaction;

            _interfacer.AddNewAccount();
            _interfacer.AddNewTransaction();

            _interfacer.RemoveTransaction(300);

            transaction = _interfacer.GetTransacctionsFromAccountName("Test");
            Assert.That(transaction.Count, Is.EqualTo(0));
        }

        [Test]
        public void IsOverSavings()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            DateTime date = DateTime.Now;
            date = date.AddMonths(1);

            _interfacer.AddNewAccount();
            _interfacer.AddNewIncome();

            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Not.Null);
            Assert.That(_interfacer.IsAccountAboveSavingThreashhold(test, date), Is.EqualTo(true));
        }

        [Test]
        public void IsUnderSavings()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            DateTime date = DateTime.Now;
            date = date.AddMonths(1);

            _interfacer.AddNewAccount();
            _interfacer.AddNewTransaction();

            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Not.Null);
            Assert.That(_interfacer.IsAccountAboveSavingThreashhold(test, date), Is.EqualTo(false));
        }

        [Test]
        public void IsOverSavingsNotime()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            DateTime date = DateTime.Now;

            _interfacer.AddNewAccount();
            _interfacer.AddNewTransaction();

            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Not.Null);
            Assert.That(_interfacer.IsAccountAboveSavingThreashhold(test, date), Is.EqualTo(true));
        }

        [Test]
        public void GetBalanceAll()
        {
            FrontendMock _frontend = new FrontendMock();
            BackendInterfacer _interfacer = _frontend.GetInterfacer();
            DateTime date = DateTime.Now;
            date = date.AddMonths(1);
            decimal balanceactual = 300;

            _interfacer.AddNewAccount();
            _interfacer.AddNewIncome();


            decimal balance = _interfacer.GetTotalAccountBalance(date);
            Account? test = _interfacer.GetAccountFromAccountName("Test");
            Assert.That(test, Is.Not.Null);
            Assert.That(balance, Is.EqualTo(balanceactual));
        }
    }
}