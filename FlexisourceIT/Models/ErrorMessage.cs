namespace FlexisourceIT.Models
{
    /// <summary>
    /// An error object returned for failed requests
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorDetail> Detail { get; set; }
    }

    /// <summary>
    /// Details of invalid request property
    /// </summary>
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
