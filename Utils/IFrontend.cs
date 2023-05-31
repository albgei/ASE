using ASE_DataModels;
using ASE_DataModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Interfaces
{
    public interface IFrontend
    {
        public Account GetNewAccount();
        public Transaction GetNewTransaction();
        public Transaction GetNewIncome();
    }
}
