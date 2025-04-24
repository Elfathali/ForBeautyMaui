using ForBeautyMaui.ViewModels;

namespace ForBeautyMaui.ViewPages
{	
	public partial class MainLoginRegisterPage : ContentPage
	{	
		public MainLoginRegisterPage ()
		{
			InitializeComponent ();
			BindingContext = new RegisterPageModelView();
		}
	}
}

