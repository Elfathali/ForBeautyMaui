using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class DetailViewProductHomePage : ContentPage
	{	
		public DetailViewProductHomePage (Product product)
		{
			InitializeComponent ();
			BindingContext = new DetailViewProductHomePageViewModel(Navigation,product ,App.SharedServices);
		}
       
    }
}

