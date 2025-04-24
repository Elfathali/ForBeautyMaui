using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
	public class ProductByCategoriesViewModel : INotifyPropertyChanged
		
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Product> ObsBrand { get; set; }
		private string _BrandName;

        public string BrandName
		{
			get => _BrandName;
			set
			{
				_BrandName = value;
				OnpropertyChanged(nameof(BrandName));
            }

        }

        public ProductByCategoriesViewModel(string BrandName)
		{
			ObsBrand = new ObservableCollection<Product>();
			this.BrandName = BrandName;
            Getbrand1(BrandName);


        }
		private async void Getbrand1(string BrandName)
		{
          var response =  await ApiSerives.GetProductByBrand(BrandName);
			foreach (var item in response)
			{
				ObsBrand.Add(item);
			}
        }
		private void OnpropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

