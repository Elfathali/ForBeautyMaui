using System;
using System.Collections.Generic;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.ShoppingCartPageTappedViewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage.ShoppingCardPages
{	
	public partial class DeliveryPage : ContentPage
	{	
		public DeliveryPage(Order OrderDetail)
		{
			InitializeComponent ();
			BindingContext = new DeliveryPageViewModel(OrderDetail);
		}
	}
}

