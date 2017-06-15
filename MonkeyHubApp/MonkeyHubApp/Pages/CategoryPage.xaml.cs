using MonkeyHubApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyHubApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as CategoryViewModel)?.LoadContent();
            base.OnAppearing();
        }
    }
}