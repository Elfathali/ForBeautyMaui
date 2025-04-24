using System;
using System.ComponentModel;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class ChangePasswordPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ChangePasswordPageViewModel()
		{
		}
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

