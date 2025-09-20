namespace MTOV_Domain.Interface
{
    using MTOV_Domain.Models;

    public interface ITradesRepository
    {
        /// <summary>
        /// Gets all open trades 
        /// </summary>
        /// <returns></returns>
        Task<List<TradesModel>> GetAllOpenTradesAsync();

        /// <summary>
        /// Get Open trades by account id
        /// </summary>
        /// <param name="tp">trading platform: MT5, MT4, cTrader</param>
        /// <param name="accountId">account id: string</param>
        /// <returns></returns>
        Task<List<TradesModel>> GetOpenTradeByAccountIdAsync(string tp, string accountId);
    }
}