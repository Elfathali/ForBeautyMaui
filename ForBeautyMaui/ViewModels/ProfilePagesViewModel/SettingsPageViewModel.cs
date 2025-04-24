using System;
using System.ComponentModel;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class SettingsPageViewModel :INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public SettingsPageViewModel()
		{
		}
		private void OnPropertyChanged(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}

