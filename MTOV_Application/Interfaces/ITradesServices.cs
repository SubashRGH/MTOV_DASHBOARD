namespace MTOV_Application.Services
{
    using MTOV_DTO.Trades;
    using System.Threading.Tasks;

    /// <summary>
    /// Trades Services
    /// </summary>
    public interface ITradesServices
    {
        Task<List<OpenTradesResDto>> GetAllOpenTradesByAccountIdAsync(string tp, string accountId);
    }
}