namespace Halda.Application.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? error { get; set; }

        public string? error_description { get; set; }
    }
}