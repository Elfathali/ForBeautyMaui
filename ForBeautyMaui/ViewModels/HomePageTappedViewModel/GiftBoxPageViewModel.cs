using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;


namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
    public class GiftBoxPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _BoxViewPriceLineHomePage;
        private bool _stackdiscountHomePage;
        public ObservableCollection<ImageModel> CarouselImages { get; set; }

        private Product _product;



        public string ImageUrl
        {
            get => _product.imageUrl;
            set
            {
                _product.ImageUrlForGift = value;
                OnPropertyChanged(nameof(ImageUrl));

            }
        }
        


        public int price
        {
            get => _product.price;
            set
            {
                _product.price = value;
                OnPropertyChanged(nameof(price));
                
            }
        }
        public string size
        {
            get => _product.Size;
            set
            {
                _product.Size = value;
                OnPropertyChanged(nameof(size));

            }
        }
        public int discount
        {
            get => _product.discount;
            set
            {
                _product.discount = value;
                OnPropertyChanged(nameof(discount));

            }
        }
        public bool BoxViewPriceLineHomePage
        {
            get => _BoxViewPriceLineHomePage;
            set
            {
                _BoxViewPriceLineHomePage = value;
                OnPropertyChanged(nameof(BoxViewPriceLineHomePage));
            }
        }
        public bool stackdiscountHomePage
        { get => _stackdiscountHomePage;
            set
            {
                _stackdiscountHomePage = value;
                OnPropertyChanged(nameof(stackdiscountHomePage));
            }
        }
        
        

        public GiftBoxPageViewModel(Product product)
        {

            _product = product;
            CarouselImages = new ObservableCollection<ImageModel>();
            EditView();
            ShowImagesById();


        }
        private void EditView()
        {
            BoxViewPriceLineHomePage = _product.discount != 0;
            stackdiscountHomePage = _product.discount != 0;

            

        }
        private async void ShowImagesById()
        {
           var response = await ApiServices.ApiSerives.GetDitalImages(_product.Id);
            foreach (var item in response)
            {
                CarouselImages.Add(item);
            }
        }


        // Notify property changes
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

