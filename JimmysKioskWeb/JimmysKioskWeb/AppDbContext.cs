using Microsoft.EntityFrameworkCore;
using JimmysKioskWeb.Models;

namespace JimmysKioskWeb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
    }
}
