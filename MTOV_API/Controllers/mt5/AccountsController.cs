namespace MTOV_API.Controllers.mt5
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MTOV_Application.Services;
    using MTOV_DTO.Account;
    using MTOV_Utility;
    using RECRM_API.Controllers;
    using RECRM_Application.Constants;

    /// <summary>
    /// Account API controller
    /// </summary>
    /// <param name="_logger">middleware logger</param>
    /// <param name="_accountServices">account service dependency</param>
    /// <param name="_tradesServices">Trades service depedency</param>
    [Route("api/mt5/[controller]")]
    [ApiController]
    public class AccountsController(ILogger<AccountsController> _logger, IAccountServices _accountServices,
        ITradesServices _tradesServices) : BaseController<AccountsController>(_logger)
    {

        /// <summary>
        /// Get account detail by account id
        /// </summary>
        /// <param name="accountId">account id: string</param>
        /// <returns>account detail</returns>
        [HttpGet]
        [Authorize]
        [Route("{accountId}")]
        [ProducesResponseType<AccountDetailResDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountDetailById(string accountId)
        {
            var result = await _accountServices.GetAccountDetailAsync("MT5", accountId);
            response = ResponseFormatter.FormatSuccessResponse(MessagesConstant.GET_ACCOUNT_DETAIL_SUCCESS, result);
            return Ok(response);
        }

        /// <summary>
        /// Get all opened trades by account Id
        /// </summary>
        /// <param name="accountId">account id: string</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{accountId}/trades")]
        [ProducesResponseType<AccountDetailResDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOpenTrades(string accountId)
        {
            var result = await _tradesServices.GetAllOpenTradesByAccountIdAsync("MT5", accountId);
            response = ResponseFormatter.FormatSuccessResponse(MessagesConstant.GET_OPEN_TRADES_LIST_SUCCESS, result);
            return Ok(response);
        }
    }
}