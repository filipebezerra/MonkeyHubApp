using MonkeyHubApp.Services;
using MonkeyHubApp.ViewModels;
using Xamarin.Forms;

namespace MonkeyHubApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(new MonkeyHubApiService());
        }

        protected override void OnAppearing()
        {
            (BindingContext as MainViewModel)?.LoadTags();
            base.OnAppearing();
        }
    }
}
