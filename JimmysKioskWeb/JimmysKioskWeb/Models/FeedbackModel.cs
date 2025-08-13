namespace JimmysKioskWeb.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
    }
}
