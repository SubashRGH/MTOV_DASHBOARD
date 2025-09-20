// Dashboard view model
namespace MTOV_WebApp.Model.Dashboard
{
    public class AccountDetails
    {
        public string AccountId { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal Equity { get; set; }
        public decimal MarginLevel { get; set; }
        public DateTime LastLogin { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class Trade
    {
        public string Ticket { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Volume { get; set; }
        public decimal Profit { get; set; }
    }

    public class DashboardViewModel
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal Equity { get; set; }
        public decimal MarginLevel { get; set; }
        public int OpenTradesCount { get; set; }
        public DateTime LastLogin { get; set; }
        public string AccountStatus { get; set; } = string.Empty;
        public List<Trade> Trades { get; set; } = new List<Trade>();
        public decimal TotalProfit => Trades.Sum(t => t.Profit);
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}