using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class BestCategoryPage : ContentPage
	{
        public BestCategoryPage ( Categories Category , HomePageViewModel homePageViewModel)
		{
			InitializeComponent ();
			BindingContext = new BestCategoryPageViewModel(Navigation , Category , homePageViewModel , App.SharedServices);
        }
        

    }
}

