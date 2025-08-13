using System;
using Xamarin.Forms;
using JimmysKiosk.Models;
using JimmysKiosk.Services;

namespace JimmysKiosk
{
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text?.Trim();
            string message = MessageEditor.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(message))
            {
                StatusLabel.Text = "Please enter your name and feedback.";
                StatusLabel.TextColor = Color.Red;
                StatusLabel.IsVisible = true;
                return;
            }

            var feedback = new FeedbackModel
            {
                CustomerName = name,
                Message = message,
                Timestamp = DateTime.Now.ToString("g")
            };

            var service = new FeedbackService();
            bool result = await service.SendFeedbackAsync(feedback);

            if (result)
            {
                // ✅ Redirect to ThankYouPage
                await Navigation.PushAsync(new ThankYouPage());

                // Optional: clear fields if you return back to this page
                NameEntry.Text = string.Empty;
                MessageEditor.Text = string.Empty;
                StatusLabel.IsVisible = false;
            }
            else
            {
                StatusLabel.Text = "❌ Failed to submit feedback.";
                StatusLabel.TextColor = Color.Red;
                StatusLabel.IsVisible = true;
            }
        }

        private async void OnSkipClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(); // Or navigate elsewhere
        }
    }
}
