using System.ComponentModel;

namespace ForBeautyMaui.ViewPages.MainTappedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TappedPages : TabbedPage ,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _NavigationBar = true;
        public bool NavigationBar
        { get => _NavigationBar;
            set
            {
                if (_NavigationBar != value)
                {
                    _NavigationBar = value;
                    OnPropertyChanged(nameof(NavigationBar));
                }
                ;
            } }                
        
        public TappedPages ()
        {
            InitializeComponent();
            BindingContext = this;


        }

        protected  void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private async void delay()
        {
           await Task.Delay(300);
            NavigationBar = false;
        }
        



    }
}
