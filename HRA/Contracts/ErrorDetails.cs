namespace HRA.Contracts
{
    public class ErrorDetails
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets localized error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets error extra information in JSON format.
        /// </summary>
        public string[] Data { get; set; }
    }
}
