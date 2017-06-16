using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MonkeyHubApp.Auth;
using Xamarin.Forms;
using MonkeyHubApp.Droid.Auth;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace MonkeyHubApp.Droid.Auth
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, 
            MobileServiceAuthenticationProvider provider, 
            IDictionary<string, string> parameters = null)
        {
            return await client.LoginAsync(Forms.Context, provider, parameters);
        }
    }
}