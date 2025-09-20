using MTOV_WebApp.Model.Dashboard;

namespace MTOV_WebApp.Controller.Interfaces
{
    /// <summary>
    /// Dashboard service
    /// </summary>
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(string accountId);
    }
}