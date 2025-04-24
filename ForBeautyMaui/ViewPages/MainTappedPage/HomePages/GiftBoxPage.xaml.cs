using System;
using System.Collections.Generic;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{	
	public partial class GiftBoxPage : ContentPage
	{	
		public GiftBoxPage (Product product)
		{
			InitializeComponent ();
			BindingContext = new GiftBoxPageViewModel(product);
		}
	}
}

