using ForBeautyMaui.ViewModels;

namespace ForBeautyMaui.ViewPages
{	
	public partial class WelcomePage : ContentPage
	{	
		public WelcomePage (string Duration)
		{
			InitializeComponent ();
            BindingContext = new RegisterPageModelView(Duration);
		}

       

    }
}

