
using System.Collections.Generic;


namespace JimmysKiosk
{
    public static class CartManager
    {
        private static List<MenuItem> cartItems = new List<MenuItem>(); // List to hold cart items

        public static void AddToCart(MenuItem item)
        {
            // Check if the item already exists in the cart
            var existing = cartItems.Find(i => i.Name == item.Name);
            if (existing != null)
            {
                existing.Quantity += item.Quantity; // Update quantity if item already exists
            }
            else
            {
                // Clone to avoid reference issues
                cartItems.Add(new MenuItem
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Icon = item.Icon,
                    Quantity = item.Quantity
                });
            }
        }

        public static IReadOnlyList<MenuItem> GetCart()
        {
            return cartItems.AsReadOnly(); // Return a read-only list of cart items
        }

        public static void ClearCart()
        {
            cartItems.Clear(); // Clear all items from the cart
        }

        public static void RemoveFromCart(string itemName)
        {
            // Remove all items with the specified name from the cart
            cartItems.RemoveAll(i => i.Name == itemName);
        }

        public static void UpdateQuantity(string itemName, int newQuantity)
        {
            var item = cartItems.Find(i => i.Name == itemName);
            if (item != null)
                item.Quantity = newQuantity; // Update the quantity of the specified item
        }
    }
}
