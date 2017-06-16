using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MonkeyHubApp.Auth;
using MonkeyHubApp.iOS.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace MonkeyHubApp.iOS.Auth
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, 
            MobileServiceAuthenticationProvider provider, 
            IDictionary<string, string> parameters = null)
        {
            return await client.LoginAsync(GetController(), provider, parameters);
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null)
                return null;

            var current = root;
            while(current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}
