using System;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class ThankYouPage : ContentPage
    {
        public ThankYouPage()
        {
            InitializeComponent();
        }

        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(); // Go to the home/menu
        }
    }
}
