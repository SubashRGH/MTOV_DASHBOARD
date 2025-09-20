namespace MTOV_WebApp.Controller.Services
{
    using MTOV_AppSettings;
    using MTOV_VModel.Common;
    using MTOV_WebApp.Controller.Interfaces;
    using MTOV_WebApp.Model.Dashboard;
    using Newtonsoft.Json;
    using JsonException = Newtonsoft.Json.JsonException;

    /// <summary>
    /// Dashboard api serviec implementtion
    /// </summary>
    public class DashboardApiService : IDashboardApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DashboardApiService> _logger;
        private readonly WebAppProperty _property;

        public DashboardApiService(HttpClient httpClient,
                                   ILogger<DashboardApiService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _property = new WebAppProperty();
        }

        /// <summary>
        /// Get account detail by account id (calling api)
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>

        public async Task<AccountDetails?> GetAccountDetailsAsync(string accountId)
        {
            try
            {
                _logger.LogInformation($"Fetching account details for account: {accountId}");

                var response = await _httpClient.GetAsync($"{_property?.ApiConfigProp?.Account?.AccountDetail ?? ""}{accountId}");

                var responseData = await APIResponseHelper.ReadHttpResponseMessage(response);

                if (responseData?.StatusCode == 100)
                {
                    var accountDetails = JsonConvert.DeserializeObject<AccountDetails>(JsonConvert.SerializeObject(responseData.Data));
                    _logger.LogInformation($"Successfully retrieved account details for account: {accountId}");
                    return accountDetails;
                }
                else
                {
                    _logger.LogWarning($"Failed to retrieve account details. Status: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Network error while fetching account details for account: {accountId}");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"JSON parsing error while processing account details for account: {accountId}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while fetching account details for account: {accountId}");
                return null;
            }
        }

        /// <summary>
        /// Get all open trades by account id (calling api)
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<List<Trade>> GetOpenTradesAsync(string accountId)
        {
            try
            {
                _logger.LogInformation($"Fetching open trades for account: {accountId}");

                var response = await _httpClient.GetAsync(_property.ApiConfigProp.Account.OpenTrades.Replace("{accountId}", accountId));

                var responseData = await APIResponseHelper.ReadHttpResponseMessage(response);

                if (responseData?.StatusCode == 100)
                {
                    var trades = JsonConvert.DeserializeObject<List<Trade>>(JsonConvert.SerializeObject(responseData.Data));
                    _logger.LogInformation($"Successfully retrieved {trades?.Count ?? 0} trades for account: {accountId}");
                    return trades;
                }
                else
                {
                    _logger.LogWarning($"Failed to retrieve trades. Status: {response.StatusCode}");
                    return [];
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Network error while fetching trades for account: {accountId}");
                return [];
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"JSON parsing error while processing trades for account: {accountId}");
                return [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while fetching trades for account: {accountId}");
                return [];
            }
        }
    }
}