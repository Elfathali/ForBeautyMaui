using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.HomePages
{	
	public partial class ProductByCategories : ContentPage
	{	
		public ProductByCategories (string BrandName)
		{
			InitializeComponent ();
			BindingContext = new ProductByCategoriesViewModel(BrandName);
		}
	}
}

