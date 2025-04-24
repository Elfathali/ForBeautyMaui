using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.Searchpages;

namespace ForBeautyMaui.ViewModels.SearchPageTappedviewModel
{
	public class ProductByCategoryPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        public SharedServicesViewModel _SharedServices;
        private Product _ProductListSelectedItem;
        private bool _StackActivityVisbilty = true;
        private bool ResetProduct = false;
        private int DefultPrice;
        private string DefultSize;
        private int DefultDiscount;
        private INavigation _navigation;
        private string _TitleName;
        public ObservableCollection<Product> ProductListSource { get; set; } = new ObservableCollection<Product>();

        public Product ProductListSelectedItem
        {
            get => _ProductListSelectedItem;
            set
            {
                _ProductListSelectedItem = value;
                OnPropertyChanged(nameof(ProductListSelectedItem));
                OnitemSelected(_ProductListSelectedItem);
            }
        }
        public bool StackActivityVisbilty
        {
            get => _StackActivityVisbilty;
            set
            {
                _StackActivityVisbilty = value;
                OnPropertyChanged(nameof(StackActivityVisbilty));
            }
        }



        private async void OnitemSelected(Product product )
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
                await _navigation.PushAsync(new DetailViewProductSearchPage(product));

                var CheckAccessToken = Preferences.Get("access_token", string.Empty);
                if (CheckAccessToken != string.Empty)
                {
                    var res = new RecentlyViewd()
                    {
                        ProductId = product.Id,
                        UserId = Preferences.Get("user_Id", 0)
                    };
                    var checkRes =_SharedServices.RecentlyViewd.FirstOrDefault(p => p.Id == product.Id);
                    if (checkRes != null)
                    {
                        _SharedServices.RecentlyViewd.Remove(checkRes);

                        _SharedServices.RecentlyViewd.Insert(0, product);
                    }
                    else
                    {

                        _SharedServices.RecentlyViewd.Insert(0, product);
                    }
                    await ApiSerives.PostRecentlyViewd(res);
                }
                ResetProduct = true;
                ProductListSelectedItem = null;



            }
        }

        public string TitleName
        {
            get => _TitleName;
            set
            {
                if (_TitleName != value)
                {
                    _TitleName = value;
                    OnPropertyChanged(nameof(TitleName));
                }
            }
        }

        public ProductByCategoryPageViewModel(INavigation navigation, Categories categories , CategorDital categorDital , string CategoryName , SharedServicesViewModel sharedServices)
		{
            _navigation = navigation;
            _SharedServices = sharedServices;
            ProductListSource = new ObservableCollection<Product>();
            GetProductByCategory(categories);
            GetCategoryDetailByName(categorDital);
            TitleName = CategoryName;

        }

        private async void GetProductByCategory(Categories categories)
        {
            if (categories != null)
            {
                StackActivityVisbilty = true;
                var response = await ApiSerives.ShowBestCategoryProduct(categories.id);
                foreach (var item in response)
                {
                    ProductListSource.Add(item);
                }
                await Task.Delay(100);
                StackActivityVisbilty = false;
            }
        }
        private async void GetCategoryDetailByName(CategorDital categorDital)
        {
            if (categorDital != null)
            {
                StackActivityVisbilty = true;

                var response = await ApiSerives.GetProductCategory(categorDital.nameDital, categorDital.categorId);
                foreach (var item in response)
                {
                    ProductListSource.Add(item);
                }
                await Task.Delay(100);
                StackActivityVisbilty = false;
            }

        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

