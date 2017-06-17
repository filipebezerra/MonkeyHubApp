using MonkeyHubApp.Services;
using MonkeyHubApp.ViewModels;
using Xamarin.Forms;

namespace MonkeyHubApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainViewModel(new MonkeyHubApiService());
        }

        protected override void OnAppearing()
        {
            viewModel.LoadTags();
            viewModel.RegisterForPushNotification();
            base.OnAppearing();
        }
    }
}
