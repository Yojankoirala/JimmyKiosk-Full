using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JimmysKiosk.Models;

namespace JimmysKiosk.Services
{
    public class PaymentService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "http://10.0.2.2:5014/api/Payments";

        public PaymentService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _client = new HttpClient(handler);
        }


        public async Task<bool> SendPaymentAsync(PaymentModel payment)
        {
            try
            {
                var json = JsonSerializer.Serialize(payment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(BaseUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
