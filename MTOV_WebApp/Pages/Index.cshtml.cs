namespace MTOV_WebApp.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MTOV_WebApp.Controller.Interfaces;
    using MTOV_WebApp.Model.Dashboard;

    /// <summary>
    /// Index view model
    /// </summary>
    /// <param name="_dashboardService"></param>
    /// <param name="_logger"></param>
    public class IndexModel(IDashboardService _dashboardService, ILogger<IndexModel> _logger) : PageModel
    {
        [BindProperty]
        public DashboardViewModel DashboardViewModel { get; set; }

        [BindProperty]
        public string AccountId { get; set; }

        public async Task OnGetAsync(string accountId = "12345")
        {
            try
            {
                _logger.LogInformation($"Loading dashboard for account: {accountId}");

                DashboardViewModel = await _dashboardService.GetDashboardDataAsync(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading dashboard for account: {accountId}");

                // Return error view or redirect to error page
                TempData["ErrorMessage"] = "Unable to load dashboard data. Please try again later.";
                //return View("Error");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(AccountId))
            {
                ModelState.AddModelError("AccountId", "Account ID is required.");
                return Page();
            }

            try
            {
                DashboardViewModel = await _dashboardService.GetDashboardDataAsync(AccountId);
                return Page(); // Re-render the page with updated model
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error loading account: {ex.Message}");
                return Page();
            }
        }
    }
}