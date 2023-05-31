using ASE_DataModels;

namespace ASE_Interfaces
{
    public interface IDataManager
    {
        public Dictionary<string, Account> GetAccounts();
        public Dictionary<int, Transaction> GetTransactions();
    }
}