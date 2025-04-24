using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels;
using ForBeautyMaui.ViewModels.SearchPageTappedviewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.Searchpages
{	
	public partial class ProductByCategoryPage : ContentPage
	{	
		public ProductByCategoryPage ( Categories categories , CategorDital categorDital , string CategoryName )
		{
			InitializeComponent ();
			BindingContext = new ProductByCategoryPageViewModel(Navigation,categories , categorDital , CategoryName ,App.SharedServices );

		}
	}
}