using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MonkeyHubApp.Auth;
using Xamarin.Forms;
using MonkeyHubApp.Helpers;
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
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider, parameters);
                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;

                return user;
            } catch(Exception ex)
            {
                // TODO: log error
                throw ex;
            }
        }
    }
}