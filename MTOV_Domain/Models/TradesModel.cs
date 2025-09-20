namespace MTOV_Domain.Models
{
    public class TradesModel
    {
        public string AccountId { get; set; }
        public string TradingPlatform { get; set; }
        public string Ticket { get; set; }
        public string Symbol { get; set; }
        public decimal Volume { get; set; }
        public decimal Profit { get; set; }
    }
}
