using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.Resources.Languages;
using ForBeautyMaui.ViewPages.MainTappedPage.HomePages;
using ForBeautyMaui.Renders;
using ForBeautyMaui.ViewPages.MainTappedPage;


namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
     public class HomePageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public SharedServicesViewModel _SharedServices;
        private string _ImageMainPage;
        private bool _ButtonNameExcVisible;
        private string _ButtonNameExcText;
        private string _LblWelcomeNameHomePage;
        private bool _StackRecentltyView;
        private Product _RecentlyViewdSelectedItem;
        private string _LblBrand1;
        private string _LblBrand2;
        private string _LblBrand3;
        private string _LblBrand4;
        private string _LblBrand5;
        private string _LblBrand6;
        private double _ScalexDireaction;
        private LayoutOptions _BoxViewNewIn;
        private Product _ProductListBestCategory;

        private Categories _selectedCategory;
        private Product _CollectionGiftBoxSelectedItem;
        private BestSearch _BestSearchSelectedItem;
        private bool _GridLoaderHomePage = true;
        private bool _BoxViewMainImageVisiblity;
        private FlowDirection _FlowDirection;
        public ObservableCollection<Product> RecentlyViewd => _SharedServices.RecentlyViewd;
        public ObservableCollection<BestSearch> CollectionSearchProduct { get; set; }
        public ObservableCollection<Product> CollectionGiftBox { get; set; }
        public ObservableCollection<Categories> CollectionBestCategorySource { get; set; }
        public ObservableCollection<Product> ObsExclosve { get; set; }
        public ObservableCollection<Product> CarouselNewIn { get; set; }
        public ObservableCollection<Product> ShowDiscount { get; set; }
        public Command BtnBoxTap { get; set; }
        public Command boxShowAllNewInTapped { get; set; }
        public Command<Product> CarouselViewImageTapped { get; }
        public Command TapShowDiscountTapped { get; }
        public Command BrandTextBox1 { get; set; }
        public Command BrandTextBox2 { get; set; }
        public Command BrandTextBox3 { get; set; }
        public Command BrandTextBox4 { get; set; }
        public Command BrandTextBox5 { get; set; }
        public Command BrandTextBox6 { get; set; }
        private INavigation _navigation;

        public bool GridLoaderHomePage
        {
            get => _GridLoaderHomePage;
            set
            {
                if (_GridLoaderHomePage != value)
                {
                    _GridLoaderHomePage = value;
                    OnPropertyChanged(nameof(GridLoaderHomePage));
                }
            }
        }
        public bool BoxViewMainImageVisiblity
        {
            get => _BoxViewMainImageVisiblity;
            set
            {
                _BoxViewMainImageVisiblity = value;
                OnPropertyChanged(nameof(BoxViewMainImageVisiblity));
            }
        }


        public Product RecentlyViewdSelectedItem
        {
            get => _RecentlyViewdSelectedItem;
            set
            {
                if (_RecentlyViewdSelectedItem != value)
                {
                    _RecentlyViewdSelectedItem = value;
                    OnPropertyChanged(nameof(RecentlyViewdSelectedItem));
                    OnItemSeleted(_RecentlyViewdSelectedItem);
                }
            }
        }
        private async void CheckExclosveDesgin()
        {
            var response = await ApiSerives.GetExclosveDesgin();
            
            if (response.IsAvalible == true)
            {
                ButtonNameExcText = response.Title;

                BoxViewMainImageVisiblity = true;

            }
            else
            {
                //    ButtonNameExc.IsVisible = false;
                //    BoxViewMainImage.IsVisible = false;
            }
        }

        private void OnItemSeleted(Product product)
        {
            if (product != null)
            {
                _navigation.PushAsync(new DetailViewProductHomePage(product));
                RecentlyViewdSelectedItem = null;
            }
        }

        public string ImageMainPage
        {
            get => _ImageMainPage;
            set
            {
                if (_ImageMainPage != value)
                {
                    _ImageMainPage = value;
                    OnPropertyChanged(nameof(ImageMainPage));
                }
            }

        }
        public bool ButtonNameExcVisible
        {
            get => _ButtonNameExcVisible;
            set
            {
                if (_ButtonNameExcVisible != value)
                {
                    _ButtonNameExcVisible = value;
                    OnPropertyChanged(nameof(ButtonNameExcVisible));
                }
            }
        }
        public string ButtonNameExcText
        { get => _ButtonNameExcText;
            set
            {
                if (_ButtonNameExcText != value)
                {
                    _ButtonNameExcText = value;
                    OnPropertyChanged(nameof(ButtonNameExcText));
                }
            }
        }
        public bool StackRecentltyView
        {
            get => _StackRecentltyView;
            set
            {
                if (_StackRecentltyView != value)
                {
                    _StackRecentltyView = value;
                    OnPropertyChanged(nameof(StackRecentltyView));
                }
            }
        }
        
        public FlowDirection flowdirecation
        {
            get => _FlowDirection;
            set
            {
                if (_FlowDirection != value)
                {
                    _FlowDirection = value;
                    OnPropertyChanged(nameof(flowdirecation));
                }
            }
        }
        public double ScalexDireaction
        {
            get => _ScalexDireaction;
            set
            {
                _ScalexDireaction = value;
                OnPropertyChanged(nameof(ScalexDireaction));
            }
        }





        public Categories CollectionBestCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(CollectionBestCategory));
                    if (_selectedCategory != null)
                    {
                        OnCategorySelected(_selectedCategory);
                    }
                }
            }
        }
        public BestSearch BestSearchSelectedItems
        {
            get => _BestSearchSelectedItem;
            set
            {
                if (_BestSearchSelectedItem != value) // Ensure value is actually changing
                {
                    _BestSearchSelectedItem = value;
                    OnPropertyChanged(nameof(BestSearchSelectedItems)); // Notify UI about the change

                    // Call method when a new item is selected
                    if (_BestSearchSelectedItem != null)
                    {
                        OnItemSelected(_BestSearchSelectedItem); // Handle the item selection
                    }
                }
            }
        }
        public string LblWelcomeNameHomePage
        {
            get => _LblWelcomeNameHomePage;
            set
            {
                if (_LblWelcomeNameHomePage != value)
                {
                    _LblWelcomeNameHomePage = value;
                    OnPropertyChanged(nameof(LblWelcomeNameHomePage));
                }
            }
        }

        private async void OnItemSelected(BestSearch product)
        {
            if (product != null)
            {
                await _navigation.PushAsync(new BestSearchPage(product));
                BestSearchSelectedItems = null;
            }
        }



        public Product CollectionGiftBoxSelectedItem
        {
            get => _CollectionGiftBoxSelectedItem;
            set
            {
                _CollectionGiftBoxSelectedItem = value;
                OnPropertyChanged(nameof(CollectionGiftBoxSelectedItem));
                if (_CollectionGiftBoxSelectedItem != null)
                {
                    OnGiftBoxSelected(_CollectionGiftBoxSelectedItem);
                }
            }
        }
        public string LblBrand1
        {
            get => _LblBrand1;
            set
            {
                _LblBrand1 = value;
                OnPropertyChanged(nameof(LblBrand1));
            }
        }
        public string LblBrand2
        {
            get => _LblBrand2;
            set
            {
                _LblBrand2 = value;
                OnPropertyChanged(nameof(LblBrand2));
            }
        }
        public string LblBrand3
        {
            get => _LblBrand3;
            set
            {
                _LblBrand3 = value;
                OnPropertyChanged(nameof(LblBrand3));
            }
        }

        public string LblBrand4
        {
            get => _LblBrand4;
            set
            {
                _LblBrand4 = value;
                OnPropertyChanged(nameof(LblBrand4));
            }
        }
        public string LblBrand5
        {
            get => _LblBrand5;
            set
            {
                _LblBrand5 = value;
                OnPropertyChanged(nameof(LblBrand5));
            }
        }
        public string LblBrand6
        {
            get => _LblBrand6;
            set
            {
                _LblBrand6 = value;
                OnPropertyChanged(nameof(LblBrand6));
            }
        }




        private async void OnGiftBoxSelected(Product product)
        {
            await _navigation.PushAsync(new DetailViewProductHomePage(product));
            CollectionGiftBoxSelectedItem = null;
            var CheckAccessToken = Preferences.Get("access_token", string.Empty);
            if (CheckAccessToken != string.Empty)
            {
                var res = new RecentlyViewd()
                {
                    ProductId = product.Id,
                    UserId = Preferences.Get("user_Id", 0)
                };
                var checkRes = RecentlyViewd.FirstOrDefault(p => p.Id == product.Id);
                if (checkRes != null)
                {
                   RecentlyViewd.Remove(checkRes);

                   RecentlyViewd.Insert(0, product);
                }
                else
                {

                  RecentlyViewd.Insert(0, product);
                }
                await ApiSerives.PostRecentlyViewd(res);
            }


        }
        

        private async void OnCategorySelected(Categories categories)
        {
            await _navigation.PushAsync(new BestCategoryPage(categories,this));
                
            CollectionBestCategory = null;

        }
        public Product ProductListBestCategory
        {
            get => _ProductListBestCategory;
            set
            {
                _ProductListBestCategory = value;
                OnPropertyChanged(nameof(ProductListBestCategory));
                
            }
        }

        public HomePageViewModel(INavigation navigation , SharedServicesViewModel sharedServices)
		{
            _navigation = navigation;
            _SharedServices = sharedServices;
            CollectionBestCategorySource = new ObservableCollection<Categories>();
            CollectionGiftBox = new ObservableCollection<Product>();
            CollectionSearchProduct = new ObservableCollection<BestSearch>();
            CarouselNewIn = new ObservableCollection<Product>();
            CarouselViewImageTapped = new Command<Product>(CarouselViewImagetap);
            boxShowAllNewInTapped = new Command(ShowAllNewIn);
            BtnBoxTap = new Command(OnBoxTap);
            ObsExclosve = new ObservableCollection<Product>();
            TapShowDiscountTapped = new Command(ShowDiscountProduct);
            BrandTextBox1 = new Command(GetbestBrand1);
            BrandTextBox2 = new Command(GetbestBrand2);
            BrandTextBox3 = new Command(GetbestBrand3);
            BrandTextBox4 = new Command(GetbestBrand4);
            BrandTextBox5 = new Command(GetbestBrand5);
            BrandTextBox6 = new Command(GetbestBrand6);
            GetWelcomeName();
            flowdirections();
            GetBestSearchHome();
            //GetRecentlyViewd();
            DisplayGetBestCategories();
            DisplayGiftBox();
            GetExclosveDesgin();
            GetBrandsName();
            GetNewIn();
            CheckExclosveDesgin();
            GetToken();
            BadgeCounterService.SetCount(2);

            //CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;


        }
        private async void GetToken()
        {
            var GetToken = new PushNotificationUserToken()
            {
                UserId = Preferences.Get("user_Id", 0),
                PushNotificationToken = Preferences.Get("PushNotificationToken", string.Empty)
            };
            Preferences.Set("IsRegister", true);
            var reponse = await ApiSerives.GetPushNotificationTokenUser(GetToken);
        }



        //private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        //{
        //    if (e.Data.ContainsKey("PrepareOrder"))
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            //App.Current.MainPage.DisplayAlert("", "preparorder", "Ok");
        //            //SystemSound.Vibrate.PlayAlertSound();
        //            //StepProgressBar.StepSelected = 2;
        //            //stepbar1.StepSelected = 2;
        //            //stackReviewOrder.IsVisible = false;
        //            //StackOrderCanceled.IsVisible = false;
        //            //StackPrepareOrder.IsVisible = true;
        //            //PrepareOrderAnimation.PlayAnimation();
        //            //carBeauty.StopAnimation();
        //            //OrderScan.StopAnimation();
        //            //lblDescriptionOrderDelivery.Text = "تمت الموافقة علي طلبك, يتم الان تجهيز طلبك";
        //        });
        //    }
        //    else if (e.Data.ContainsKey("StartDeleviry"))
        //    {

        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            //StackPrepareOrder.IsVisible = false;
        //            //stackReviewOrder.IsVisible = false;
        //            //StackOrderCanceled.IsVisible = false;
        //            //stackCarBeauty.IsVisible = true;
        //            //lblDescriptionOrderDelivery.Text = "جاري توصيل طلبك";
        //            //StepProgressBar.StepSelected = 3;
        //            //stepbar1.StepSelected = 3;
        //            //PrepareOrderAnimation.StopAnimation();
        //            //OrderScan.StopAnimation();
        //            //carBeauty.PlayAnimation();
        //            //SystemSound.Vibrate.PlayAlertSound();
        //             App.Current.MainPage.DisplayAlert("", "startDeleivery", "Ok");
        //        });
        //    }
        //    else if (e.Data.ContainsKey("OrderComplete"))
        //    {
        //        //Device.BeginInvokeOnMainThread(() =>
        //        {
        //            App.Current.MainPage.DisplayAlert("", "Completed", "Ok");
        //            //StepProgressBar.StepSelected = 4;
        //            //stepbar1.StepSelected = 4;
        //            //lblDescriptionOrderDelivery.Text = "تم تسليم الطلب";
        //            //stackReviewOrder.IsVisible = false;
        //            //StackPrepareOrder.IsVisible = false;
        //            //stackCarBeauty.IsVisible = false;
        //            //StackOrderCanceled.IsVisible = false;
        //            //StackOrderCompleted.IsVisible = true;
        //            //OrderCompletedAnimation.PlayAnimation();
        //            //OrderCompletedAnimation.RepeatCount = 3;
        //            //SystemSound.Vibrate.PlayAlertSound();
        //            //OrderCompletedAnimation.StopAnimation();
        //            //var animation = new Animation(v => StackOrderState.Opacity = v, 1, 0);
        //            //animation.Commit(StackOrderState, "FadeOutAnimation", length: 5000, easing: Easing.Linear, finished: (v, c) => StackOrderState.IsVisible = false);
        //            //var animation1 = new Animation(v => StackOrderState1.Opacity = v, 1, 0);
        //            //animation.Commit(StackOrderState, "FadeOutAnimation", length: 5000, easing: Easing.Linear, finished: (v, c) => StackOrderState.IsVisible = false);
        //        };
        //    }
        //    else if (e.Data.ContainsKey("OrderCanceled"))
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            //StepProgressBar.IsVisible = false;
        //            //stepbar1.IsVisible = false;
        //            //lblDescriptionOrderDelivery.Text = "عذراً، تم إلغاء طلبك ";
        //            //stackReviewOrder.IsVisible = false;
        //            //StackPrepareOrder.IsVisible = false;
        //            //stackCarBeauty.IsVisible = false;
        //            //StackOrderCompleted.IsVisible = false;
        //            //StackOrderCanceled.IsVisible = true;
        //            //SystemSound.Vibrate.PlayAlertSound();
        //            //OrderCanceled.PlayAnimation();
        //            //OrderCanceled.RepeatCount = 3;
        //            //StackOrderState.IsVisible = false;
        //            Application.Current.MainPage.DisplayAlert("","canceled","Ok");
        //        });
        //    }
        //}



        private async  void GetbestBrand1()
        {
           await _navigation.PushAsync(new ProductByCategories(LblBrand1));
            
        }
        private async void GetbestBrand2()
        {
            await _navigation.PushAsync(new ProductByCategories(LblBrand2));

        }
        private async void GetbestBrand3()
        {
            await _navigation.PushAsync(new ProductByCategories(LblBrand3));

        }
        private async void GetbestBrand4()
        {
            await _navigation.PushAsync(new ProductByCategories(LblBrand4));

        }
        private async void GetbestBrand5()
        {
            await _navigation.PushAsync(new ProductByCategories(LblBrand5));

        }
        private async void GetbestBrand6()
        {
            await _navigation.PushAsync(new ProductByCategories(LblBrand6));

        }

        private async void OnBoxTap()
        {
            await _navigation.PushAsync(new NewsProductPage());
            
        }

        private void GetWelcomeName()
        {
            DateTime currentTime = DateTime.Now;
            DateTime morningStartTime = DateTime.Today.AddHours(5);
            DateTime morningEndTime = DateTime.Today.AddHours(11).AddMinutes(59);
            string username = Preferences.Get("user_name", string.Empty);

            if (currentTime >= morningStartTime && currentTime <= morningEndTime)
            {
                LblWelcomeNameHomePage = string.Format(AppResources.GoodMorning +" "+ username+" !");
            }
            else
            {
                LblWelcomeNameHomePage = string.Format(AppResources.HelloWorld +" "+ username+" !");
            }
        }
        public LayoutOptions BoxViewNewIn
        {
            get => _BoxViewNewIn;
            set
            {
                _BoxViewNewIn = value;
                OnPropertyChanged(nameof(BoxViewNewIn));
            }
        }
        private void flowdirections()
        {
            string currentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            flowdirecation = currentLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            ScalexDireaction = currentLanguage == "ar" ? 1 :-1;
            BoxViewNewIn = currentLanguage == "ar" ? LayoutOptions.Start : LayoutOptions.End;
        }

        private async void ShowDiscountProduct()
        {
            
            await _navigation.PushAsync(new DiscountPage());
        }

        private async void CarouselViewImagetap(Product product)
        {
           await _navigation.PushAsync(new DetailViewProductHomePage(product));

        }

        private async void ShowAllNewIn(object obj)
        {
           await _navigation.PushAsync(new AllNewInProductPage());

        }

        private async void DisplayGetBestCategories()
        {
          var response = await ApiSerives.GetBestCategories();
            foreach (var item in response)
            {
                CollectionBestCategorySource.Add(item);
            }
        }
        private async void DisplayGiftBox()
        {
           var response = await ApiSerives.GetGiftBox();
            foreach (var item in response)
            {
                CollectionGiftBox.Add(item);
            }

        }
        private async void GetBestSearchHome()
        {
            var response = await ApiSerives.GetSearchProduct();
            foreach (var item in response)
            {
                CollectionSearchProduct.Add(item);
            }
        }
        private async void GetRecentlyViewd()
        {
            var response = await ApiSerives.GetRecentlyViewd(Preferences.Get("user_Id", 0));
            foreach (var item in response)
            {
               RecentlyViewd.Add(item);
            }
            StackRecentltyView = response.Count != 0 ? true : false;

        }
        private async void GetExclosveDesgin()
        {
          var response = await ApiSerives.GetExclosveDesgin();
            ImageMainPage = response.ImageUrl;
            ButtonNameExcVisible = response.IsAvalible;
            ButtonNameExcText = response.Title;
        }

        private async void GetBrandsName()
        {
            // Array to hold the properties that will be updated (mapping each label)
            string[] brandProperties = { "LblBrand1", "LblBrand2", "LblBrand3", "LblBrand4", "LblBrand5", "LblBrand6" };

            // Track the overall brand index
            int currentBrandIndex = 0;

            for (int i = 0; i < brandProperties.Length; i++)
            {
                // Call API to get the list of brands for the current index (assuming API returns multiple brands at once)
                var brandList = await ApiSerives.GetBrandsName(i + 1); // Assuming API takes a 1-based index

                // Check if the API returned any brands
                if (brandList != null && brandList.Count > 0)
                {
                    // Loop through the brands in the result and assign them to the corresponding label
                    for (int s = 0; s < brandList.Count && currentBrandIndex < brandProperties.Length; s++, currentBrandIndex++)
                    {
                        switch (currentBrandIndex)
                        {
                            case 0:
                                LblBrand1 = brandList[s].Name;
                                break;
                            case 1:
                                LblBrand2 = brandList[s].Name;
                                break;
                            case 2:
                                LblBrand3 = brandList[s].Name;
                                break;
                            case 3:
                                LblBrand4 = brandList[s].Name;
                                break;
                            case 4:
                                LblBrand5 = brandList[s].Name;
                                break;
                            case 5:
                                LblBrand6 = brandList[s].Name;
                                break;
                        }
                    }
                }
            }
        }
        private async void GetNewIn()
        {
            var response = await ApiSerives.GetNewIn();
            foreach (var item in response)
            {
              CarouselNewIn.Add(item);

            }
           await Task.Delay(150);
            GridLoaderHomePage = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

