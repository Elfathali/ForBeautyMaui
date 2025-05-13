using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Maui.Graphics;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.ShoppingCardPages;

namespace ForBeautyMaui.ViewModels.ShoppingCartPageTappedViewModel
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private SharedServicesViewModel _SharedServices;
        private double _LblTotalPrice;
        private Color _lblcouponmessageColor;
        private bool _ActivityIndicatorCoupon;
        private string _Name;
        private bool _lblDeliveryOptionErrorVisiblety;
        private bool _lblMessageErrorPickerVisiblety;
        private Cities _CitySeletedItem;
        private FlowDirection _FlowDirection;
        private bool _EntyCodeDiscountIsEnbaled = true;
        private bool _GridLoadingCheckOut;
        private Color _boxLinecoupon = Color.FromHex("#D4D4D4");
        private bool _LblPriceDiffrentVisiblity;
        private bool _StackShoppingVisiblety;
        private string _LblPriceDiffrent;
        private bool _ImagehandmoneyVisiblty;
        private bool _stackForPersonalRecive = false;
        private bool _StackForDeliveryDeatils = false;
        private bool IsCouponeEntred = false;
        private bool _lblcouponmessageVisiblty;
        private double DiscountForAddShopping;
        private bool _ImageCheckCouponVisiblty;
        private string _ImageCheckCoupon;
        private string _lblDiscountCoupon = 0.ToString();
        private string _EntyCodeDiscount;
        private string _lblcouponmessage;
        private bool _FramePickUpShadow = false;
        private Color _FramePickUpBorderColor = Colors.Gray;
        private Color _FrameDeliveryBorderColor = Colors.Gray;
        private bool _FrameDeliveryShadow;
        public Command CommandPickUpOrder { get; set; }
        public Command CommandDeliveryOrder { get; set; }
        public Command CommandApplyCoupone { get; set; }
        public Command CheckOut { get; set; }
        public ObservableCollection<ShoppingCart> ObsShoppings => _SharedServices.ObsShopping;

        public Command<ShoppingCart> CommandDeleteForShoppingCart { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Cities> _firstPickerSource;
        private List<Area> _secondPickerSource;
        public List<Cities> FirstPickerSource
        {
            get => _firstPickerSource;
            set
            {
                _firstPickerSource = value;
                OnPropertyChanged(nameof(FirstPickerSource));
            }
        }
        public List<Area> secondPickerSource
        {
            get => _secondPickerSource;
            set
            {
                _secondPickerSource = value;
                OnPropertyChanged(nameof(secondPickerSource));
            }
        }
        public Cities CitySeletedItem
        {
            get => _CitySeletedItem;
            set
            {
                _CitySeletedItem = value;
                OnPropertyChanged(nameof(CitySeletedItem));
                LoadArea();
            }
        }

        public bool ImageCheckCouponVisiblty
        {
            get => _ImageCheckCouponVisiblty;
            set
            {
                if (_ImageCheckCouponVisiblty != value)
                {
                    _ImageCheckCouponVisiblty = value;
                    OnPropertyChanged(nameof(ImageCheckCouponVisiblty));
                }
            }
        }
        public string LblPriceDiffrent
        {
            get => _LblPriceDiffrent;
            set
            {
                _LblPriceDiffrent = value;
                OnPropertyChanged(nameof(LblPriceDiffrent));
            }
        }
        public bool GridLoadingCheckOut
        {
            get => _GridLoadingCheckOut;
            set
            {
                _GridLoadingCheckOut = value;
                OnPropertyChanged(nameof(GridLoadingCheckOut));
            }
        }
        public bool lblDeliveryOptionErrorVisiblety
        {
            get => _lblDeliveryOptionErrorVisiblety;
            set
            {
                _lblDeliveryOptionErrorVisiblety = value;
                OnPropertyChanged(nameof(lblDeliveryOptionErrorVisiblety));
            }
        }
        public bool lblMessageErrorPickerVisiblety
        {
            get => _lblMessageErrorPickerVisiblety;
            set
            {
                _lblMessageErrorPickerVisiblety = value;
                OnPropertyChanged(nameof(lblMessageErrorPickerVisiblety));
            }
        }



        public double LblTotalPrice
        {
            get => _LblTotalPrice;
            set
            {
                _LblTotalPrice = value;
                OnPropertyChanged(nameof(LblTotalPrice));
            }

        }
        public bool StackShoppingVisiblety
        {
            get => _StackShoppingVisiblety;
            set
            {
                _StackShoppingVisiblety = value;
                OnPropertyChanged(nameof(StackShoppingVisiblety));
            }
        }
        public bool ImagehandmoneyVisiblty
        {
            get => _ImagehandmoneyVisiblty;
            set
            {
                if (_ImagehandmoneyVisiblty != value)
                {
                    _ImagehandmoneyVisiblty = value;
                    OnPropertyChanged(nameof(ImagehandmoneyVisiblty));
                }
            }
        }
        public ObservableCollection<ShoppingCart> ObsShopping
        {
            get => _SharedServices.ObsShopping;

        }
        public string ImageCheckCoupon
        {
            get => _ImageCheckCoupon;
            set
            {
                if (_ImageCheckCoupon != value)
                {
                    _ImageCheckCoupon = value;
                    OnPropertyChanged(nameof(ImageCheckCoupon));
                }
            }
        }
        public bool stackForPersonalRecive
        {
            get => _stackForPersonalRecive;
            set
            {
                _stackForPersonalRecive = value;
                OnPropertyChanged(nameof(stackForPersonalRecive));
            }
        }
        public bool StackForDeliveryDeatils
        {
            get => _StackForDeliveryDeatils;
            set
            {
                _StackForDeliveryDeatils = value;
                OnPropertyChanged(nameof(StackForDeliveryDeatils));
            }
        }
        public bool LblPriceDiffrentVisiblity
        {
            get => _LblPriceDiffrentVisiblity;
            set
            {
                if (_LblPriceDiffrentVisiblity != value)
                {
                    _LblPriceDiffrentVisiblity = value;
                    OnPropertyChanged(nameof(LblPriceDiffrentVisiblity));
                }
            }
        }
        public async Task<double> GetShoppingCart()
        {
            double TotalPrice = 0;

            double p = 0;
            double PriceDiffrent = 0;
            var response = await ApiSerives.GetShoppingCartItem(Preferences.Get("user_Id", 0));
            foreach (var item in response)
            {
                var existingItem = ObsShoppings.FirstOrDefault(A => A.ProductId == item.ProductId && A.Size == item.Size);
                if (existingItem == null)
                {
                    ObsShoppings.Add(item);
                }
            }
            foreach (var item1 in ObsShoppings)
            {
                if (item1.discount > 0)
                {
                    TotalPrice += item1.discount * item1.Qyt;
                    PriceDiffrent += item1.discount * item1.Qyt;
                    p += item1.price * item1.Qyt;
                }
                else
                {
                    TotalPrice += item1.price * item1.Qyt;
                }
            }
            LblTotalPrice = TotalPrice;
            CheckVilibletyShoppingCart();
            if (p - PriceDiffrent == 0)
            {
                LblPriceDiffrentVisiblity = false;
                ImagehandmoneyVisiblty = false;
            }
            else
            {
                LblPriceDiffrentVisiblity = true;
                string currentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

                LblPriceDiffrent = currentLanguage == "ar" ? ("ستـقـوم بتـوفـير  " + (p - PriceDiffrent).ToString() + " د.ل") : ("You will save " + (p - PriceDiffrent).ToString() + " dl");
                ImagehandmoneyVisiblty = true;

            }
            BadgeCounterService.SetCount(ObsShoppings.Count);

            return TotalPrice;
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
        public double UpdatePrice()
        {
            LblTotalPrice = 0;
            double p = 0;
            double PriceDiffrent = 0;
            foreach (var item1 in ObsShoppings)
            {
                if (item1.discount > 0)
                {
                    LblTotalPrice += item1.discount * item1.Qyt;
                    PriceDiffrent += item1.discount * item1.Qyt;
                    p += item1.price * item1.Qyt;
                }
                else
                {
                    LblTotalPrice += item1.price * item1.Qyt;
                }
            }
            if (p - PriceDiffrent == 0)
            {
                LblPriceDiffrentVisiblity = false;
                ImagehandmoneyVisiblty = false;
            }
            else
            {
                LblPriceDiffrentVisiblity = true;

                LblPriceDiffrent = ("ستـقـوم بتـوفـير  " + (p - PriceDiffrent).ToString() + " د.ل");
                ImagehandmoneyVisiblty = true;

            }
            return LblTotalPrice;
        }
        public string EntyCodeDiscount
        {
            get => _EntyCodeDiscount;
            set
            {
                if (_EntyCodeDiscount != value)
                {
                    _EntyCodeDiscount = value;
                    OnPropertyChanged(nameof(EntyCodeDiscount));
                }
            }

        }
        public string lblDiscountCoupon
        {
            get => _lblDiscountCoupon;
            set
            {
                if (_lblDiscountCoupon != value)
                {
                    _lblDiscountCoupon = value;
                    OnPropertyChanged(nameof(lblDiscountCoupon));
                }
            }
        }

        public bool lblcouponmessageVisiblty
        {
            get => _lblcouponmessageVisiblty;
            set
            {
                if (_lblcouponmessageVisiblty != value)
                {
                    _lblcouponmessageVisiblty = value;
                    OnPropertyChanged(nameof(lblcouponmessageVisiblty));
                }
            }
        }
        public string lblcouponmessage
        {
            get => _lblcouponmessage;
            set
            {
                if (_lblcouponmessage != value)
                {
                    _lblcouponmessage = value;
                    OnPropertyChanged(nameof(lblcouponmessage));
                }
            }
        }

        public bool EntyCodeDiscountIsEnbaled
        {
            get => _EntyCodeDiscountIsEnbaled;
            set
            {
                if (_EntyCodeDiscountIsEnbaled != value)
                {
                    _EntyCodeDiscountIsEnbaled = value;
                    OnPropertyChanged(nameof(EntyCodeDiscountIsEnbaled));
                }
            }
        }
        public bool ActivityIndicatorCoupon
        {
            get => _ActivityIndicatorCoupon;
            set
            {
                if (_ActivityIndicatorCoupon != value)
                {
                    _ActivityIndicatorCoupon = value;
                    OnPropertyChanged(nameof(ActivityIndicatorCoupon));
                }
            }
        }
        public Color lblcouponmessageColor
        {
            get => _lblcouponmessageColor;
            set
            {
                if (_lblcouponmessageColor != value)
                {
                    _lblcouponmessageColor = value;
                    OnPropertyChanged(nameof(lblcouponmessageColor));
                }

            }

        }
        public bool FramePickUpShadow
        {
            get => _FramePickUpShadow;
            set
            {
                _FramePickUpShadow = value;
                OnPropertyChanged(nameof(FramePickUpShadow));
            }
        }
        public Color FramePickUpBorderColor
        {
            get => _FramePickUpBorderColor;
            set
            {
                _FramePickUpBorderColor = value;
                OnPropertyChanged(nameof(FramePickUpBorderColor));
            }
        }
        public bool FrameDeliveryShadow
        {
            get => _FrameDeliveryShadow;
            set
            {
                _FrameDeliveryShadow = value;
                OnPropertyChanged(nameof(FrameDeliveryShadow));
            }
        }
        public Color FrameDeliveryBorderColor
        {
            get => _FrameDeliveryBorderColor;
            set
            {
                _FrameDeliveryBorderColor = value;
                OnPropertyChanged(nameof(FrameDeliveryBorderColor));
            }
        }
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ShoppingCartViewModel(SharedServicesViewModel sharedServices)
        {
            _SharedServices = sharedServices;
            CommandApplyCoupone = new Command(ApplyCoupone);
            GetShoppingCart().ConfigureAwait(false);
            CommandDeleteForShoppingCart = new Command<ShoppingCart>(item => DeleteFromShppingCart(item));
            CheckVilibletyShoppingCart();
            _SharedServices.ShoppingCartUpdated += OnShoppingCartUpdated;
            CommandPickUpOrder = new Command(OnPickUpSelected);
            CommandDeliveryOrder = new Command(OnDeliverySelected);
            CheckOut = new Command(CheckOutBtn);
            LoadCites();
            flowdirections();
        }

        private async void CheckOutBtn()
        {
            try
            {
                GridLoadingCheckOut = true;
                var Cresponse = await ApiSerives.CheckAllawAndLimt();
                if (Cresponse.IsAllaw == false)
                {
                    await App.Current.MainPage.DisplayAlert("", "تم تعليق الطلبات مؤقتا , يرجي المحاولة لاحقا", "تمام");
                    GridLoadingCheckOut = false;
                    return;

                }
                else if (Convert.ToDouble(LblTotalPrice) < Cresponse.OrderLimt)
                {
                    await App.Current.MainPage.DisplayAlert("", "الحد الادني للشراء هوا " + Cresponse.OrderLimt + " د.ل", "تمام");
                    GridLoadingCheckOut = false;
                    return;
                }

                string s;
                if (FramePickUpShadow == false && FrameDeliveryShadow == false)
                {
                    lblDeliveryOptionErrorVisiblety = true;
                    GridLoadingCheckOut = false;
                    return;
                }
                else
                {
                    lblDeliveryOptionErrorVisiblety = false;
                }
                if (FramePickUpShadow)
                {
                    s = "إستلام شخصي";

                }
                else
                {
                    s = "توصيل";
                    //if (firstPicker.SelectedIndex == -1 || secondPicker.SelectedIndex == -1)
                    //{
                    //    lblMessageErrorPickerVisiblety = true;
                    //    GridLoadingCheckOut = false;
                    //    return;
                    //}
                    //else
                    //{
                    //    lblMessageErrorPickerVisiblety = false;
                    //}
                }


                var item = new Order()
                {
                    UserId = Preferences.Get("user_Id", 0),
                    //City = firstPicker.SelectedIndex == -1 ? string.Empty : firstPicker.Items[firstPicker.SelectedIndex],
                    //Area = secondPicker.SelectedIndex == -1 ? string.Empty : secondPicker.Items[secondPicker.SelectedIndex],
                    Coupon = IsCouponeEntred == true ? EntyCodeDiscount : " ",
                    TypeOfOrder = s,
                    TotalAmount = Convert.ToDouble(LblTotalPrice)
                };
                var response = await ApiSerives.PlaceOrder(item);

                if (response.check)
                {
                    // await hubConnection.InvokeAsync("SubmitOrder", Preferences.Get("user_Id", string.Empty));
                    ObsShopping.Clear();
                    //updateBadgeAndShoppingCartPage();
                    if (FrameDeliveryShadow)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new DeliveryPage(response.order));

                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new PickUpPage());
                        //    OrderIdFinsh.Text = response.order.id.ToString();
                        //    FinshOrderPlace.Text = response.order.orderPlaced.ToString();
                        //    ZxingBaraCode.BarcodeValue = OrderIdFinsh.Text;
                        //    lottieFireWorks.IsVisible = true;
                        //    stackdistance.IsVisible = true;
                        //    framebaracode.IsVisible = true;
                        //    GridOrderFinish.IsVisible = true;
                        //    stackforbeautyAddress.IsVisible = true;
                        //    GetOrderStateUser();
                        //    lottieFireWorks.PlayAnimation();
                        //    await Task.Delay(7000);
                        //    lottieFireWorks.IsVisible = false;
                        //    lottieFireWorks.StopAnimation();


                    }
                    //couponDelete();
                    CheckVilibletyShoppingCart();
                    GridLoadingCheckOut = false;



                }
                else
                {
                    GridLoadingCheckOut = false;
                    await App.Current.MainPage.DisplayAlert("", response.Error, "تمام");
                }

            }
            catch (Exception ex)
            {
                GridLoadingCheckOut = false;
                await App.Current.MainPage.DisplayAlert("ErrorMessage", $"{ex.Message}", "Ok");
            }
        }

        private async Task PopulateAreaPickerAsync()
        {
            if (CitySeletedItem.Id >= 0)
            {
                List<Area> areas = await ApiSerives.GetAreaByCity(CitySeletedItem.Id);
                secondPickerSource = areas;
            }
        }
        private async void LoadArea()
        {
            await PopulateAreaPickerAsync();
        }
        private async Task PopulateCityPickerAsync()
        {

            List<Cities> cities = await ApiSerives.GetCites();
            FirstPickerSource = cities;

        }
        private async void LoadCites()
        {
            await PopulateCityPickerAsync();
        }

        private void OnPickUpSelected()
        {
            FramePickUpShadow = true;
            FramePickUpBorderColor = Colors.Blue;
            FrameDeliveryShadow = false;
            FrameDeliveryBorderColor = Color.FromArgb("#D4D4D4");
            stackForPersonalRecive = true;
            StackForDeliveryDeatils = false;


        }
        private void OnDeliverySelected()
        {
            FramePickUpShadow = false;
            FramePickUpBorderColor = Color.FromArgb("#D4D4D4");
            FrameDeliveryShadow = true;
            FrameDeliveryBorderColor = Colors.Blue;
            stackForPersonalRecive = false;
            StackForDeliveryDeatils = true;
        }

        public Color boxLinecoupon
        {
            get => _boxLinecoupon;
            set
            {
                _boxLinecoupon = value;
                OnPropertyChanged(nameof(boxLinecoupon));
            }
        }

        private void ActivityIndicatorCouponOn()
        {
            ActivityIndicatorCoupon = true;
            ActivityIndicatorCoupon = true;

        }
        private void ActivityIndicatorCouponOff()
        {
            ActivityIndicatorCoupon = false;
            ActivityIndicatorCoupon = false;
        }

        private async void ApplyCoupone()
        {

            ImageCheckCouponVisiblty = false;
            ActivityIndicatorCouponOn();

            if (IsCouponeEntred == true)
            {
                var response = await App.Current.MainPage.DisplayAlert("", "هدا الكوبون مفعل هل تريد الغاء التفعيل", "نعم", "لا");
                if (response)
                {
                    lblDiscountCoupon = "0";
                    EntyCodeDiscountIsEnbaled = true;
                    boxLinecoupon = Color.FromHex("#D4D4D4");
                    ImageCheckCouponVisiblty = false;
                    lblcouponmessageVisiblty = false;
                    IsCouponeEntred = false;
                    await GetShoppingCart();
                    ActivityIndicatorCouponOff();

                    return;
                }
                else
                {
                    ImageCheckCoupon = "correctGreen";
                    ImageCheckCouponVisiblty = true;
                    ActivityIndicatorCouponOff();
                    return;
                }
            }

            var reponse1 = await ApiSerives.CheckCouponLimt(EntyCodeDiscount);
            if (reponse1.check)
            {

                if (reponse1.checklimt.TotalLimt > Convert.ToDouble(LblTotalPrice))
                {
                    await App.Current.MainPage.DisplayAlert("", "الحد الادني لشراء بواسطة هدا الكبون  " + reponse1.checklimt.TotalLimt + " د.ل", "تمام");
                    ActivityIndicatorCouponOff();
                }
                else
                {
                    var check = new CheckCoupon()
                    {
                        code = EntyCodeDiscount,
                        UserId = Preferences.Get("user_Id", 0),
                    };
                    var reponse = await ApiSerives.CheckCoupon(check);
                    if (reponse.check)
                    {
                        DiscountForAddShopping = reponse1.checklimt.Discount;
                        ImageCheckCoupon = "correctGreen";
                        ImageCheckCouponVisiblty = true;
                        boxLinecoupon = Color.FromArgb("#D4D4D4");
                        double lblbeforeDiscount = Convert.ToInt32(LblTotalPrice);
                        double clulateP = Convert.ToInt32(LblTotalPrice) * reponse.message.discount;
                        double newprice = Convert.ToInt32(LblTotalPrice) - clulateP;
                        lblDiscountCoupon = (lblbeforeDiscount - newprice).ToString();
                        LblTotalPrice = newprice;
                        lblcouponmessage = reponse.message.couponeMessage;
                        lblcouponmessageColor = Colors.Green;
                        lblcouponmessageVisiblty = true;
                        EntyCodeDiscountIsEnbaled = false;
                        IsCouponeEntred = true;
                        ActivityIndicatorCouponOff();
                    }
                    else
                    {
                        lblcouponmessage = reponse.checkcoupon.ToString();
                        lblcouponmessageColor = Colors.Red;
                        ImageCheckCoupon = "WrongCoupon.png";
                        ImageCheckCouponVisiblty = true;
                        lblcouponmessageVisiblty = true;
                        boxLinecoupon = Colors.Red;
                        ActivityIndicatorCouponOff();
                    }
                }
            }
            else
            {
                lblcouponmessageVisiblty = true;
                lblcouponmessageColor = Colors.Red;
                lblcouponmessage = "خطأ في ادخال الكوبون";
                ImageCheckCouponVisiblty = true;
                ImageCheckCoupon = "WrongCoupon.png";
                ActivityIndicatorCouponOff();
            }
        }


        public async void DeleteFromShppingCart(ShoppingCart shoppingCart)
        {
            if (IsCouponeEntred)
            {
                var response = await ApiSerives.CheckCouponLimt(EntyCodeDiscount);
                double s = 0;
                if (shoppingCart.discount <= 0)
                {
                    s = (shoppingCart.price * shoppingCart.Qyt);
                }
                else
                {
                    s = (shoppingCart.discount * shoppingCart.Qyt);
                }
                var TotailPrice = Convert.ToDouble(LblTotalPrice) + Convert.ToDouble(lblDiscountCoupon) - s;

                if (response.checklimt.TotalLimt > TotailPrice)
                {
                    var response1 = await Application.Current.MainPage.DisplayAlert("", "الحد الادني لشراء بواسطة هدا الكوبون  " + response.checklimt.TotalLimt + "د.ل هل تريد إلغاء الكوبون وحدف العنصر ؟", "نعم", "لا");
                    if (response1)
                    {

                        var responseCheck = await ApiSerives.RemoveFromShoppingCart(Preferences.Get("user_Id", 0), shoppingCart.ProductId, shoppingCart.Size);

                        if (responseCheck)
                        {
                            ObsShopping.Remove(shoppingCart);
                            ImageCheckCouponVisiblty = false;
                            boxLinecoupon = Color.FromArgb("#D4D4D4");
                            lblcouponmessageVisiblty = false;
                            EntyCodeDiscountIsEnbaled = true;
                            EntyCodeDiscount = string.Empty;
                            lblDiscountCoupon = "0";
                            IsCouponeEntred = false;
                            await GetShoppingCart();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    var response1 = await ApiSerives.RemoveFromShoppingCart(Preferences.Get("user_Id", 0), shoppingCart.ProductId, shoppingCart.Size);

                    if (response1)
                    {
                        ObsShopping.Remove(shoppingCart);
                        await GetShoppingCart();
                    }
                    double lblbeforeDiscount = Convert.ToDouble(LblTotalPrice);
                    double clulateP = Convert.ToDouble(LblTotalPrice) * response.checklimt.Discount;
                    double newprice = Convert.ToDouble(LblTotalPrice) - clulateP;
                    lblDiscountCoupon = (lblbeforeDiscount - newprice).ToString();
                    LblTotalPrice = newprice;
                    return;
                }
            }
            var UserId = Preferences.Get("user_Id", 0);


            _SharedServices.ObsShopping.Remove(shoppingCart);
            UpdatePrice();
            CheckVilibletyShoppingCart();
            BadgeCounterService.SetCount(ObsShoppings.Count);
            await ApiSerives.RemoveFromShoppingCart(UserId, shoppingCart.ProductId, shoppingCart.Size);

        }
        private void CheckVilibletyShoppingCart()
        {
            if (_SharedServices.ObsShopping.Count > 0)
            {
                StackShoppingVisiblety = true;
            }
            else
            {
                StackShoppingVisiblety = false;
            }
        }

        private void OnShoppingCartUpdated()
        {
            LblTotalPrice = UpdatePrice();
            CheckVilibletyShoppingCart();


        }
        private void flowdirections()
        {
            string currentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            flowdirecation = currentLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }






        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}