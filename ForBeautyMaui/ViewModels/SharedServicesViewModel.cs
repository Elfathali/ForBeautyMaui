using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels
{
	public class SharedServicesViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        public event Action ShoppingCartUpdated;
        public double _TotalPrice;
        private bool _StackFavouriteVisiblety;
        public ObservableCollection<ShoppingCart> ObsShopping { get; set; }
		public ObservableCollection<AddToFavourite> ObsFavourite { get; set; }
        public ObservableCollection<Product> RecentlyViewd { get; set; }
        public SharedServicesViewModel()
		{
			ObsFavourite = new ObservableCollection<AddToFavourite>();
			ObsShopping = new ObservableCollection<ShoppingCart>();
            RecentlyViewd = new ObservableCollection<Product>();
            
        }
        public double UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            bool itemFound = false;

            foreach (var item in ObsShopping)
            {
                if (item.ProductId == shoppingCart.ProductId && item.Size == shoppingCart.Size)
                {
                    item.Qyt += 1;
                    itemFound = true;
                    break;
                }
            }
            if (!itemFound)
            {
                ObsShopping.Add(shoppingCart);
            }
            ShoppingCartUpdated?.Invoke();
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            BadgeCounterService.SetCount(ObsShopping.Count);
            return _TotalPrice;
        }


        
       


        public async void GetRecentlyViewd()
        {
            var response = await ApiSerives.GetRecentlyViewd(Preferences.Get("user_Id", 0));
            foreach (var item in response)
            {
                
                    RecentlyViewd.Add(item);
                

            }
            // StackRecentltyView = response.Count != 0 ? true : false;

        }
       

        public void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}

