using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using JimmysKiosk.Models;
using JimmysKiosk.Services;

namespace JimmysKiosk.ViewModels
{
    public class FeedbackViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string customerName;
        private string message;
        private string feedbackStatus;

        public string CustomerName
        {
            get => customerName;
            set { customerName = value; OnPropertyChanged(nameof(CustomerName)); }
        }

        public string Message
        {
            get => message;
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }

        public string FeedbackStatus
        {
            get => feedbackStatus;
            set { feedbackStatus = value; OnPropertyChanged(nameof(FeedbackStatus)); }
        }

        public ICommand SubmitFeedbackCommand { get; }

        public FeedbackViewModel()
        {
            SubmitFeedbackCommand = new Command(async () => await SubmitFeedbackAsync());
        }

        private async Task SubmitFeedbackAsync()
        {
            if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(Message))
            {
                FeedbackStatus = "Please enter your name and feedback.";
                return;
            }

            var feedback = new FeedbackModel
            {
                CustomerName = CustomerName,
                Message = Message,
                Timestamp = DateTime.Now.ToString("g")
            };

            var result = await new FeedbackService().SendFeedbackAsync(feedback);

            if (result)
            {
                FeedbackStatus = "✅ Thank you for your feedback!";
                await Application.Current.MainPage.DisplayAlert("Success", "Feedback submitted successfully!", "OK");
                CustomerName = string.Empty;
                Message = string.Empty;
            }
            else
            {
                FeedbackStatus = "❌ Failed to send feedback.";
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong.", "OK");
            }
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
