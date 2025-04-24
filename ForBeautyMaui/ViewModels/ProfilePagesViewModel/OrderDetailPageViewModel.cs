using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class OrderDetailPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private string _lbltotalinDetial;

        public ObservableCollection<OrderDetail> ListOrderDeatil { get; set; } = new ObservableCollection<OrderDetail>();

		public string lbltotalinDetial
		{
			get => _lbltotalinDetial;
			set
			{
				if (_lbltotalinDetial != value)
				{
					_lbltotalinDetial = value;
					OnPropertyChanged(nameof(lbltotalinDetial));

                }
			}
		}


        public OrderDetailPageViewModel(PrefeuseOrderUserById OrderUser)
		{
            ListOrderDeatil = new ObservableCollection<OrderDetail>();
			GetOrderDetail(OrderUser.id);
			lbltotalinDetial = OrderUser.totalAmount.ToString();

        }

		private async void GetOrderDetail(int OrderId)
		{

			var response = await ApiServices.ApiSerives.GetOrderDetails(OrderId);
			foreach (var item in response)
			{
                ListOrderDeatil.Add(item);
			}

            



        }

		private void OnPropertyChanged(string PropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}

