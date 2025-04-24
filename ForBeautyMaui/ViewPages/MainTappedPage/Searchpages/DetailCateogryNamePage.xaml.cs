using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.SearchPageTappedviewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.Searchpages
{	
	public partial class DetailCateogryNamePage : ContentPage
	{	
		public DetailCateogryNamePage (Categories categories ,CategorDital categorDital)
		{
			InitializeComponent ();
			BindingContext = new DetailCateogryNameViewModel(Navigation, categories , categorDital);
		}
	}
}

