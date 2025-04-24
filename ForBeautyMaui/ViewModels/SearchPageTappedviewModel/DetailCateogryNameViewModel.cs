using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.Searchpages;

namespace ForBeautyMaui.ViewModels.SearchPageTappedviewModel
{
	public class DetailCateogryNameViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CategorDital> ProductListSource { get; set; } = new ObservableCollection<CategorDital>();
        public Command AllCategoryTap { get; set; }
        private string _TitleName;
        private bool _StackActivityVisbilty = true;
        private CategorDital _ProductListSelectedItem;
        private Categories _categories;
        private INavigation _navigation;
        private string _LblAllCategory;
        private string _LblCategorditalsname;
        public string LblAllCategory
        {
            get => _LblAllCategory;
            set
            {
                if (_LblAllCategory != value)
                {
                    _LblAllCategory = value;
                    OnPropertyChanged(nameof(LblAllCategory));
                }
            }
        }
        public bool StackActivityVisbilty
        {
            get => _StackActivityVisbilty;
            set
            {
                _StackActivityVisbilty = value;
                OnPropertyChanged(nameof(StackActivityVisbilty));
            }
        }
        public string TitleName
        { get => _TitleName;
            set
            {
                if (_TitleName != value)
                {
                    _TitleName = value;
                    OnPropertyChanged(nameof(TitleName));
                }
            }
        }
        public string LblCategorditalsname
        {
            get => _LblCategorditalsname;
            set
            {
                if (_LblCategorditalsname != value)
                {
                    _LblCategorditalsname = value;
                    OnPropertyChanged(nameof(LblCategorditalsname));
                }
            }
        }
        public CategorDital ProductListSelectedItems
        {
            get => _ProductListSelectedItem;
            set
            {
                if (_ProductListSelectedItem != value)
                {
                    _ProductListSelectedItem = value;
                    OnPropertyChanged(nameof(ProductListSelectedItems));
                    OnItemSelectDeatilCategoryByName(_categories,_ProductListSelectedItem);
                }
            }
        }


        public DetailCateogryNameViewModel(INavigation navigation , Categories categories , CategorDital categorDital )
		{
            _navigation = navigation;
            ProductListSource = new ObservableCollection<CategorDital>();
            _categories = categories;
            AllCategoryTap = new Command(() =>ShowAllProduct(categories , categorDital));
            GetDeatilCategory(categories);
            TitleName = categories.CategoryName;
        }

        private async void ShowAllProduct(Categories categories ,CategorDital categorDital)
        {
            await _navigation.PushAsync(new ProductByCategoryPage(categories ,null , categories.CategoryName ));
        }

        
        private async void GetDeatilCategory(Categories categories)
        {
            StackActivityVisbilty = true;
            var response = await ApiSerives.NumberOfCategory(categories.id);
            LblAllCategory = "الكل " + "(" + response.ToString() + ")";
            LblCategorditalsname = categories.CategoryName;
            GetDitalCateogy(categories.id);
            
        }
        private async void OnItemSelectDeatilCategoryByName( Categories categories , CategorDital categorDital)
        {
            if (categorDital != null)
            {
                await _navigation.PushAsync(new ProductByCategoryPage(null, categorDital , categorDital.nameDital ));

                ProductListSelectedItems = null;
                OnPropertyChanged(nameof(ProductListSelectedItems));

            }

            
        }
        private async void GetDitalCateogy(int id)
        {
            StackActivityVisbilty = true;
            var respnse = await ApiSerives.CategorDitals(id);
            foreach (var item in respnse)
            {
                ProductListSource.Add(item);
            }
           await Task.Delay(200);
            StackActivityVisbilty = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

