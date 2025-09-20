namespace MTOV_Infrastructure.Repositiries
{
    using MTOV_Domain.Interface;
    using MTOV_Domain.Models;

    public class TradesRepository : ITradesRepository
    {
        /// <summary>
        /// Get all open trades
        /// </summary>
        /// <returns></returns>
        public async Task<List<TradesModel>> GetAllOpenTradesAsync()
        {
            // Realtime data from dbcontext (with await)
            return MockData.GetOpenTrades();
        }

        /// <summary>
        ///  Get open trades by account id
        /// </summary>
        /// <param name="tp">trading platform</param>
        /// <param name="accountId">account id: string</param>
        /// <returns></returns>
        public async Task<List<TradesModel>> GetOpenTradeByAccountIdAsync(string tp, string accountId)
        {
            // Realtime data from dbcontext (with await)
            // note: tp directly filtered, but we can filter by account only (account has tp type by default)
            return MockData.GetOpenTrades().Where(t => t.TradingPlatform==tp && t.AccountId == accountId).ToList();
        }
    }
}