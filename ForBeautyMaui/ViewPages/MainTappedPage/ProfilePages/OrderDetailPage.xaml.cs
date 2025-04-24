
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.ProfilePagesViewModel;


namespace ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages
{	
	public partial class OrderDetailPage : ContentPage
	{	
		public OrderDetailPage (PrefeuseOrderUserById OrderUser)
		{
			InitializeComponent ();
			BindingContext = new OrderDetailPageViewModel(OrderUser);
		}
	}
}

