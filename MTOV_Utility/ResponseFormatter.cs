using MTOV_VModel.Common;

namespace MTOV_Utility
{
    /// <summary>
    /// Defines the <see cref="ResponseFormatter" />
    /// </summary>
    public static class ResponseFormatter
    {
        /// <summary>
        /// The ConstructReturnResponse
        /// </summary>
        /// <param name="statusCode">The statusCode<see cref="int"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="responseData">The responseData<see cref="object"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public static Result ConstructReturnResponse(
            int statusCode,
            string message,
            object? responseData = null
        )
        {
            return new Result
            {
                StatusCode = statusCode,
                Message = message,
                Data = responseData!,
            };
        }

        /// <summary>
        /// The FormatResponse
        /// </summary>
        /// <param name="errMsg">The errMsg<see cref="string"/></param>
        /// <param name="httpMethod">The httpMethod<see cref="HttpMethods"/></param>
        /// <param name="successMsg">The successMsg<see cref="string"/></param>
        /// <param name="responseData">The responseData<see cref="object"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public static Result FormatResponse(
           string errMsg,
           HttpMethods httpMethod,
           string successMsg = "",
           object? responseData = null
        )
        {
            if (!string.IsNullOrEmpty(errMsg))
            {
                // Handle language-based message return if needed
                var errorCode = errMsg == GlobalConstants.INVALID_SESSION_OR_EXPIRED ? 103 : 101;
                return ConstructReturnResponse(errorCode, errMsg);
            }

            if (responseData != null || !string.IsNullOrEmpty(successMsg))
            {
                return ConstructReturnResponse(100, successMsg, responseData);
            }

            return ConstructReturnResponse(104, GlobalConstants.NO_RESULTS_FOUND);
        }

        /// <summary>
        /// The FormatErrorResponse
        /// </summary>
        /// <param name="errorMessage">The errorMessage<see cref="string"/></param>
        /// <param name="responseData">The responseData<see cref="object?"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public static Result FormatErrorResponse(
         string errorMessage,
         object? responseData = null
        )
        {
            if (responseData != null || !string.IsNullOrEmpty(errorMessage))
            {
                return ConstructReturnResponse(101, errorMessage, responseData);
            }
            return ConstructReturnResponse(104, GlobalConstants.NO_RESULTS_FOUND);
        }

        /// <summary>
        /// The FormatSuccessResponse
        /// </summary>
        /// <param name="successMsg">The successMsg<see cref="string"/></param>
        /// <param name="responseData">The responseData<see cref="object?"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public static Result FormatSuccessResponse(
          string successMsg,
          object? responseData = null
        )
        {
            if (responseData != null || !string.IsNullOrEmpty(successMsg))
            {
                return ConstructReturnResponse(100, successMsg, responseData);
            }
            return ConstructReturnResponse(104, GlobalConstants.NO_RESULTS_FOUND);
        }
    }
}
