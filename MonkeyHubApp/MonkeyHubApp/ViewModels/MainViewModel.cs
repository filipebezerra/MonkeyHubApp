using MonkeyHubApp.Models;
using MonkeyHubApp.Notification;
using MonkeyHubApp.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Tag> DataSource { get; }

        public Command AboutCommand { get; }

        public Command ShowSearchPageCommand { get; }

        public Command<Tag> ShowItemTappedCommand { get; }

        private readonly IMonkeyHubApiService monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService _monkeyHubApiService) : base("Monkey Hub")
        {
            monkeyHubApiService = _monkeyHubApiService;
            ShowItemTappedCommand = new Command<Tag>(ExecuteShowSelectedTagCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
            ShowSearchPageCommand = new Command(ExecuteShowSearchPageCommand);
            DataSource = new ObservableCollection<Tag>();
        }

        private async void ExecuteShowSearchPageCommand()
        {
            await PushAsync<SearchContentViewModel>(monkeyHubApiService);
        }

        private async void ExecuteShowSelectedTagCommand(Tag tag)
        {
            await PushAsync<CategoryViewModel>(monkeyHubApiService, tag);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        public async void LoadTags()
        {
            var tags = await monkeyHubApiService.GetTagsAsync();
            DataSource.Clear();
            foreach(Tag tag in tags)
            {
                DataSource.Add(tag);
            }
        }

        public void RegisterForPushNotification()
        {
            var push = DependencyService.Get<IPushNotification>();
            push.RegisterForPushNotification();
        }
    }
}
