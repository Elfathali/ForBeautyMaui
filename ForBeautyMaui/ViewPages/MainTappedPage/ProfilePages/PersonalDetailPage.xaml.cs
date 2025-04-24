
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class PersonalDetailPage : ContentPage
	{	
		public PersonalDetailPage ()
		{
			InitializeComponent ();
			BindingContext = new PersonalDeatilPageViewModel();
		}
	}
}

