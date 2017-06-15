using System.Windows.Input;
using Xamarin.Forms;

namespace MonkeyHubApp.Controls
{
    public class FullListView : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty
            = BindableProperty.Create("ItemTappedCommand", typeof(ICommand), typeof(FullListView), null);

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public FullListView(ListViewCachingStrategy strategy) : base(strategy)
        {
            Initialize();
        }

        public FullListView() : this(ListViewCachingStrategy.RecycleElement)
        {
            
        }

        private void Initialize()
        {
            ItemSelected += (sender, e) =>
            {
                if (ItemTappedCommand == null)
                    return;

                if (ItemTappedCommand.CanExecute(e.SelectedItem))
                    ItemTappedCommand.Execute(e.SelectedItem);
            };
        }
    }
}
