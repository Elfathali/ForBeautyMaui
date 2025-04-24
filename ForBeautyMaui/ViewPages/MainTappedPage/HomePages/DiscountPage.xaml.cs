using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class DiscountPage : ContentPage
	{	
		public DiscountPage ()
		{
			InitializeComponent ();
			BindingContext = new DiscountPageViewModel(Navigation);
		}
	}
}

