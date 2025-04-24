

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{	
	public partial class AboutUsPage : ContentPage
	{	
		public AboutUsPage ()
		{
			InitializeComponent ();
			BindingContext = new AboutUsPageViewModel(Navigation);
		}
	}
}

