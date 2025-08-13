using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class ViewCartPage : ContentPage
    {
        public ObservableCollection<MenuItem> CartItems { get; set; } // Collection of items in the cart

        public ViewCartPage()
        {
            InitializeComponent();

            // Initialize the cart items from the CartManager
            CartItems = new ObservableCollection<MenuItem>(CartManager.GetCart());
            BindingContext = this; // Set the binding context for data binding
            UpdateTotal(); // Update the total price display
            CartCollectionView.ItemsSource = CartItems; // Set the item source for the CollectionView
        }

        private void UpdateTotal()
        {
            decimal total = 0; // Initialize total price
            foreach (var item in CartItems)
            {
                // Parse the price and calculate the total
                if (decimal.TryParse(item.Price.Replace("$", ""), out decimal price))
                {
                    total += price * item.Quantity; // Add item price multiplied by quantity
                }
            }
            TotalLabel.Text = $"Total: ${total:F2}"; // Update the total label
        }

        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Get the button that was clicked
            var item = button?.CommandParameter as MenuItem; // Get the associated MenuItem
            if (item != null)
            {
                if (item.Quantity > 1) // If quantity is greater than 1, just decrease it
                {
                    item.Quantity--; // Decrease the quantity
                }
                else // If quantity is 1, remove the item
                {
                    CartItems.Remove(item); // Remove the item from the ObservableCollection
                    CartManager.RemoveFromCart(item.Name); // Remove the item from the CartManager
                }
                UpdateTotal(); // Update the total price
            }
        }

        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Get the button that was clicked
            var item = button?.CommandParameter as MenuItem; // Get the associated MenuItem
            if (item != null) // Ensure item exists
            {
                item.Quantity++; // Increase the quantity
                UpdateTotal(); // Update the total price
            }
        }

        private void OnRemoveItemClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Get the button that was clicked
            var item = button?.CommandParameter as MenuItem; // Get the associated MenuItem
            if (item != null)
            {
                Console.WriteLine($"Removing item: {item.Name}"); // Debugging output
                if (CartItems.Contains(item)) // Check if the item exists in the cart
                {
                    CartItems.Remove(item); // Remove the item from the ObservableCollection
                    CartManager.RemoveFromCart(item.Name); // Remove the item from the CartManager
                    UpdateTotal(); // Update the total price
                    Console.WriteLine($"Item {item.Name} removed successfully."); // Debugging output
                }
                else
                {
                    Console.WriteLine($"Item {item.Name} not found in CartItems."); // Debugging output
                }
            }
        }
        private async void OnProceedToPaymentClicked(object sender, EventArgs e)
        {
            // Check if the cart is empty
            if (CartItems.Count == 0)
            {
                // Display an alert if the cart is empty
                await DisplayAlert("Oops!", "Your cart is empty. Please add some delicious items before proceeding to payment!", "OK");
                return; // Exit the method early
            }

            // If the cart is not empty, navigate to the PaymentPage
            await Navigation.PushAsync(new PaymentPage());
        }

    }
}



