namespace MTOV_VModel.Common
{
    using Newtonsoft.Json;

    public static class APIResponseHelper
    {
        public static async Task<Result> ReadHttpResponseMessage(HttpResponseMessage response)
        {
            Result result = new();

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var resString = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(resString))
                    {
                        result = JsonConvert.DeserializeObject<Result>(resString);
                    }
                    else
                    {
                        result = new Result
                        {
                            StatusCode = 404,
                            Message = "Invalid api response"
                        };
                    }

                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                    result = new Result
                    {
                        StatusCode = 401,
                        Message = "Authorization failed"
                    };
                    break;

                default:
                    result = new Result
                    {
                        StatusCode = 500,
                        Message = "Invalid api response"
                    };
                    break;
            }

            return result ?? new();
        }
    }
}