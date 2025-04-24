
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;


namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class PreveuseOrderPage : ContentPage
	{	
		public PreveuseOrderPage ()
		{
			InitializeComponent ();
			BindingContext = new PreveuseOrderPageViewModel(Navigation);
		}
	}
}

