using System;
using System.ComponentModel;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class AboutUsPageViewModel :INotifyPropertyChanged
	{
		private INavigation _navigation;

        public event PropertyChangedEventHandler PropertyChanged;
		public Command CommandBackAboutUs { get; set; }

        public AboutUsPageViewModel(INavigation navigation)
		{
			_navigation = navigation;
			CommandBackAboutUs = new Command(NavigateBackAboutUs);

        }

        private async void NavigateBackAboutUs()
        {
			await _navigation.PopModalAsync();
        }

        private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

