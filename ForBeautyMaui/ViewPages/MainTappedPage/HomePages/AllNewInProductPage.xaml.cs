using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class AllNewInProductPage : ContentPage
	{	
		public AllNewInProductPage ()
		{
			InitializeComponent ();
			BindingContext = new AllNewInProductPageViewModel(Navigation);
		}
	}
}

