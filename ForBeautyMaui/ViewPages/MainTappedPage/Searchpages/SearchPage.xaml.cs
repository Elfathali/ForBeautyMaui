using System;
using System.Collections.Generic;
using ForBeautyMaui.ViewModels.SearchPageTappedviewModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage
{	
	public partial class SearchPage : ContentPage
	{	
		public SearchPage ()
		{
			InitializeComponent ();
			BindingContext = new SearchPageViewModel(Navigation);

        }
	}
}

