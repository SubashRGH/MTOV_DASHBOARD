namespace MTOV_Application.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using MTOV_DTO.Trades;
    using MTOV_Domain.Interface;

    /// <summary>
    /// Trades Services
    /// </summary>
    /// <param name="_tradesRepository"></param>
    /// <param name="_logger"></param>
    /// <param name="_mapper"></param>
    public class TradesServices(ITradesRepository _tradesRepository,
                                    ILogger<AccountServices> _logger,
                                    IMapper _mapper) : ITradesServices
    {
        public async Task<List<OpenTradesResDto>> GetAllOpenTradesByAccountIdAsync(string tp, string accountId)
        {
            var result = await _tradesRepository.GetOpenTradeByAccountIdAsync(tp, accountId);

            return _mapper.Map<List<OpenTradesResDto>>(result);
        }
    }
}