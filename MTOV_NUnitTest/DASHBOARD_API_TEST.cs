namespace MTOV_NUnitTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using Moq.Protected;
    using MTOV_VModel.Common;
    using MTOV_WebApp.Controller.Services;
    using MTOV_WebApp.Model.Dashboard;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using Xunit;

    /// <summary>
    /// Testing
    /// </summary>
    public class DASHBOARD_API_TEST : IDisposable
    {
        private readonly Mock<ILogger<DashboardApiService>> _mockLogger;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly DashboardApiService _apiService;
        private string _token = string.Empty;

        private readonly string _baseUrl = "https://localhost:44389/api";

        public DASHBOARD_API_TEST()
        {
            _mockLogger = new Mock<ILogger<DashboardApiService>>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(_baseUrl),
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            _apiService = new DashboardApiService(_httpClient, _mockLogger.Object);
        }

        [Test]
        public async Task GetAccountDetailsAsync_SuccessfulResponse_ReturnsAccountDetails()
        {
            // Arrange
            var accountId = "12345";
            var expectedAccount = new Result
            {
                StatusCode = 100,
                Message = "",
                Data = new AccountDetails
                {
                    AccountId = accountId,
                    Balance = 10000.50m,
                    Equity = 9700.25m,
                    MarginLevel = 325.67m,
                    LastLogin = DateTime.Parse("2025-07-21T14:10:00Z"),
                    Status = "Active"
                }
            };

            var jsonContent = JsonConvert.SerializeObject(expectedAccount);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _apiService.GetAccountDetailsAsync(accountId);

            var testData = expectedAccount.Data as AccountDetails;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData.AccountId, result.AccountId);
            Assert.Equal(testData.Balance, result.Balance);
            Assert.Equal(testData.Equity, result.Equity);
            Assert.Equal(testData.MarginLevel, result.MarginLevel);
            Assert.Equal(testData.Status, result.Status);
        }

        [Test]
        public async Task GetAccountDetailsAsync_HttpRequestException_ReturnsNull()
        {
            // Arrange
            var accountId = "12345";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            var result = await _apiService.GetAccountDetailsAsync(accountId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetAccountDetailsAsync_NotFoundResponse_ReturnsNull()
        {
            // Arrange
            var accountId = "12345";
            var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _apiService.GetAccountDetailsAsync(accountId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetOpenTradesAsync_SuccessfulResponse_ReturnsTradesList()
        {
            // Arrange
            var accountId = "12345";
            var expectedTrades = new Result
            {
                StatusCode = 100,
                Message = "",
                Data = new List<Trade>
            {
                new() { Ticket = "10001", Symbol = "EURUSD", Volume = 1.0m, Profit = 100.20m },
                new() { Ticket = "10002", Symbol = "GBPUSD", Volume = 0.5m, Profit = -45.10m }
            }
            };

            var jsonContent = JsonConvert.SerializeObject(expectedTrades);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _apiService.GetOpenTradesAsync(accountId);

            var testData = expectedTrades.Data as List<Trade>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData.Count, result.Count);
            Assert.Equal(testData[0].Ticket, result[0].Ticket);
            Assert.Equal(testData[1].Symbol, result[1].Symbol);
        }

        [Test]
        public async Task GetOpenTradesAsync_HttpRequestException_ReturnsEmptyList()
        {
            // Arrange
            var accountId = "12345";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            var result = await _apiService.GetOpenTradesAsync(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Test]
        public async Task GetOpenTradesAsync_InvalidJson_ReturnsEmptyList()
        {
            // Arrange
            var accountId = "12345";
            var invalidJson = "{ invalid json }";
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(invalidJson, Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _apiService.GetOpenTradesAsync(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}