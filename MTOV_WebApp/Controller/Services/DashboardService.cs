namespace MTOV_WebApp.Controller.Services
{
    using MTOV_WebApp.Controller.Interfaces;
    using MTOV_WebApp.Model.Dashboard;

    /// <summary>
    /// Dashboard service
    /// </summary>
    /// <param name="_dashboardApiService"></param>
    /// <param name="_logger"></param>
    public class DashboardService(IDashboardApiService _dashboardApiService, ILogger<DashboardService> _logger) : IDashboardService
    {
        public async Task<DashboardViewModel> GetDashboardDataAsync(string accountId)
        {
            var dashboard = new DashboardViewModel
            {
                AccountNumber = accountId
            };

            try
            {
                _logger.LogInformation($"Building dashboard data for account: {accountId}");

                // Fetch account details and trades concurrently
                var accountDetailsTask = _dashboardApiService.GetAccountDetailsAsync(accountId);
                var tradesTask = _dashboardApiService.GetOpenTradesAsync(accountId);

                await Task.WhenAll(accountDetailsTask, tradesTask);

                var accountDetails = await accountDetailsTask;
                var trades = await tradesTask;

                if (accountDetails != null)
                {
                    dashboard.Balance = accountDetails.Balance;
                    dashboard.Equity = accountDetails.Equity;
                    dashboard.MarginLevel = accountDetails.MarginLevel;
                    dashboard.LastLogin = accountDetails.LastLogin;
                    dashboard.AccountStatus = accountDetails.Status;
                }
                else
                {
                    _logger.LogWarning($"Failed to retrieve account details for account: {accountId}");
                    dashboard.HasError = true;
                    dashboard.ErrorMessage = "Unable to retrieve account details. Please try again later.";
                }

                dashboard.Trades = trades;
                dashboard.OpenTradesCount = trades.Count;

                _logger.LogInformation($"Successfully built dashboard data for account: {accountId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error building dashboard data for account: {accountId}");
                dashboard.HasError = true;
                dashboard.ErrorMessage = "An unexpected error occurred while loading dashboard data.";
            }

            return dashboard;
        }
    }
}