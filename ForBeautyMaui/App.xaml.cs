using ForBeautyMaui.ViewModels;
using ForBeautyMaui.ViewPages;
using ForBeautyMaui.ViewPages.MainTappedPage;
using ForBeautyMaui.ViewPages.MainTappedPage.FavouritePages;
using ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages;

namespace ForBeautyMaui
{
    public partial class App : Application
    {
        public static SharedServicesViewModel SharedServices;
        public App()
        {
            InitializeComponent();
            SharedServices = new SharedServicesViewModel();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new WelcomePage()));
        }
    }
}