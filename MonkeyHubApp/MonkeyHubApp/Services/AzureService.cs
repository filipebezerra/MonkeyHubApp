using Microsoft.WindowsAzure.MobileServices;
using MonkeyHubApp.Auth;
using MonkeyHubApp.Helpers;
using MonkeyHubApp.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace MonkeyHubApp.Services
{
    public class AzureService
    {
        public MobileServiceClient Client { get; set; } = null;

        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(Constants.AzureMobileAppUrl);

            if (!string.IsNullOrWhiteSpace(Settings.UserId) && 
                !string.IsNullOrWhiteSpace(Settings.AuthToken))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }
        
        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
                return true;
            }
        }

        public async Task LogoutAsync()
        {
            Initialize();

            if (Settings.IsLoggedIn)
            {
                var auth = DependencyService.Get<IAuthentication>();
                await auth.LogoutAsync(Client);
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
            }
        }
    }
}
