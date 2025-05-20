using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ApiServices;
using ForBeautyMaui.ViewPages;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
    public partial class DetailViewProductHomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MoreOptionsPageViewModel _MoreOptionPageViewModel;
        private SharedServicesViewModel _sharedServices;
        private bool _BoxViewPriceLineHomePage;
        public Command<Product> CommandAddToFavourite { get; set; }
        private bool _stackdiscountHomePage;
        private INavigation _navigation;
        private string _MoreDeatilTitle;
        private bool _StackMoreOptionVisible;
        private Product _CollectionSeeMoreSelectedItem;
        private string _FavouriteSource = "favorite";
        private string _LblMoreOptionCount;
        public ObservableCollection<ImageModel> CarouselImages { get; set; }
        public ObservableCollection<Product> CollectionSeeMore { get; set; }
        public ObservableCollection<OtherDesginProduct> MoreOptionsList { get; set; }
        public Command CommandMoreOptionProduct { get; set; }
        public Command ButtonAddToShopping { get; set; }

        private Product _product;
        public string MoreDeatilTitle
        {
            get => _MoreDeatilTitle;
            set
            {
                if (_MoreDeatilTitle != value)
                {
                    _MoreDeatilTitle = value;
                    OnPropertyChanged(nameof(MoreDeatilTitle));
                }
            }
        }
        public bool StackMoreOptionVisible
        {
            get => _StackMoreOptionVisible;
            set
            {
                if (_StackMoreOptionVisible != value)
                {
                    _StackMoreOptionVisible = value;
                    OnPropertyChanged(nameof(StackMoreOptionVisible));
                }
            }
        }
        public Product CollectionSeeMoreSelectedItem
        {
            get => _CollectionSeeMoreSelectedItem;
            set
            {
                _CollectionSeeMoreSelectedItem = value;
                OnPropertyChanged(nameof(CollectionSeeMoreSelectedItem));
                OnitemSeclted(_CollectionSeeMoreSelectedItem);
            }

        }
        private void OnitemSeclted(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductHomePage(product));
                CollectionSeeMoreSelectedItem = null;

            }
        }


            public string LblMoreOptionCount
            {
            get => _LblMoreOptionCount;
            set
            {
                if (_LblMoreOptionCount != value)
                {
                    _LblMoreOptionCount = value;
                    OnPropertyChanged(nameof(LblMoreOptionCount));
                }
            }
        }
        public int LbllIdProduct
        {
            get => _product.Id;
            set
            {
                if (_product.Id != value)
                {
                    _product.Id = value;
                    OnPropertyChanged(nameof(LbllIdProduct));
                }
            }
        }
        public string LblNameProduct
        {
            get => _product.name;
            set
            {
                if (_product.name != value)
                {
                    _product.name = value;
                    OnPropertyChanged(nameof(LblNameProduct));
                }
            }
        }
        public string LblCategoryProduct
        {
            get => _product.categoryName;
            set
            {
                if (_product.categoryName != value)
                {
                    _product.categoryName = value;
                    OnPropertyChanged(nameof(LblCategoryProduct));
                }
            }
        }
        public string LblDiscreption
        {
            get => _product.ProductDiscription;
            set
            {
                if (_product.ProductDiscription != value)
                {
                    _product.ProductDiscription = value;
                    OnPropertyChanged(nameof(LblDiscreption));
                }
            }
        }
        public int Id
        {
            get => _product.Id;
            set
            {
                _product.Id = value;
                OnPropertyChanged(nameof(Id));

            }
        }
        public string ImageUrl
        {
            get => _product.imageUrl;
            set
            {
                _product.imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));

            }
        }
        public string Name
        {
            get => _product.name;
            set
            {
                _product.name = value;
                OnPropertyChanged(nameof(Name));

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
        {
            get => _stackdiscountHomePage;
            set
            {
                _stackdiscountHomePage = value;
                OnPropertyChanged(nameof(stackdiscountHomePage));
            }
        }

        public string FavouriteSource
        {
            get => _FavouriteSource;
            set
            {
                _FavouriteSource = value;
                OnPropertyChanged(nameof(FavouriteSource));
            }
        }
        public DetailViewProductHomePageViewModel( INavigation navigation, Product product , SharedServicesViewModel sharedServices)
		{
            _sharedServices = sharedServices;
            _navigation = navigation;
            _product = product;
            MoreDeatilTitle = product.MoreDital;
            CommandAddToFavourite  = new Command<Product>((item) => OnAddFavouriteTap(product));
            CarouselImages = new ObservableCollection<ImageModel>();
            CollectionSeeMore = new ObservableCollection<Product>();
            MoreOptionsList = new ObservableCollection<OtherDesginProduct>();
            CommandMoreOptionProduct = new Command(GetMoreOptionProduct);
            ButtonAddToShopping = new Command(AddToShopping);
            ShowImagesById();
            ProductUMayLike(product);
            CheckIfInFavourite(product);
            MoreOption(product.Id);
        }
        private void CheckIfInFavourite(Product product)
        {
            var chekfavourite = _sharedServices.ObsFavourite.FirstOrDefault(P => P.ProductId == product.Id);

            if (chekfavourite != null)
            {
                FavouriteSource = "favorite_red.png";
            }
            else
            {
                FavouriteSource = "favorite.png";
            }
        }

        private async void OnAddFavouriteTap(Product product)
        {
            if (product != null)
            {
                var checkfavourite = _sharedServices.ObsFavourite.FirstOrDefault(P => P.ProductId == product.Id);
                
                if (checkfavourite == null)
                {

                    AddToFavourite AddFavourite = new AddToFavourite()
                    {
                        categoryName = product.categoryName,
                        Discount = product.discount,
                        discription = product.ProductDiscription,
                        MoreDital = product.MoreDital,
                        imageUrl = product.imageUrl,
                        name = product.name,
                        price = product.price,
                        UserId = Preferences.Get("user_Id", 0),
                        Size = product.Size,
                        ProductId = product.Id
                    };

                    FavouriteSource = "favorite_red.png";
                    _sharedServices.ObsFavourite.Add(AddFavourite);
                    BadgeCounterService.SetCount(3, _sharedServices.ObsFavourite.Count);
                    HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                    await ApiSerives.PostFavourite(AddFavourite);

                }
                else
                {
                    var DeleteFav = _sharedServices.ObsFavourite.FirstOrDefault(D => D.ProductId == product.Id);
                    if (DeleteFav != null)
                    {
                        FavouriteSource = "favorite.png";
                        _sharedServices.ObsFavourite.Remove(DeleteFav);
                        BadgeCounterService.SetCount(3, _sharedServices.ObsFavourite.Count);
                        var response = await ApiSerives.RemoveFromFavorites(Preferences.Get("user_Id", 0), product.Id);
                    }
                }
            }
        }
        private async void ProductUMayLike(Product product)
        {
            var response = await ApiSerives.SeeMoreProduct(product.categoryId, product.Id);
            foreach (var item in response)
            {
                CollectionSeeMore.Add(item);
            }
        }
        private async void AddToShopping()
        {
            var CheckAccessToken = Preferences.Get("access_token", string.Empty);
            if (CheckAccessToken == string.Empty)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new WelcomePage()));
                return;
            }
            //lottieLoading.IsVisible = true;
            ButtonAddToShopping = null;
            var AddShopping = new ShoppingCart()
            {
                Name = Name,
                ImageUrl = ImageUrl,
                price = Convert.ToDouble(price),
                discount = Convert.ToDouble(discount),
                Qyt = 1,
                Size = size,
                UserId = Preferences.Get("user_Id", 0),
                ProductId = Convert.ToInt32(Id)
            };
                var response = await ApiSerives.AddToShoppingCart(AddShopping);
                if (response)
                {
                  _sharedServices.UpdateShoppingCart(AddShopping);
                }
        }

        private async void GetMoreOptionProduct()
        {
           // await PopupNavigation.Instance.PushAsync(new MoreOptionsPage(MoreOptionsList , LblMoreOptionCount , this));
        }

        private async void MoreOption(int id)
        {
            var respnse = await ApiSerives.OtherDesginP(id);
            
            foreach (var item in respnse)
            {
                
                MoreOptionsList.Add(item);
                MoreOptionsList[0].IsSelected = true;
                
            }
            if (MoreOptionsList.Count -1 >= 1)
            {
                StackMoreOptionVisible = true;
                string currentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                LblMoreOptionCount = currentLanguage == "ar" ?  "▼    " + (int)( MoreOptionsList.Count -1) + "  أصناف أخري متوفرة" : "▼    " + (int)(MoreOptionsList.Count - 1) +" other designs available";
            }
            else
            {
                StackMoreOptionVisible = false;
            }
           
        }
        private async void ShowImagesById()
        {

            var response = await ForBeautyMaui.ApiServices.ApiSerives.GetDitalImages(_product.Id);
            foreach (var item in response)
            {
                CarouselImages.Add(item);
            }
        }
       

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

