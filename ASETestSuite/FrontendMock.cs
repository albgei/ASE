using ASE_Core.Data;
using ASE_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASETestSuite
{
    internal class FrontendMock : IFrontend
    {
        BackendInterfacer _interfacer;
        public FrontendMock()
        {
            _interfacer = new BackendInterfacer(this);
        }
        public Account GetNewAccount()
        {
            return new Account("Test", 100, (decimal)0.01, 10);
        }

        public Transaction GetNewIncome()
        {
            return new Transaction(200, 200, DateTime.Now, -1, String.Empty, "Test");
        }

        public Transaction GetNewTransaction()
        {
            return new Transaction(300, 200, DateTime.Now, 1, "Test", String.Empty);
        }

        public BackendInterfacer GetInterfacer()
        {
            return _interfacer;
        }
    }
}
