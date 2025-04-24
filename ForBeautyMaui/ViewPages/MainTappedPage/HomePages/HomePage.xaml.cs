using System;
using System.Collections.Generic;
using ForBeautyMaui;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage
{	
	public partial class HomePage : ContentPage
	{	
		public HomePage ()
		{
			InitializeComponent();
			BindingContext = new HomePageViewModel(Navigation, App.SharedServices) ;

		}
       

    }
}

