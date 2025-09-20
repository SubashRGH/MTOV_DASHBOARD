using MTOV_WebApp.Model.Dashboard;

namespace MTOV_WebApp.Controller.Interfaces
{
    /// <summary>
    /// Dashboard Api service
    /// </summary>
    public interface IDashboardApiService
    {
        Task<AccountDetails?> GetAccountDetailsAsync(string accountId);

        Task<List<Trade>> GetOpenTradesAsync(string accountId);
    }
}