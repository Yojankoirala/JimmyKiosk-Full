using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JimmysKiosk.Models;

namespace JimmysKiosk.Services
{
    public class FeedbackService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "http://10.0.2.2:5014/api/Feedbacks";

        public FeedbackService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _client = new HttpClient(handler);
        }

        public async Task<bool> SendFeedbackAsync(FeedbackModel feedback)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(feedback, options);
                System.Diagnostics.Debug.WriteLine($"📤 Sending Feedback JSON: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(BaseUrl, content);

                var responseText = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"📥 Feedback Response: {response.StatusCode} - {responseText}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ FeedbackService Exception: {ex.Message}");
                return false;
            }
        }

    }
}
