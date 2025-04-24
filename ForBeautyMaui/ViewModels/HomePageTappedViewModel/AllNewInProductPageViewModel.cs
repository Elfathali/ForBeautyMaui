using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ApiServices;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
	public class AllNewInProductPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private string _TitleName;
        private INavigation _navigation;
        private Product _ProductListSelectedIte;
        public Product ProductListSelectedIte
        {
            get => _ProductListSelectedIte;
            set
            {
                if (_ProductListSelectedIte != value)
                {
                    _ProductListSelectedIte = value;
                    OnPropertyChanged(nameof(ProductListSelectedIte));
                    OnItemSelecte(_ProductListSelectedIte);
                }
            }
        }

        private void OnItemSelecte(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductHomePage(product));
                ProductListSelectedIte = null;
            }
        }

        public string TitleName
        { get => _TitleName;
            set
            {
                _TitleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }



        public ObservableCollection<Product> ProductList { get; set; }

        public AllNewInProductPageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            ProductList = new ObservableCollection<Product>();
            TitleName = "جديد لدينا";
            ShowAllNewIn();

        }

        private async void ShowAllNewIn()
        {
            var response = await ApiSerives.ShowAllNewIn();
            foreach (var item in response)
            {
                ProductList.Add(item);
            }

        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

