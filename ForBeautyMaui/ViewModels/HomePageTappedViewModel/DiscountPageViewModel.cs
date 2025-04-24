using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;


namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
	public class DiscountPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        private string _TitleName;
        private bool _StackActivityVisbilty = true;
        private Product _DiscountProductSelectedItem;
        public ObservableCollection<Product> DiscountProductSource { get; set; }

        public string TitleName
        {
            get => _TitleName;
            set
            {
                _TitleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }
        public Product DiscountProductSelectedItem
        {
            get => _DiscountProductSelectedItem;
            set
            {
                if (_DiscountProductSelectedItem != value)
                {
                    _DiscountProductSelectedItem = value;
                    OnPropertyChanged(nameof(DiscountProductSelectedItem));
                    OnItemSeleted(_DiscountProductSelectedItem);
                }
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

        private async void OnItemSeleted(Product product)
        {
            if (product != null)
            {
                await _navigation.PushAsync(new DetailViewProductHomePage(product));
                DiscountProductSelectedItem = null;
            }
        }

        public DiscountPageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            DiscountProductSource = new ObservableCollection<Product>();
            TitleName = "التخفيضات";
            ShowDiscountProduct();

        }



        private async void ShowDiscountProduct()
        {
            StackActivityVisbilty = true;
            var response = await ApiSerives.GetDiscountProduct();
            foreach (var item in response)
            {
                DiscountProductSource.Add(item);
            }
            await Task.Delay(100);
            StackActivityVisbilty = false;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

