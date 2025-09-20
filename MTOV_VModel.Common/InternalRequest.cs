namespace MTOV_VModel.Common
{
    /// <summary>
    /// Defines the <see cref="InternalRequest" />
    /// </summary>
    public class InternalRequest
    {
        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the SessionId
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// Gets or sets the VirtualPath
        /// </summary>
        public string? VirtualPath { get; set; }

        /// <summary>
        /// Gets or sets the Lang
        /// </summary>
        public string? Lang { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string? Message { get; set; }
    }
}
