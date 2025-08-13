using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JimmysKiosk.ViewModels;

namespace JimmysKiosk
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        private PaymentViewModel viewModel;

        public PaymentPage()
        {
            InitializeComponent();

            // ✅ Create ViewModel and bind
            viewModel = new PaymentViewModel();
            BindingContext = viewModel;

            // ✅ Calculate total from cart and assign to ViewModel
            var cart = CartManager.GetCart();
            decimal total = 0;

            if (cart != null && cart.Any())
            {
                total = cart.Sum(item =>
                {
                    if (decimal.TryParse(item.Price.Replace("$", ""), out decimal price))
                        return price * item.Quantity;
                    return 0;
                });
            }

            viewModel.TotalAmount = total;
        }
    }
}
