using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.ShoppingCartPageTappedViewModel;


namespace ForBeautyMaui.ViewPages.MainTappedPage
{	
	public partial class ShoppingPage : ContentPage
	{	
		public ShoppingPage ()
		{
			InitializeComponent ();
			BindingContext = new ShoppingCartViewModel(App.SharedServices);
		}
	}
}

 