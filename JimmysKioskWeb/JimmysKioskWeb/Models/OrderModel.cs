namespace JimmysKioskWeb.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ItemsSummary { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
