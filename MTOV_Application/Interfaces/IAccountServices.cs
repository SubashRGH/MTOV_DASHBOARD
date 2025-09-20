namespace MTOV_Application.Services
{
    using MTOV_DTO.Account;
    using System.Threading.Tasks;

    /// <summary>
    /// Account interface
    /// </summary>
    public interface IAccountServices
    {
        Task<AccountDetailResDto> GetAccountDetailAsync(string tp, string accountId);
    }
}