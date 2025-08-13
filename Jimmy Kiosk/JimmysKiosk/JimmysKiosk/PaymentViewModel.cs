using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using JimmysKiosk.Models;
using JimmysKiosk.Services;

namespace JimmysKiosk.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string fullName;
        private string cardNumber;
        private string expiryDate;
        private string cvv;
        private string paymentStatus;
        private decimal totalAmount;

        public string FullName
        {
            get => fullName;
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public string CardNumber
        {
            get => cardNumber;
            set { cardNumber = value; OnPropertyChanged(nameof(CardNumber)); }
        }

        public string ExpiryDate
        {
            get => expiryDate;
            set { expiryDate = value; OnPropertyChanged(nameof(ExpiryDate)); }
        }

        public string CVV
        {
            get => cvv;
            set { cvv = value; OnPropertyChanged(nameof(CVV)); }
        }

        public string PaymentStatus
        {
            get => paymentStatus;
            set { paymentStatus = value; OnPropertyChanged(nameof(PaymentStatus)); }
        }

        public decimal TotalAmount
        {
            get => totalAmount;
            set { totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
        }

        public ICommand PayCommand { get; }

        public PaymentViewModel()
        {
            PayCommand = new Command(async () => await ProcessPayment());
        }

        private async Task ProcessPayment()
        {
            try
            {
                PaymentStatus = string.Empty;

                if (string.IsNullOrWhiteSpace(FullName) ||
                    string.IsNullOrWhiteSpace(CardNumber) ||
                    string.IsNullOrWhiteSpace(ExpiryDate) ||
                    string.IsNullOrWhiteSpace(CVV))
                {
                    PaymentStatus = "Please fill all fields.";
                    return;
                }

                await Task.Delay(1000); // Simulate processing

                var payment = new PaymentModel
                {
                    FullName = FullName,
                    CardNumber = CardNumber,
                    ExpiryDate = ExpiryDate,
                    CVV = CVV,
                    Status = "Success",
                    Timestamp = DateTime.Now.ToString("g"),
                    TotalAmount = TotalAmount
                };

                var paymentResult = await new PaymentService().SendPaymentAsync(payment);

                if (!paymentResult)
                {
                    PaymentStatus = "❌ Failed to send payment to server.";
                    await Application.Current.MainPage.DisplayAlert("Error", "Payment failed to send.", "OK");
                    return;
                }

                // Build order from cart
                var cartItems = CartManager.GetCart();
                string itemsSummary = string.Join(", ", cartItems.Select(item => $"{item.Quantity}x {item.Name}"));

                var order = new OrderModel
                {
                    ItemsSummary = itemsSummary,
                    TotalAmount = TotalAmount,
                    // Timestamp = DateTime.Now
                };

                var orderResult = await new OrderService().SendOrderAsync(order);

                if (orderResult)
                {
                    CartManager.ClearCart();
                    PaymentStatus = "✅ Payment and Order saved successfully!";
                    await Application.Current.MainPage.DisplayAlert("Success", "Enjoy your order!", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new FeedbackPage());
                }
                else
                {
                    PaymentStatus = "⚠️ Payment saved, but order failed!";
                    await Application.Current.MainPage.DisplayAlert("Warning", "Payment saved, but order failed!", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Exception in ProcessPayment: {ex.Message}");
                PaymentStatus = "❌ An unexpected error occurred.";
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
