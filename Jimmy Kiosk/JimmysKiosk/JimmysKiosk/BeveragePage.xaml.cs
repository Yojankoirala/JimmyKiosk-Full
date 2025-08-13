using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class BeveragePage : ContentPage
    {
        public ObservableCollection<MenuItem> BeverageItems { get; set; }

        public BeveragePage()
        {
            InitializeComponent();

            // sample  beverage items that will be displayed in BeveragePage
            BeverageItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Coffee", Price = "$2.99", Description = "Freshly brewed coffee", Icon = "☕" },
                new MenuItem { Name = "Tea", Price = "$2.49", Description = "Organic green or black tea", Icon = "🍵" },
                new MenuItem { Name = "Orange Juice", Price = "$3.29", Description = "Freshly squeezed orange juice", Icon = "🍊" },
                new MenuItem { Name = "Smoothie", Price = "$4.99", Description = "Fruit smoothie with yogurt", Icon = "🥤" },
                new MenuItem { Name = "Soft Drink", Price = "$1.99", Description = "Choice of soda", Icon = "🥤" }
            };

            BindingContext = this;
        }
        //decrease the quantity when adding iten on cart
        private void OnDecreaseQuantityClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as MenuItem;
            if (item != null && item.Quantity > 1)
                item.Quantity--;
        }
        //increasing the quantity when adding iten on cart
        private void OnIncreaseQuantityClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as MenuItem;
            if (item != null)
                item.Quantity++;
        }
        //Adding items in cart
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
            await Navigation.PushAsync(new ViewCartPage());//navigates to viewcartpage when viewcart button's clicked
        }
    }
}
