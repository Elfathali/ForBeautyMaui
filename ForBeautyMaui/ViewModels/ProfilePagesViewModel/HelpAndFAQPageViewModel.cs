
using System.ComponentModel;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class HelpAndFAQPageViewModel :  INotifyPropertyChanged
	{
        private INavigation _navigation;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command CommandTapBackHelpFAQ { get; set; }

        public HelpAndFAQPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            CommandTapBackHelpFAQ = new Command(NavigateBackHelpFAQ);

        }

        private async void NavigateBackHelpFAQ()
        {
            await _navigation.PopModalAsync();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HelpAndFAQPageViewModel()
		{
		}
	}
}

