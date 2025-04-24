using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages;


namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class PreveuseOrderPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        private FlowDirection _FlowDirection;
        private PrefeuseOrderUserById _PrevouseOrderSelectedItem;
        public ObservableCollection<PrefeuseOrderUserById> ListPrevouseOrder { get; set; } = new ObservableCollection<PrefeuseOrderUserById>();



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

        public PrefeuseOrderUserById PrevouseOrderSelectedItem

        {
            get => _PrevouseOrderSelectedItem;
            set
            {
                _PrevouseOrderSelectedItem = value;
                OnPropertyChanged(nameof(PrevouseOrderSelectedItem));
                OnItemSelected(_PrevouseOrderSelectedItem);
            }
        }

        private void OnItemSelected(PrefeuseOrderUserById OrderUser)
        {
            if (OrderUser != null)
            {
                _navigation.PushAsync(new OrderDetailPage(OrderUser));
                PrevouseOrderSelectedItem = null;
            }
        }

        public PreveuseOrderPageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            ListPrevouseOrder = new ObservableCollection<PrefeuseOrderUserById>();
            GetPervouseOrder();
            flowdirections();

        }

        private async void GetPervouseOrder()
        {
          var reponse = await ApiServices.ApiSerives.PrevouseOrderByUser(Preferences.Get("user_Id", 0));
            foreach (var item in reponse)
            {
                ListPrevouseOrder.Add(item);
            }
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

