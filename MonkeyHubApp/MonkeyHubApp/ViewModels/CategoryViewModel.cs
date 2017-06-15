using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public ObservableCollection<Content> DataSource { get; }

        private readonly IMonkeyHubApiService monkeyHubApiService;

        public Tag Tag { get; }

        public Command<Content> ShowItemTappedCommand { get; }

        public CategoryViewModel(IMonkeyHubApiService _monkeyHubApiService, Tag _tag) : base(_tag.Name)
        {
            monkeyHubApiService = _monkeyHubApiService;
            Tag = _tag;
            DataSource = new ObservableCollection<Content>();
            ShowItemTappedCommand = new Command<Content>(ExecuteShowContentCommand);
        }

        private async void ExecuteShowContentCommand(Content content)
        {
            await PushAsync<ContentWebViewModel>(content);
        }

        public async void LoadContent()
        {
            var contents = await monkeyHubApiService.GetContentsByTagIdAsync(Tag.Id);
            DataSource.Clear();
            if (contents.Any())
            {
                foreach(Content content in contents)
                {
                    DataSource.Add(content);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Nada encontrado",
                    $"Não encontramos nenhum conteúdo para '{Tag}'!", "Ok");
            }
        }
    }
}
