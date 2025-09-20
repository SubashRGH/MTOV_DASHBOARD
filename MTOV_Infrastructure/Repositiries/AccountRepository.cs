namespace MTOV_Infrastructure.Repositiries
{
    using MTOV_Domain.Interface;
    using MTOV_Domain.Models;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Account Repository
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Gets accounts by account id
        /// </summary>
        /// <param name="tp">trading platform</param>
        /// <param name="accountId">account id: string</param>
        /// <returns>account detail</returns>
        public async Task<AccountModel?> GetByIdAsync(string tp, string accountId)
        {

            // actual work flow to get the data from db context with await
            return MockData.GetAccounts().FirstOrDefault(t => t.TradingPlatform==tp && t.AccountId == accountId) ?? null;
        }
    }
}