using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using JimmysKiosk.Services;


namespace JimmysKiosk
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderListPage : ContentPage
    {
        public OrderListPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private async void LoadOrders()
        {
            var orders = await DatabaseService.GetOrders();
            OrderList.ItemsSource = orders;
        }

        private async void OnClearOrdersClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Clear all orders from database?", "Yes", "Cancel");
            if (!confirm) return;

            await DatabaseService.ClearOrders();
            await DisplayAlert("Done", "All orders have been deleted.", "OK");
            LoadOrders();
        }
    }
}
