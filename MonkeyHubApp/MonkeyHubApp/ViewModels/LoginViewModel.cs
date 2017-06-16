﻿using System.Threading.Tasks;
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
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Algo deu errado",
                                "Não conseguimos efetuar o seu login, tente novamente!", "Ok");
                    }

                    var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                    foreach (var page in existingPages)
                    {
                        if (page.GetType() == typeof(LoginPage))
                            App.Current.MainPage.Navigation.RemovePage(page);
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
