namespace MTOV_VModel.Common
{
    /// <summary>
    /// Defines the <see cref="Result" />
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the Data
        /// </summary>
        public object? Data { get; set; }
    }
}
