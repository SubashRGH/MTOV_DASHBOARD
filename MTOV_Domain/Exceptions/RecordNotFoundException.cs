namespace MTOV_Domain.Exceptions
{
    /// <summary>
    /// Defines the <see cref="RecordNotFoundException" />
    /// </summary>
    public class RecordNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/></param>
        public RecordNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="innerException">The innerException<see cref="Exception"/></param>
        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
