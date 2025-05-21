using ForBeautyMaui.ViewModels;

namespace ForBeautyMaui.ViewPages
{	
	public partial class MainLoginRegisterPage : ContentPage
	{	
		public MainLoginRegisterPage (string Duration)
		{
			InitializeComponent ();
			BindingContext = new RegisterPageModelView(Duration);
		}
	}
}

