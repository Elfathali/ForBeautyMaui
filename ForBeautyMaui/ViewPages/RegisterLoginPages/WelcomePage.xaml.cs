using ForBeautyMaui.ViewModels;

namespace ForBeautyMaui.ViewPages
{	
	public partial class WelcomePage : ContentPage
	{	
		public WelcomePage ()
		{
			InitializeComponent ();
            BindingContext = new RegisterPageModelView();
		}

       

    }
}

