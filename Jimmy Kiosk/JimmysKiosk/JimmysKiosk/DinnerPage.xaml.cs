using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class DinnerPage : ContentPage
    {
        public ObservableCollection<MenuItem> DinnerItems { get; set; }

        public DinnerPage()
        {
            InitializeComponent();

            // Sample Dinner  items 
            DinnerItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Steak", Price = "$21.99", Description = "Grilled steak with garlic butter", Icon = "🥩" },
                new MenuItem { Name = "Pasta Primavera", Price = "$13.49", Description = "Pasta with seasonal vegetables", Icon = "🍝" },
                new MenuItem { Name = "Grilled Salmon", Price = "$23.99", Description = "Freshly grilled salmon", Icon = "🐟" },
                new MenuItem { Name = "Mixed Greens Salad", Price = "$9.49", Description = " spinach with lemon dressing,spiral beets,tomatoes,cucumbers", Icon = "🥗" },
                new MenuItem { Name = "Chicken Alfredo", Price = "$14.99", Description = "Creamy Alfredo sauce over pasta", Icon = "🍜" }
            };

            BindingContext = this;
        }

        private void OnDecreaseQuantityClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as MenuItem;
            if (item != null && item.Quantity > 1)
                item.Quantity--;
        }

        private void OnIncreaseQuantityClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as MenuItem;
            if (item != null)
                item.Quantity++;
        }

        private async void OnAddToCartClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as MenuItem;
            if (item != null)
            {
                // Clone the item to avoid reference issues
                var cartItem = new MenuItem
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Icon = item.Icon,
                    Quantity = item.Quantity
                };

                CartManager.AddToCart(cartItem); // Your cart logic
                await DisplayAlert("Added to Cart", $"{item.Quantity} x {item.Name} added.", "OK");
                item.Quantity = 0; // Reset quantity
            }
        }

        private async void OnViewCartClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ViewCartPage());//navigate to ViewCartPage
        }
    }
}

