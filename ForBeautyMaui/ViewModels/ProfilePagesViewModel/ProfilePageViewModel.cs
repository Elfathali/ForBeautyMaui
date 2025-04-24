using System;
using System.ComponentModel;
using ForBeauty.Pages;
using ForBeautyMaui.ViewPages;
using ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages;


namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        private string _LblWelcomeNameProfile;
        public Command CommadPreveuseOrder { get; set; }
        public Command CommandPersonalProfile { get; set; }
        public Command CommandChangePassword { get; set; }
        public Command CommandSettingPage { get; set; }
        public Command CommandTellFreind { get; set; }
        public Command commandAboutUs { get; set; }
        public Command CommandPrivacyPolices { get; set; }
        public Command CommandContactUs { get; set; }
        public Command HelpANDFAQ { get; set; }
        private FlowDirection _FlowDirection;
        public Command LogOutTap { get; set; }
        private bool _StackLoginOrRegisterAccountVisiblety;
        private bool _StackPersonalDitalAccountVisiblety;
        private double _ScalexDireaction;
        public string LblWelcomeNameProfile
         {
            get => _LblWelcomeNameProfile;
            set
            {
                if (_LblWelcomeNameProfile != value)
                {
                    _LblWelcomeNameProfile = value;
                    OnPropertyChanged(nameof(_LblWelcomeNameProfile));
                }
            }
         }
        public bool StackLoginOrRegisterAccountVisiblety
        {
            get => _StackLoginOrRegisterAccountVisiblety;
            set
            {
                _StackLoginOrRegisterAccountVisiblety = value;
                OnPropertyChanged(nameof(StackLoginOrRegisterAccountVisiblety));
            }
        }
        public bool StackPersonalDitalAccountVisiblety
        {
            get => _StackPersonalDitalAccountVisiblety;
            set
            {
                _StackPersonalDitalAccountVisiblety = value;
                OnPropertyChanged(nameof(StackPersonalDitalAccountVisiblety));
            }
                
        }
        private void CheckToken()
        {
            if (Preferences.Get("access_token", string.Empty) != string.Empty)
            {
                StackLoginOrRegisterAccountVisiblety = false;
                StackPersonalDitalAccountVisiblety = true;

            }
            else
            {
                StackLoginOrRegisterAccountVisiblety = true;
                StackPersonalDitalAccountVisiblety = false;
            }
        }

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
        public double ScalexDireaction
        {
            get => _ScalexDireaction;
            set
            {
                _ScalexDireaction = value;
                OnPropertyChanged(nameof(ScalexDireaction));
            }
        }

        public ProfilePageViewModel(INavigation navigation)
		{
            _navigation = navigation;
            CommadPreveuseOrder = new Command(NavigateToPreveusePage);
            CommandPersonalProfile = new Command(NavigateToPersonalDetailPage);
            CommandChangePassword = new Command(NavigateToChangePassPage);
            CommandSettingPage = new Command(NavigateToSettingsPage);
            CommandTellFreind = new Command(NavigateToTellFreindPage);
            commandAboutUs = new Command(NavigateToAboutUs);
            CommandPrivacyPolices = new Command(NavigateToPrivacyPoliciesPage);
            CommandContactUs = new Command(NavigateToContactUs);
            HelpANDFAQ = new Command(NavigateToHelpANDFAQ);
            LogOutTap = new Command(LogOut);
            GetUserName();
            CheckToken();
            flowdirections();


        }

        private async void LogOut()
        {
            bool logoutConfirmed = await App.Current.MainPage.DisplayAlert("تسجيل الخروج", "هل انت متأكد من تسجيل الخروج ؟", "نعم", "لا");
            if (logoutConfirmed)
            {
                Preferences.Set("access_token", string.Empty);
                Preferences.Set("expiration_Time", 0);
                Preferences.Set("Phone_NumberForToken", 0);
                Preferences.Set("Password_ForToken", string.Empty);
                App.Current.MainPage = new NavigationPage(new WelcomePage());
            }

            else
            {

            }
        }

        private void flowdirections()
        {
            string currentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            flowdirecation = currentLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            ScalexDireaction = currentLanguage == "ar" ? 1 : -1;
        }

        private void NavigateToPrivacyPoliciesPage()
        {
            _navigation.PushModalAsync(new PrivacyPoliciesPage());
        }

        private void NavigateToContactUs()
        {
            _navigation.PushModalAsync(new ContactUsPage());
        }

        private void NavigateToHelpANDFAQ()
        {
            _navigation.PushModalAsync(new HelpAndFAQPage());
        }

        private void NavigateToAboutUs()
        {
            _navigation.PushModalAsync(new AboutUsPage());
        }

        private void NavigateToTellFreindPage()
        {
            _navigation.PushAsync(new tellFreindPage());
        }

        private void GetUserName()
        {
            string username = Preferences.Get("user_name", string.Empty);
            LblWelcomeNameProfile = "مرحبا " + username + " !";
        }

        private void NavigateToSettingsPage()
        {
            _navigation.PushAsync(new SettingsPage());
        }

        private void NavigateToChangePassPage()
        {
            _navigation.PushAsync(new ChangePasswordPage());
        }

        private void NavigateToPersonalDetailPage()
        {
            _navigation.PushAsync(new PersonalDetailPage());
        }

        public void NavigateToPreveusePage()
        {
            _navigation.PushAsync(new PreveuseOrderPage());
            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

