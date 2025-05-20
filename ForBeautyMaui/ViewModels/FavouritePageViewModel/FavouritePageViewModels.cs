using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.FavouritePages;

namespace ForBeautyMaui.ViewModels.FavouritePageViewModel
{
	public class FavouritePageViewModels : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private INavigation _navigation;
		private SharedServicesViewModel _SharedServices;
		private AddToFavourite _ListViewSelectedItem;
		private bool _StackFavouriteVisiblety;
		private bool _GridFavuriteVisiblity;
		public ObservableCollection<AddToFavourite> ObsFavouirte => _SharedServices.ObsFavourite;
		public Command<AddToFavourite> CommandFrameAddToshoppingCart { get; set; }
		public Command<AddToFavourite> CommandDeleteFavourite { get; set; }
		public AddToFavourite ListViewSelectedItem
		{
			get => _ListViewSelectedItem;
			set
			{
				if (_ListViewSelectedItem != value)
				{
					_ListViewSelectedItem = value;
					OnpropertyChanged(nameof(ListViewSelectedItem));
					OnItemSeleted(_ListViewSelectedItem);

				}
			}

		}
		public bool StackFavouriteVisiblety
		{
			get => _StackFavouriteVisiblety;
			set
			{
				if (_StackFavouriteVisiblety != value)
				{
					_StackFavouriteVisiblety = value;
					OnpropertyChanged(nameof(StackFavouriteVisiblety));
				}

            }

        }
        

        

        public bool GridFavuriteVisiblity
		{
			get => _GridFavuriteVisiblity;
			set
			{
				if(_GridFavuriteVisiblity != value)
				{
					_GridFavuriteVisiblity = value;
					OnpropertyChanged(nameof(GridFavuriteVisiblity));
				}
			}

        }
		public ObservableCollection<AddToFavourite> ObsFavourite
		{
			get => _SharedServices.ObsFavourite;
			
		}

        private void OnItemSeleted(AddToFavourite Product)
        {
			if (Product != null)
			{
				_navigation.PushAsync(new DetailGridViewFavuritePage());
				ListViewSelectedItem = null;
                OnpropertyChanged(nameof(ListViewSelectedItem));

            }
        }
        public FavouritePageViewModels(INavigation navigation , SharedServicesViewModel sharedServices)
		{
			_navigation = navigation;
			_SharedServices = sharedServices;
            GetFavourite();
            CommandFrameAddToshoppingCart = new Command<AddToFavourite>((item) => AddToShoppingFromFavourite(item));
            CommandDeleteFavourite = new Command<AddToFavourite>((item) => DeleteFromFavourite(item));
            _SharedServices.ObsFavourite.CollectionChanged += OnFavouriteCollectionChanged;

            UpdateFavouriteVisibility();


        }
        private void UpdateFavouriteVisibility()
        {
            // Update visibility based on item count in ObsFavourite
            StackFavouriteVisiblety = ObsFavourite.Count > 0;
		}

        private void OnFavouriteCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Update visibility whenever collection changes
            UpdateFavouriteVisibility();
        }
        

        //private  void OnFavouriteCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    GridFavuriteVisiblity = ListViewFavouriteSource.Any();

        //}

        public async void AddToShoppingFromFavourite(AddToFavourite item)
        {
            if (item != null)
            {
				var shopping = new ShoppingCart()
				{
					Id = item.Id,
					Name = item.name,
					Description = item.discription,
					price = item.price,
					discount = item.Discount,
					Size = item.Size,
					Qyt = 1,
					ImageUrl = item.imageUrl,
					UserId = item.UserId,
					ProductId = item.ProductId,
					
					
				};
				var AddToShoppiing =await ApiServices.ApiSerives.AddFromFavourites(shopping);
				if (AddToShoppiing)
				{
					_SharedServices.UpdateShoppingCart(shopping);
					ObsFavouirte.Remove(item);
                    BadgeCounterService.SetCount(3,ObsFavourite.Count);
                }
            }
        }
        private async void DeleteFromFavourite(AddToFavourite product)
        {
            var DeleteFav = _SharedServices.ObsFavourite.FirstOrDefault(D => D.ProductId == product.ProductId);
            if (DeleteFav != null)
            {
                _SharedServices.ObsFavourite.Remove(DeleteFav);
                BadgeCounterService.SetCount(3, _SharedServices.ObsFavourite.Count);
                var response = await ApiServices.ApiSerives.RemoveFromFavorites(Preferences.Get("user_Id", 0), product.ProductId);

            }
        }


        private void OnpropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public async void GetFavourite()
		{
		 var response =	await ApiServices.ApiSerives.GetFavouritesByuser(Preferences.Get("user_Id", 0));
			foreach (var item in response)
			{
				_SharedServices.ObsFavourite.Add(item);
                BadgeCounterService.SetCount(3, _SharedServices.ObsFavourite.Count);



            }
        }
		
		
	}
}

