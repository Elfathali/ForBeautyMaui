using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels.HomePageTappedViewModel
{
    public class MoreOptionsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DetailViewProductHomePageViewModel _DetailViewProductHomePageViewModel;
        private ObservableCollection<OtherDesginProduct> _CollectionMoreOptionsSource { get; set; }
        private OtherDesginProduct _CollectionMoreOptionsSelectedItem;
        private string _LblMoreOptionCount;
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
        public OtherDesginProduct CollectionMoreOptionsSelectedItem
        {
            get => _CollectionMoreOptionsSelectedItem;
            set
            {
                if (_CollectionMoreOptionsSelectedItem != value)
                {
                    _CollectionMoreOptionsSelectedItem = value;
                    OnPropertyChanged(nameof(CollectionMoreOptionsSelectedItem));
                    OnItemSelected(_CollectionMoreOptionsSelectedItem);
                }
            }
        }
        public ObservableCollection<OtherDesginProduct> CollectionMoreOptionsSource
        {
            get => _CollectionMoreOptionsSource;
            set
            {
                _CollectionMoreOptionsSource = value;
                OnPropertyChanged(nameof(CollectionMoreOptionsSource));
            }
        }





        private async void OnItemSelected(OtherDesginProduct selectedOption)
        {
             ObservableCollection<OtherDesginProduct > UpdateList = new ObservableCollection<OtherDesginProduct>();
            var CheckSelectedId = selectedOption.id;

            foreach (var item in CollectionMoreOptionsSource)
            {
                item.IsSelected = (item.id == CheckSelectedId);
                UpdateList.Add(item);
            }
            CollectionMoreOptionsSource = UpdateList;

            
                _DetailViewProductHomePageViewModel.price = selectedOption.price;
                _DetailViewProductHomePageViewModel.discount = selectedOption.Discound;
                _DetailViewProductHomePageViewModel.size = selectedOption.Size;
            
          //  await PopupNavigation.Instance.PopAsync(true);


        }

        

        public MoreOptionsPageViewModel(ObservableCollection<OtherDesginProduct>MoreProduct ,string MoreOptionCount , DetailViewProductHomePageViewModel detailViewModel )
		{
            
            CollectionMoreOptionsSource = MoreProduct;
            LblMoreOptionCount = MoreOptionCount;
            _DetailViewProductHomePageViewModel = detailViewModel;



        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}

