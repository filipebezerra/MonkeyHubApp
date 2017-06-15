using MonkeyHubApp.Services;
using Xamarin.Forms;
using MonkeyHubApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MonkeyHubApp.ViewModels
{
    public class SearchContentViewModel : BaseViewModel
    {
        private readonly IMonkeyHubApiService monkeyHubApiService;

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        public ObservableCollection<Content> DataSource { get; }

        public Command SearchCommand { get; }

        public Command<Content> ShowItemTappedCommand { get; }

        public SearchContentViewModel(IMonkeyHubApiService _monkeyHubApiService) : base("Busca de conteúdo")
        {
            monkeyHubApiService = _monkeyHubApiService;
            SearchCommand = new Command(ExecuteSearchCommand);
            ShowItemTappedCommand = new Command<Content>(ExecuteShowContentCommand);
            DataSource = new ObservableCollection<Content>();
        }

        private async void ExecuteShowContentCommand(Content content)
        {
            await PushAsync<ContentWebViewModel>(content);
        }

        private async void ExecuteSearchCommand()
        {
            var results = await monkeyHubApiService.GetContentsByFilterAsync(SearchText);
            DataSource.Clear();
            if (results.Any())
            {
                foreach (Content content in results)
                {
                    DataSource.Add(content);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Nada encontrado", 
                    $"Não encontramos nada para '{SearchText}'!", "Ok");
            }
        }
    }
}
