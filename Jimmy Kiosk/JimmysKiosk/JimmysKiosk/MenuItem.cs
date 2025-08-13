using System.ComponentModel;

namespace JimmysKiosk
{
    public class MenuItem : INotifyPropertyChanged
    {
        private int quantity; // Backing field for the Quantity property
        public string Name { get; set; }          // Name of the menu item
        public string Price { get; set; }         // Price of the menu item
        public string Description { get; set; }   // Description of the menu item
        public string Icon { get; set; }          // Icon representing the menu item

        public int Quantity
        {
            get => quantity; // Getter for the quantity
            set
            {
                if (quantity != value) // Check if the new value is different
                {
                    quantity = value; // Update the quantity
                    OnPropertyChanged(nameof(Quantity)); // Notify the UI
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; // Event for property changes

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Raise the PropertyChanged event
        }
    }
}


