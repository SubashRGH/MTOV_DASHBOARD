namespace MTOV_WebApp.Extensions
{
    using MTOV_AppSettings;
    using MTOV_DTO.Auth;
    using MTOV_VModel.Common;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;

    /// <summary>
    /// Extensions (token handler)
    /// </summary>
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WebAppProperty _property;

        public BearerTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _property = new WebAppProperty();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                // Call API to get new token
                token = await GetNewTokenAsync();
                _httpContextAccessor.HttpContext?.Session.SetString("AccessToken", token);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetNewTokenAsync()
        {
            string token = string.Empty;
            // Replace with your actual token endpoint and credentials
            using var client = new HttpClient();

            client.BaseAddress = new Uri(_property?.ApiConfigProp?.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Clear();

            var request = new
            {
                userName = _property?.ApiConfigProp?.Auth?.AuthUser ?? "",
                password = _property?.ApiConfigProp?.Auth?.AuthPass ?? ""
            };

            var json = JsonConvert.SerializeObject(request);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_property?.ApiConfigProp?.Auth?.Login ?? "", content);

            var responseData = await APIResponseHelper.ReadHttpResponseMessage(response);

            if (responseData?.StatusCode == 100)
            {
                var authRes = JsonConvert.DeserializeObject<AuthResDto>(JsonConvert.SerializeObject(responseData.Data));

                token = authRes?.Token ?? "";
            }

            return token;
        }
    }
}