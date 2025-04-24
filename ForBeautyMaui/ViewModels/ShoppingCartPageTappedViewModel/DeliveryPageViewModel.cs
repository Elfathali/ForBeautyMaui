using System;
using System.ComponentModel;

using ForBeauty.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ForBeautyMaui.ViewModels.ShoppingCartPageTappedViewModel
{
	public class DeliveryPageViewModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private int _OrderIdFinshDelivery;
        private bool _StackPrepareOrder;
        private string _lblDescriptionOrderDelivery ="جاري مراجعة طلبك";
        private bool _StackOrderCanceled;
        private bool _StackOrderCompleted;
        public bool StackOrderCompleted
        {
            get => _StackOrderCompleted;
            set
            {
                _StackOrderCompleted = value;
                OnPropertyChanged(nameof(StackOrderCompleted));
            }
        }
        public bool StackOrderCanceled
        {
            get => _StackOrderCanceled;
            set
            {
                _StackOrderCanceled = value;
                OnPropertyChanged(nameof(StackOrderCanceled));
            }
        }
        public string lblDescriptionOrderDelivery
        {
            get => _lblDescriptionOrderDelivery;
            set
            {
                _lblDescriptionOrderDelivery = value;
                OnPropertyChanged(nameof(lblDescriptionOrderDelivery));
            }
        }
        public bool StackPrepareOrder
        {
            get => _StackPrepareOrder;
            set
            {
                _StackPrepareOrder = value;
                OnPropertyChanged(nameof(StackPrepareOrder));


            }
        }
        private bool _stackCarBeauty;

        private string _FinshOrderPlaceDelivery;

        public bool stackCarBeauty
		{
			get => _stackCarBeauty;
			set
			{
				_stackCarBeauty = value;
				OnPropertyChanged(nameof(stackCarBeauty));
			}
        }
        public string FinshOrderPlaceDelivery
        {
            get => _FinshOrderPlaceDelivery;
            set
            {
                _FinshOrderPlaceDelivery = value;
                OnPropertyChanged(nameof(FinshOrderPlaceDelivery));
            }
        }
        private bool _stackReviewOrder;
        public bool stackReviewOrder
        {
            get => _stackReviewOrder;
            set
            {
                _stackReviewOrder = value;
                OnPropertyChanged(nameof(stackReviewOrder));
            }
        }

        public int OrderIdFinshDelivery
		{
			get => _OrderIdFinshDelivery;
			set
			{
				_OrderIdFinshDelivery = value;
				OnPropertyChanged(nameof(OrderIdFinshDelivery));
            }
        }
		private void GetOrderState(Order order)
		{
            OrderIdFinshDelivery = order.id;
            FinshOrderPlaceDelivery = order.orderPlaced.ToString();


            //    //StackOrderCompleted.IsVisible = false;
                  stackReviewOrder = true;
                  stackCarBeauty = false;
                  StackPrepareOrder = false;
            //    StackOrderCanceled.IsVisible = false;
            //    OrderScan.PlayAnimation();
            //    OrderIdFinshDelivery.Text = response.order.id.ToString();
            //    FinshOrderPlaceDelivery.Text = response.order.orderPlaced.ToString();
            //    lottieFireWorks.IsVisible = true;
            //    GridOrderFinishDelivery.IsVisible = true;
            //    GetOrderStateUser();
            //    lottieFireWorks.PlayAnimation();
            //    await Task.Delay(7000);
            //    lottieFireWorks.IsVisible = false;
            //    lottieFireWorks.StopAnimation();
        }


        public DeliveryPageViewModel(Order OrderDetail)
		{
           // CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            GetOrderState(OrderDetail);

        }

        //private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        //{
        //    if (e.Data.ContainsKey("PrepareOrder"))
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            App.Current.MainPage.DisplayAlert("", "preparorder", "Ok");
        //            //SystemSound.Vibrate.PlayAlertSound();
                   
        //            stackReviewOrder = false;
        //            StackOrderCanceled = false;
        //            StackPrepareOrder = true;
        //            lblDescriptionOrderDelivery = "تمت الموافقة علي طلبك, يتم الان تجهيز طلبك";
        //        });
        //    }
        //    else if (e.Data.ContainsKey("StartDeleviry"))
        //    {

        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            StackPrepareOrder = false;
        //            stackReviewOrder = false;
        //            StackOrderCanceled = false;
        //            stackCarBeauty = true;
        //            lblDescriptionOrderDelivery = "جاري توصيل طلبك";
        //            //SystemSound.Vibrate.PlayAlertSound();
                    
        //        });
        //    }
        //    else if (e.Data.ContainsKey("OrderComplete"))
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            lblDescriptionOrderDelivery = "تم تسليم الطلب";
        //            stackReviewOrder = false;
        //            StackPrepareOrder = false;
        //            stackCarBeauty = false;
        //            StackOrderCanceled = false;
        //            StackOrderCompleted = true;
        //            //OrderCompletedAnimation.PlayAnimation();
        //            //OrderCompletedAnimation.RepeatCount = 3;
        //            //SystemSound.Vibrate.PlayAlertSound();
        //            //OrderCompletedAnimation.StopAnimation();
        //            //var animation = new Animation(v => StackOrderState.Opacity = v, 1, 0);
        //            //animation.Commit(StackOrderState, "FadeOutAnimation", length: 5000, easing: Easing.Linear, finished: (v, c) => StackOrderState.IsVisible = false);
        //            //var animation1 = new Animation(v => StackOrderState1.Opacity = v, 1, 0);
        //            //animation.Commit(StackOrderState, "FadeOutAnimation", length: 5000, easing: Easing.Linear, finished: (v, c) => StackOrderState.IsVisible = false);
        //        });
        //    }
        //    else if (e.Data.ContainsKey("OrderCanceled"))
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
                    
        //            lblDescriptionOrderDelivery = "عذراً، تم إلغاء طلبك ";
        //            stackReviewOrder = false;
        //            StackPrepareOrder = false;
        //            stackCarBeauty = false;
        //            StackOrderCompleted = false;
        //            StackOrderCanceled = true;
        //            //SystemSound.Vibrate.PlayAlertSound();
        //            //OrderCanceled.RepeatCount = 3;
        //            //StackOrderState.IsVisible = false;
        //        });
        //    }
        //}



        private void OnPropertyChanged(string propertyname)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
		}
	}
}

