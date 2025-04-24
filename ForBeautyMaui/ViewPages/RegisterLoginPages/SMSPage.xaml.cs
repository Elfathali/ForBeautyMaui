using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels;
namespace ForBeautyMaui.ViewPages
{	
	public partial class SMSPage : ContentPage
	{	
		public SMSPage (string PhoneNumber)
		{
			InitializeComponent ();
			BindingContext = new SMSPageViewModel(PhoneNumber);
		}
	}
}

