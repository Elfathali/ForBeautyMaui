using ForBeautyMaui.ViewModels;

namespace ForBeautyMaui.ViewPages
{	
	public partial class RegisterDetailUserPage : ContentPage
	{	
		public RegisterDetailUserPage (string PhoneNumber)
		{
			InitializeComponent ();
			BindingContext = new RegisterDetailUserPageViewModel(PhoneNumber);
		}
	}
}

