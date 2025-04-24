
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class ContactUsPage : ContentPage
	{	
		public ContactUsPage ()
		{
			InitializeComponent ();
			BindingContext = new ContactUsPageViewModel(Navigation);
		}
	}
}

