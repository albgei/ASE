using ASE_DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Interfaces
{
    public interface IPersist
    {
        public void InitializePersister(IDataManager data);
        public void Load(Dictionary<string, Account> accounts, Dictionary<int, Transaction> transactions);
        public void Persist();
    }
}
