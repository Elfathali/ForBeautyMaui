using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ApiServices;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
	public class BestCategoryPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private HomePageViewModel _HomePageViewModel;
        public SharedServicesViewModel _sharedServices;
        private INavigation _navigation;
        private bool ResetProduct = false;
        private int DefultPrice;
        private string DefultSize;
        private int DefultDiscount;
        private int _CategoryId;
        private string _TitleName;
        private Product _ProductListSelectedItem;
        public ObservableCollection<Product> ProductListBestCategory { get; set; } = new ObservableCollection<Product>();

        public string TitleName
        {
            get => _TitleName;
            set
            {
                _TitleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }
        public Product ProductListSelectedItem
        {
            get => _ProductListSelectedItem;
            set
            {
                if (_ProductListSelectedItem != value)
                {
                    _ProductListSelectedItem = value;
                    OnPropertyChanged(nameof(ProductListSelectedItem));
                    OnItemselected(_ProductListSelectedItem);
                }
            }
        }
        

        private async void OnItemselected(Product product)
        {
            if (product != null)
            {
                if (!ResetProduct)
                {
                    DefultPrice = product.price;
                    DefultSize = product.Size;
                    DefultDiscount = product.discount;

                }
                else
                {
                    product.price = DefultPrice;
                    product.Size = DefultSize;
                    product.discount = DefultDiscount;

                }

                var CheckAccessToken = Preferences.Get("access_token", string.Empty);
                if (CheckAccessToken != string.Empty)
                {
                    var res = new RecentlyViewd()
                    {
                        ProductId = product.Id,
                        UserId = Preferences.Get("user_Id", 0)
                    };
                    var checkRes =  _HomePageViewModel.RecentlyViewd.FirstOrDefault(p => p.Id == product.Id);
                    if (checkRes != null)
                    {
                        _HomePageViewModel.RecentlyViewd.Remove(checkRes);

                        _HomePageViewModel.RecentlyViewd.Insert(0, product);
                    }
                    else
                    {
                        
                        _HomePageViewModel.RecentlyViewd.Insert(0, product);
                    }
                    await ApiSerives.PostRecentlyViewd(res);
                }
                await _navigation.PushAsync(new DetailViewProductHomePage(product));
                    ResetProduct = true;
                    ProductListSelectedItem = null;
               


            }
            
        }

        public BestCategoryPageViewModel(INavigation navigation, Categories categories , HomePageViewModel homePageViewModel , SharedServicesViewModel sharedServices )
		{
            _sharedServices = sharedServices;
            _CategoryId = categories.id;
            _navigation = navigation;
            ProductListBestCategory = new ObservableCollection<Product>();
            GetListBestCatogires(_CategoryId);
             TitleName = categories.CategoryName;
            _HomePageViewModel = homePageViewModel;

        }


        private async void GetListBestCatogires(int Id)
        {
           var response = await ApiSerives.ShowBestCategoryProduct(Id);
            foreach (var item in response)
            {
                ProductListBestCategory.Add(item);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}

