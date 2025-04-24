using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class BestSearchPage : ContentPage
	{	
		public BestSearchPage (BestSearch bestSearch)
		{
			InitializeComponent ();
			BindingContext = new BestSearchPageViewModel(Navigation,bestSearch);
		}
	}
}

