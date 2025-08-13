using System;
using Xamarin.Forms;

namespace JimmysKiosk
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        async void OnBreakfastClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BreakfastPage());//Navigate to BreakfastPage 
        }

        async void OnLunchClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LunchPage());//Navigate to Lunch Pagr
        }

        async void OnDinnerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DinnerPage()); //Navigate to DinnerPage
        }

        async void OnBeverageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeveragePage());//Navigate to BeveragePage
        }

        async void OnViewCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewCartPage());//Navigate to ViewCartPage
        }
       


    }
}

