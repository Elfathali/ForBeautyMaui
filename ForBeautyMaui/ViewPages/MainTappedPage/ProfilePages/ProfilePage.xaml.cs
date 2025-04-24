using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;


namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class ProfilePage : ContentPage
	{	
		public ProfilePage ()
		{
			InitializeComponent ();
			BindingContext = new ProfilePageViewModel(Navigation);
		}
	}
}

