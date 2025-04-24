using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
	public class BestSearchPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        public ObservableCollection<Product> BestSearchProdcutSource { get; set; }
        private BestSearch _bestSearch;
        private Product _BestSearchProductSelectedItem;
        private string _TitleName;


        public string TitleName
        {
            get => _TitleName;
            set
            {
                _TitleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }
        public Product BestSearchProductSelectedItem
        {
            get => _BestSearchProductSelectedItem;
            set
            {
                if (_BestSearchProductSelectedItem != value)
                {
                    _BestSearchProductSelectedItem = value;
                    OnPropertyChanged(nameof(BestSearchProductSelectedItem));
                    OnItemSeleted(_BestSearchProductSelectedItem);
                }
            }
        }

        private void OnItemSeleted(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductHomePage(product));
                BestSearchProductSelectedItem = null;
            }
        }

        public BestSearchPageViewModel(INavigation navigation,BestSearch bestSearch)
		{
            _navigation = navigation;
            BestSearchProdcutSource = new ObservableCollection<Product>();
            _bestSearch = bestSearch;
            TitleName = _bestSearch.name;

            ShowBestSearch();

        }
        private async void ShowBestSearch()
        {
          var response =   await ApiServices.ApiSerives.GetBestSearchProduct(_bestSearch.name);
            foreach (var item in response)
            {
                BestSearchProdcutSource.Add(item);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

