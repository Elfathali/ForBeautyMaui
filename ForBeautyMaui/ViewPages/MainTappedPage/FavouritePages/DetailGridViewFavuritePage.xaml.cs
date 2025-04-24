using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.FavouritePageViewModel;
namespace ForBeautyMaui.ViewPages.MainTappedPage.FavouritePages
{	
	public partial class DetailGridViewFavuritePage : ContentPage
	{	
		public DetailGridViewFavuritePage ()
		{
			InitializeComponent ();
			BindingContext = new DetailGridViewFavuritePageViewModel();

        }
	}
}

