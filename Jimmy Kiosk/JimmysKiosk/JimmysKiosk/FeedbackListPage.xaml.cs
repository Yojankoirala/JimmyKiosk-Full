using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JimmysKiosk.Services;

namespace JimmysKiosk
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackListPage : ContentPage
    {
        public FeedbackListPage()
        {
            InitializeComponent();
            LoadFeedbacks();
        }

        private async void LoadFeedbacks()
        {
            var feedbacks = await DatabaseService.GetFeedbacks();
            FeedbackList.ItemsSource = feedbacks;
        }

        private async void OnClearFeedbackClicked(object sender, System.EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Do you want to clear all feedbacks?", "Yes", "Cancel");
            if (!confirm) return;

            await DatabaseService.ClearFeedback();
            await DisplayAlert("Done", "All feedback entries have been deleted.", "OK");

            LoadFeedbacks(); // Refresh list
        }
    }
}
