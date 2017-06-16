using System.Threading.Tasks;
using MonkeyHubApp.Services;
using Xamarin.Forms;
using System.Linq;
using MonkeyHubApp.Pages;

namespace MonkeyHubApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private AzureService azureService;

        private Command loginWithFacebookCommand;
        public Command LoginWithFacebookCommand => loginWithFacebookCommand ??
            (loginWithFacebookCommand = new Command(async () => await ExecuteLoginWithFacebookCommand()));

        public LoginViewModel() : base("Entre com sua conta social")
        {
            azureService = DependencyService.Get<AzureService>();
        }

        private async Task ExecuteLoginWithFacebookCommand()
        {
            if (LoginWithFacebookCommand.CanExecute(null))
            {
                LoginWithFacebookCommand.ChangeCanExecute();
                var result = await azureService.LoginAsync();

                if (result)
                {
                    await PushAsync<MainViewModel>();
                }

                var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    if (page.GetType() == typeof(LoginPage))
                        App.Current.MainPage.Navigation.RemovePage(page);
                }
            }
        }
    }
}
