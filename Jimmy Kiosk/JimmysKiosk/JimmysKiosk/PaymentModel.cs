using System;
using System.Collections.Generic;
using System.Text;

namespace JimmysKiosk.Models
{
    public class PaymentModel
    {
     
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
        public decimal TotalAmount { get; set; }

    }

}
