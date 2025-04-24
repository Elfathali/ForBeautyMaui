
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;


namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class PrivacyPoliciesPage : ContentPage
	{	
		public PrivacyPoliciesPage ()
		{
			InitializeComponent ();
			BindingContext = new PrivacyPoliciesPageViewModel(Navigation);
		}
	}
}

