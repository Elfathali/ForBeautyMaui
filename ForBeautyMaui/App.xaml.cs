using ForBeautyMaui.ViewModels;
using ForBeautyMaui.ViewPages;
using ForBeautyMaui.ViewPages.MainTappedPage;

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