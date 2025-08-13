
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class LunchPage : ContentPage
    {
        public ObservableCollection<MenuItem> LunchItems { get; set; }

        public LunchPage()
        {
            InitializeComponent();

            // Sample lunch items
            LunchItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Cheeseburger", Price = "$8.49", Description = "Juicy beef patty with cheese and toppings", Icon = "🍔" },
                new MenuItem { Name = "Caesar Salad", Price = "$9.49", Description = "Romaine lettuce, croutons, and Caesar dressing", Icon = "🥗" },
                new MenuItem { Name = "Grilled Chicken Sandwich", Price = "$9.25", Description = "Marinated grilled chicken in a bun", Icon = "🍗" },
                new MenuItem { Name = "Veggie Wrap", Price = "$7.99", Description = "Healthy wrap with assorted vegetables", Icon = "🌯" },
                new MenuItem { Name = "Fish and Chips", Price = "$10.49", Description = "Crispy fried fish with potato fries", Icon = "🍟" }
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
            await Navigation.PushAsync(new ViewCartPage());
        }
    }
}
