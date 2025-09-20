namespace MTOV_Domain.Interface
{
    using MTOV_Domain.Models;

    public interface IAccountRepository
    {
        /// <summary>
        /// Get account detail by id
        /// </summary>
        /// <param name="tp">Trading Platform : MT5, MT5, cTrader</param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<AccountModel> GetByIdAsync(string tp, string accountId);
    }
}