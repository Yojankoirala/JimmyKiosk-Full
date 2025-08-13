using System.IO;
using System.Threading.Tasks;
using SQLite;
using JimmysKiosk.Models;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace JimmysKiosk.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _database;

        private static async Task<SQLiteAsyncConnection> GetConnection()
        {
            if (_database != null)
                return _database;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "JimmysKiosk.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Create tables
            await _database.CreateTableAsync<PaymentModel>();
            await _database.CreateTableAsync<FeedbackModel>();
            await _database.CreateTableAsync<OrderModel>();

            return _database;
        }

        // Payment Methods
        public static async Task AddPayment(PaymentModel payment)
        {
            var db = await GetConnection();
            await db.InsertAsync(payment);
        }

        public static async Task<List<PaymentModel>> GetPayments()
        {
            var db = await GetConnection();
            return await db.Table<PaymentModel>().ToListAsync();
        }

        // Feedback Methods
        public static async Task AddFeedback(FeedbackModel feedback)
        {
            var db = await GetConnection();
            await db.InsertAsync(feedback);
        }

        public static async Task<List<FeedbackModel>> GetFeedbacks()
        {
            var db = await GetConnection();
            return await db.Table<FeedbackModel>().ToListAsync();
        }

        public static async Task ClearFeedback()
        {
            var db = await GetConnection();
            await db.DeleteAllAsync<FeedbackModel>();
        }

        // Order Methods
        public static async Task AddOrder(OrderModel order)
        {
            var db = await GetConnection();
            await db.InsertAsync(order);
        }

        public static async Task<List<OrderModel>> GetOrders()
        {
            var db = await GetConnection();
            return await db.Table<OrderModel>().ToListAsync();
        }

        public static async Task ClearOrders()
        {
            var db = await GetConnection();
            await db.DeleteAllAsync<OrderModel>();
        }
    }
}
