using ForBeautyMaui.ViewModels.ProfilePagesViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class HelpAndFAQPage : ContentPage
	{	
		public HelpAndFAQPage ()
		{
			InitializeComponent ();
			BindingContext = new HelpAndFAQPageViewModel(Navigation);
		}
	}
}

