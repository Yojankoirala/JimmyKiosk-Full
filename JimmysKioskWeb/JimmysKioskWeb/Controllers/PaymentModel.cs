namespace JimmysKioskWeb.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string Status { get; set; } = "Success";
        public string Timestamp { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
