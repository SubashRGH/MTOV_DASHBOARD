namespace MTOV_Domain.Models
{
    public class AccountModel
    {
        public string AccountId { get; set; }
        public string TradingPlatform { get; set; }
        public decimal Balance { get; set; }
        public decimal Equity { get; set; }
        public decimal MarginLevel { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Status { get; set; }
    }
}
