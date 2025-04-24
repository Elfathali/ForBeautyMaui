using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.FavouritePageViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.FavouritePages
{	
	public partial class FavouritePage : ContentPage
	{	
		public FavouritePage()
		{
			InitializeComponent ();
			BindingContext = new FavouritePageViewModels(Navigation,App.SharedServices);
		}
	}
}

