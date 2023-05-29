using ASE_Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Core.Interfaces
{
    public interface IFrontend
    {
        public Account GetNewAccount();
        public Transaction GetNewTransaction();
        public Transaction GetNewIncome();
    }
}
