using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages.MainTappedPage.Searchpages;

namespace ForBeautyMaui.ViewModels.SearchPageTappedviewModel
{
	public class SearchPageViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        private Categories _ListCategorySelectedItem;
        private bool _SearchLoaderVisiblity;
        public ObservableCollection<Categories> ListCategorySource { get; set; } = new ObservableCollection<Categories>();

        public SearchPageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            SearchLoaderVisiblity = true;
            GetCategoriesSearch();

        }
        public bool SearchLoaderVisiblity
        {
            get => _SearchLoaderVisiblity;
            set
            {
                _SearchLoaderVisiblity = value;
                OnPropertyChanged(nameof(SearchLoaderVisiblity));
            }
        }
        public Categories ListCategorySelectedItem
        {
            get => _ListCategorySelectedItem;
            set
            {
                _ListCategorySelectedItem = value;
                OnPropertyChanged(nameof(ListCategorySelectedItem));
                OnItemSelect(_ListCategorySelectedItem);
            }
        }

        private async void OnItemSelect(Categories categories )
        {
            if (categories != null)
            {
              await  _navigation.PushAsync(new DetailCateogryNamePage(categories ,null));
                ListCategorySelectedItem = null;

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ListCategorySource = new ObservableCollection<Categories>();
        }
        private async void GetCategoriesSearch()
        {
           var response = await ApiSerives.CategorySearch();
            foreach (var item in response)
            {
                ListCategorySource.Add(item);
            }
            await Task.Delay(300);
            SearchLoaderVisiblity = false;
        }
    }
}

