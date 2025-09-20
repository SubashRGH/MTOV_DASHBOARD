namespace MTOV_Application.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using MTOV_DTO.Account;
    using MTOV_Domain.Interface;
    using System.Threading.Tasks;


    /// <summary>
    /// Trading Account Services
    /// </summary>
    /// <param name="_accountRepository"></param>
    /// <param name="_logger"></param>
    /// <param name="_mapper"></param>
    public class AccountServices(IAccountRepository _accountRepository,
                                    ILogger<AccountServices> _logger,
                                    IMapper _mapper) : IAccountServices
    {

        public async Task<AccountDetailResDto> GetAccountDetailAsync(string tp, string accountId)
        {
            var result = await _accountRepository.GetByIdAsync(tp, accountId);

            return _mapper.Map<AccountDetailResDto>(result);
        }
    }
}