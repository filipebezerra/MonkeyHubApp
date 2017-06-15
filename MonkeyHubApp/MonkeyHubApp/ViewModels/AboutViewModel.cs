namespace MonkeyHubApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Version { get; }

        public AboutViewModel() : base("Sobre o Monkey Hub app")
        {
            Version = "1.0";
        }
    }
}
