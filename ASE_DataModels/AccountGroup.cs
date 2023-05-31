using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_DataModels.Utils;

namespace ASE_DataModels
{
    public class AccountGroup
    {
        public Dictionary<string, Account> _accounts { get; init; } = new Dictionary<string, Account>();
        public Dictionary<int, Transaction> _transactions { get; init; } = new Dictionary<int, Transaction>();
    }
}
