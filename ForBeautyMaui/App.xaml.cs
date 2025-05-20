using ForBeautyMaui.ApiServices;
using ForBeautyMaui.Renders;
using ForBeautyMaui.ViewModels;
using ForBeautyMaui.ViewPages;
using ForBeautyMaui.ViewPages.MainTappedPage;
using ForBeautyMaui.ViewPages.MainTappedPage.Searchpages;

namespace ForBeautyMaui
{
    public partial class App : Application
    {
        private DateTime appBackgroundTime;

        public static SharedServicesViewModel SharedServices;

        public App()
        {

            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzg2ODI1MkAzMjM5MmUzMDJlMzAzYjMyMzkzYmZvejgzMXZabTN5VTB6SkN6Vnl5SFFGWDNhOXphaEdFdjdvVzNiZnNPUzg9");

        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            var current = Connectivity.NetworkAccess;
            SharedServices = new SharedServicesViewModel();
            Routing.RegisterRoute("DetailCateogryNamePage", typeof(DetailCateogryNamePage));


            if (current != NetworkAccess.Internet)
            {
                // MainPage = new NavigationPage(new ConnectionPage());
                return new Window(new NavigationPage(new MainLoginRegisterPage()));

            }
            else
            {

                if (Preferences.Get("access_token", string.Empty) != string.Empty)
                {
                    return new Window(new NavigationPage(new TappedPages()));
                    Checkblocked();
                    //GetToken();

                }
                else
                {

                    return new Window(new NavigationPage(new WelcomePage()));


                }
            }
        }
            private async void Checkblocked()
        {
            int userId = Preferences.Get("user_Id", 0);
            var response = await ApiSerives.IsBlocked(userId);
            if (response)
            {
                if (MainPage is Page mpage)
                {
                    await mpage.DisplayAlert("", "تم تغير الاعدادات, يرجي إعادة تسجيل الدخول ", "موافق");
                    Preferences.Set("access_token", string.Empty);
                    Preferences.Set("expiration_Time", 0);
                    MainPage = new NavigationPage(new WelcomePage());

                }
            }
        }

    }
}