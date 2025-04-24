using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
    public class NewsProductPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation _navigation;
        public ObservableCollection<Product> ObsExclosve { get; set; }
        private Product _NewProductSeletedItem;
        private string _TitleName;
        private string _ImageExclosve;
        public string ImageExclosve
        {
            get => _ImageExclosve;
            set
            {
                _ImageExclosve = value;
                OnPropertyChanged(nameof(ImageExclosve));
                
            }
        }

        public Product NewProductSeletedItem
        {
            get => _NewProductSeletedItem;
            set
            {
                _NewProductSeletedItem = value;
                OnPropertyChanged(nameof(NewProductSeletedItem));
                onItemSeleted(_NewProductSeletedItem);
            }
        }

        private void onItemSeleted(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductHomePage(product));
                NewProductSeletedItem = null;
            }
        }

        public string TitleName
        {
            get => _TitleName;
            set
            {
                _TitleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }

        public NewsProductPageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            ObsExclosve = new ObservableCollection<Product>();
            GetNewsProduct();
            GetExclosveDesgin();

        }

        private void NewProductSeleted(object obj)
        {
            throw new NotImplementedException();
        }

        private async void GetNewsProduct()
        {

            var response = await ApiSerives.GetExclosveProduct();
            foreach (var item in response)
            {
                ObsExclosve.Add(item);
            }
        }
        private async void GetExclosveDesgin()
        {
            var response = await ApiSerives.GetExclosveDesgin();
            ImageExclosve = response.ImageUrl;
            TitleName = response.Title;
            if (response.IsAvalible == true)
            {
                //ButtonNameExc = response.Title;
            //    LblTextAd.Text = response.Title;
            //    BoxViewMainImage.IsVisible = true;

            }
            else
            {
            //    ButtonNameExc.IsVisible = false;
            //    BoxViewMainImage.IsVisible = false;
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

