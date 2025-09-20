namespace MTOV_DTO.Account
{
    public class AccountDetailResDto
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public decimal Equity { get; set; }
        public decimal MarginLevel { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Status { get; set; }
    }
}