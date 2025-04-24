

using ForBeautyMaui.ViewModels.ProfilePagesViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class SettingsPage : ContentPage
	{	
		public SettingsPage ()
		{
			InitializeComponent ();
			BindingContext = new SettingsPageViewModel();
		}
	}
}

