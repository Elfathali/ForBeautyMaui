using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ViewModels.HomePageTappedViewModel;
using ForBeautyMaui.ViewPages.MainTappedPage.Searchpages;
using System;
using ForBeautyMaui.ViewPages;
using System.Linq;
using ForBeautyMaui.ApiServices;
using ForBeautyMaui.ViewPages;


namespace ForBeautyMaui.ViewModels.SearchPageTappedviewModel
{
    public class DetailViewProductSearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MoreOptionsPageViewModel _MoreOptionPageViewModel;
        private INavigation _navigation;
        private string _FavouriteSource = "favorite";
        public Command ButtonAddToShopping { get; set; }
        private SharedServicesViewModel _SharedServices;
        private string _MoreDeatilTitle;
        private bool _StackMoreOptionVisible;
        private Product _CollectionSeeMoreSelectedItem;
        private string _LblMoreOptionCount;
        public ObservableCollection<ImageModel> CarouselImages { get; set; }
        public ObservableCollection<OtherDesginProduct> MoreOptionsList { get; set; }
        public ObservableCollection<Product> CollectionSeeMore { get; set; }
        public Command CommandMoreOptionProduct { get; set; }
        public Command<Product> CommandTapHearth { get; set; }
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

        public Product CollectionSeeMoreSelectedItems
        {
            get => _CollectionSeeMoreSelectedItem;
            set
            {
                _CollectionSeeMoreSelectedItem = value;
                OnPropertyChanged(nameof(CollectionSeeMoreSelectedItems));
                OnItemSelected(_CollectionSeeMoreSelectedItem);
            }
        }
        public void OnItemSelected(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductSearchPage(product));
                CollectionSeeMoreSelectedItems = null;
                
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
        public string MoreDital
        {
            get => _product.MoreDital;
            set
            {
                if (_product.MoreDital != value)
                {
                    _product.MoreDital = value;
                    OnPropertyChanged(nameof(MoreDital));
                }
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



        public string ImageUrl
        {
            get => _product.imageUrl;
            set
            {
                _product.imageUrl = value;
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
        public int Id
        {
            get => _product.Id;
            set
            {
                _product.Id = value;
                OnPropertyChanged(nameof(Id));

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



        public DetailViewProductSearchViewModel(INavigation navigation, Product product , SharedServicesViewModel sharedServices)
        {
            _navigation = navigation;
            _product = product;
            _SharedServices = sharedServices;
            MoreDeatilTitle = product.MoreDital;
            CarouselImages = new ObservableCollection<ImageModel>();
            CollectionSeeMore = new ObservableCollection<Product>();
            MoreOptionsList = new ObservableCollection<OtherDesginProduct>();
            CommandMoreOptionProduct = new Command(GetMoreOptionProduct);
            CommandTapHearth = new Command<Product>((item) => CommandAddToFavourite(product));
            ButtonAddToShopping = new Command(AddToShopping);
            ShowImagesById();
            CheckIfInFavourite(product);
            MoreOption(product.Id);
            ProductUMayLike(product);
        }
        private void CheckIfInFavourite( Product product)
        {
            var chekfavourite = _SharedServices.ObsFavourite.FirstOrDefault(P => P.ProductId == product.Id);

            if (chekfavourite != null)
            {
                FavouriteSource = "favorite_red.png";
            }
            else
            {
                FavouriteSource = "favorite.png";
            }
        }
        private async void ProductUMayLike(Product product)
        {
           var response = await ApiSerives.SeeMoreProduct(product.categoryId,product.Id);
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
                _SharedServices.UpdateShoppingCart(AddShopping);
                }
        }

        private  async void CommandAddToFavourite(Product product)
        {
            if (product != null)
            {
                var checkfavourite = _SharedServices.ObsFavourite.FirstOrDefault(P => P.ProductId == product.Id);

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
                    _SharedServices.ObsFavourite.Add(AddFavourite);
                    BadgeCounterService.SetCount(3, _SharedServices.ObsFavourite.Count);
                    HapticFeedback.Default.Perform(HapticFeedbackType.Click);

                    await ApiSerives.PostFavourite(AddFavourite);

                }
                else
                {
                    var DeleteFav = _SharedServices.ObsFavourite.FirstOrDefault(D => D.ProductId == product.Id);
                    if (DeleteFav != null)
                    {
                        FavouriteSource = "favorite.png";
                        _SharedServices.ObsFavourite.Remove(DeleteFav);
                        BadgeCounterService.SetCount(3, _SharedServices.ObsFavourite.Count);
                        var response = await ApiSerives.RemoveFromFavorites(Preferences.Get("user_Id", 0), product.Id);
                    }
                }
            }
        }

        private async void GetMoreOptionProduct()
        {
           // await PopupNavigation.Instance.PushAsync(new MoreOptionSearchPage(MoreOptionsList, LblMoreOptionCount, this));
        }

        private async void MoreOption(int id)
        {
            var respnse = await ApiSerives.OtherDesginP(id);


            foreach (var item in respnse)
            {

                MoreOptionsList.Add(item);
                MoreOptionsList[0].IsSelected = true;

            }
            if (MoreOptionsList.Count - 1 >= 1)
            {
                StackMoreOptionVisible = true;
                LblMoreOptionCount = "▼    " + (int)(MoreOptionsList.Count - 1) + "  أصناف أخري متوفرة";
            }
            else
            {
                StackMoreOptionVisible = false;
            }

        }
        private async void ShowImagesById()
        {

            var response = await ApiSerives.GetDitalImages(_product.Id);
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

