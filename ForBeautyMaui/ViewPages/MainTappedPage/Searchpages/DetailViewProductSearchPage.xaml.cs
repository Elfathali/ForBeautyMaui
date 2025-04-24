using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.FavouritePageViewModel;
using ForBeautyMaui.ViewModels.SearchPageTappedviewModel;
using ForBeautyMaui;

namespace ForBeautyMaui.ViewPages.MainTappedPage.Searchpages
{	
	public partial class DetailViewProductSearchPage : ContentPage
	{	
		public DetailViewProductSearchPage (Product product )
		{
			InitializeComponent ();
			BindingContext = new DetailViewProductSearchViewModel(Navigation ,product , App.SharedServices );
		}
	}
}

