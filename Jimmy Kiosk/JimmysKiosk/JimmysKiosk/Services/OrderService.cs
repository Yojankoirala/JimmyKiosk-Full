using JimmysKiosk.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;

public class OrderService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "http://10.0.2.2:5014/api/Orders";

    public OrderService()
    {
        // ✅ Properly initialize HttpClient and bypass SSL cert check for emulator
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true
        };
        _client = new HttpClient(handler);
    }

    public async Task<bool> SendOrderAsync(OrderModel order)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(order, options);
            System.Diagnostics.Debug.WriteLine($"📤 Sending Order JSON: {json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl, content);

            string responseText = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"📥 Response: {response.StatusCode} - {responseText}");

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"❌ OrderService failed: {response.StatusCode} - {responseText}");
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"❌ OrderService Exception: {ex.Message}");
            return false;
        }
    }
}
