using MonkeyHubApp.Helpers;
using Xamarin.Forms;
using System.Threading.Tasks;
using MonkeyHubApp.Services;
using System;
using System.Diagnostics;

namespace MonkeyHubApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private AzureService azureService;

        public string Version { get; }

        public string UserId { get; }

        private Command logoutAsyncCommand;
        public Command LogoutAsyncCommand => logoutAsyncCommand ?? 
            (logoutAsyncCommand = new Command(async () => await ExecuteLogoutAsyncCommand()));

        public AboutViewModel() : base("Sobre o Monkey Hub app")
        {
            Version = "1.0";
            UserId = Settings.UserId;
            azureService = DependencyService.Get<AzureService>();
        }

        private async Task ExecuteLogoutAsyncCommand()
        {
            if (LogoutAsyncCommand.CanExecute(null))
            {
                try
                {
                    LogoutAsyncCommand.ChangeCanExecute();
                    await azureService.LogoutAsync();

                    if (!Settings.IsLoggedIn)
                    {
                        await PushToRootAsync<LoginViewModel>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[ExecuteLogoutAsyncCommand] Error = {ex.Message}");
                }
                finally
                {
                    LogoutAsyncCommand.ChangeCanExecute();
                }
            }
        }
    }
}
