public class OrderModel
{
    public int Id { get; set; }
    public string ItemsSummary { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    // Removed: public string Timestamp { get; set; } = string.Empty;
}
