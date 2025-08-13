using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class BreakfastPage : ContentPage
    {
        public ObservableCollection<MenuItem> BreakfastItems { get; set; }

        public BreakfastPage()
        {
            InitializeComponent();

            // Sample Breakfast items
            BreakfastItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Pancakes", Price = "$5.20", Description = "Fluffy pancakes with maple syrup", Icon = "🥞" },
                new MenuItem { Name = "Omelette", Price = "$6.29", Description = "Eggs with veggies and cheese", Icon = "🍳" },
                new MenuItem { Name = "Bacon & Eggs", Price = "$6.59", Description = "Crispy bacon and sunny-side eggs", Icon = "🥓" },
                new MenuItem { Name = "Waffles", Price = "$5.25", Description = "Golden waffles with whipped cream", Icon = "🧇" },
                new MenuItem { Name = "Breakfast Burrito", Price = "$7.50", Description = "Sausage, eggs, cheese, and salsa in a tortilla", Icon = "🌯" }
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


