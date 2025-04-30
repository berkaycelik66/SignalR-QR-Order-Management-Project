namespace SignalRWebUI.Models
{
    public class ApiValidationErrorResponseModel
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
        public string? TraceID { get; set; }
    }
}
