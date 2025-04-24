using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class NewsProductPage : ContentPage
	{	
		public NewsProductPage ()
		{
			InitializeComponent ();
			BindingContext = new NewsProductPageViewModel(Navigation);
		}
	}
}

