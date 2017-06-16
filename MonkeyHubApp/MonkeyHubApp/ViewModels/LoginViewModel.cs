using System.Threading.Tasks;
using MonkeyHubApp.Services;
using Xamarin.Forms;
using System.Linq;
using MonkeyHubApp.Pages;
using System;
using System.Diagnostics;

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
                try
                {
                    LoginWithFacebookCommand.ChangeCanExecute();
                    var result = await azureService.LoginAsync();

                    if (result)
                    {
                        await PushToRootAsync<MainViewModel>();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Algo deu errado",
                                "Não conseguimos efetuar o seu login, tente novamente!", "Ok");
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"[ExecuteLoginWithFacebookCommand] Error = {ex.Message}");
                }
                finally
                {
                    LoginWithFacebookCommand.ChangeCanExecute();
                }
            }
        }
    }
}
