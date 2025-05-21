using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using Microsoft.Maui.Storage;
using ForBeautyMaui.ViewPages;
using ForBeauty.Pages.ReadPages;
using ForBeautyMaui.ViewPages.MainTappedPage;
using Microsoft.VisualBasic;
namespace ForBeautyMaui.ViewModels
{
    public class RegisterPageModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isLoginVisible;
        private bool _isRegisterVisible;
        private bool _isCheck;
        private string _LoadingButtonText = "متابعة";
        private string _EntryPhoneNumber;
        private string _BtnLoginName = "تسجيل الدخول";
        private string _ErrorMessageText;
        private bool _IsLoading;
        private bool _AnimationLoginBtn;
        private string _LoginPhoneEnt;
        private string _LoginPasswordEnt;
        private bool _IsPasswordBtn = true;
        private string _ImageshowHide = "hide";
        private bool _ErrorMessageLbl;
        private string _ErrorMessageLblText;
        private string _Copyright;
        private string _BtnLoginFontFamily ="times";
        private string _BtnRegisterFontFamily = "times";



        public ICommand TapCheckCommand { get; }
        public Command CommandReadMore { get; }
        public Command CommandReadPolicy { get; }
        public Command CommandCountinueRegister { get; }
        public ICommand TapShowHidePasswordTapped { get; set; }
        public Command LoginBtn { get; }
        public bool _Errorlbl;


        public RegisterPageModelView(string Duraction)
        {
            BtnLoginPage = new Command(ShowLogin);
            BtnRegisterPage = new Command(ShowRegister);
            TapCheckCommand = new Command(OnCheckTapped);
            CommandReadMore = new Command(NavigateToReadMore);
            CommandReadPolicy = new Command(ReadPolicy);
            CommandCountinueRegister = new Command(CountinueRegisterBtn);
            CommandToRegisterPage = new Command(ToRegitserPage);
            CommandToLoginPage = new Command(ToLoginPage);
            TapShowHidePasswordTapped = new Command(TapShowHidePassword);
            LoginBtn = new Command(Login);
            IsLoginVisible = true;
            IsRegisterVisible = false;
            CopyRight();
            LoginRegister(Duraction);



        }
        private void LoginRegister(string Duraction)
        {
            if (Duraction == "Login")
            {
                IsLoginVisible = true;
                IsRegisterVisible = false;
            }
            else
            {
                IsLoginVisible = false;
                IsRegisterVisible = true;

            }
        }
        private void CopyRight()
        {
            var currentYear = DateAndTime.Today.Year;
            _Copyright = $"all copy are saved 2022/{currentYear}";
        }
        public string Copyright
        {
            
            get => _Copyright;
            set
            {
                _Copyright = value;
                OnPropertyChanged(nameof(Copyright));
            }

        }

        private void TapShowHidePassword()
        {
            IsPasswordBtn = !IsPasswordBtn;
            ImageshowHide = IsPasswordBtn ? "hide" : "view";
        }

        private async void Login()
        {
            ErrorMessageLbl = false;
            if (LoginPhoneEnt == null || LoginPasswordEnt == null)
            {

                ErrorMessageLblText = "لا يمكن ترك الحقل فارغا";
                ErrorMessageLbl = true;
                return;
            }
            AnimationLoginBtn = true;
            BtnLoginName = string.Empty;
            string filterEntryPhoneNumber = LoginPhoneEnt;
            if (filterEntryPhoneNumber.StartsWith("0"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(1);
            }
            else if (filterEntryPhoneNumber.StartsWith("+218"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(3);
            }
            else if (filterEntryPhoneNumber.StartsWith("+218"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(4);
            }
            var response = await ApiSerives.Login("+218" + filterEntryPhoneNumber, LoginPasswordEnt);
            Preferences.Set("Phone_NumberForToken", "+218" + filterEntryPhoneNumber);
            Preferences.Set("Password_ForToken", LoginPasswordEnt);
            if (!response.Check)
            {
                await Shell.Current.DisplayAlert("", response.Message, "إلغاء");
                AnimationLoginBtn = false;
                BtnLoginName = "تسجيل الدخول";
            }
            else
            {
                AnimationLoginBtn = false;
                BtnLoginName = "تسجيل الدخول";
                Preferences.Set("Phone_Number", "0" + filterEntryPhoneNumber);
                Preferences.Set("Phone_NumberForChangePassword", "+218" + filterEntryPhoneNumber);
                Application.Current.MainPage = new NavigationPage(new TappedPages());
                var GetToken = new PushNotificationUserToken()
                {
                    UserId = Preferences.Get("user_Id", 0),
                    PushNotificationToken = Preferences.Get("PushNotificationToken", string.Empty)
                };
                Preferences.Set("IsRegister", true);
                await ApiSerives.GetPushNotificationTokenUser(GetToken);
            }
        }

        private void ToLoginPage()
        {
            ShowLogin();
            Application.Current.MainPage.Navigation.PushAsync(new MainLoginRegisterPage("Login"));
           
        }

        private async void ToRegitserPage()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new MainLoginRegisterPage("Register"));
        }


        private async void CountinueRegisterBtn()
        {
            IsLoading = true;
            LoadingButtonText = string.Empty;
            if (IsCheck == false)
            {
                ErrorLblVisible = true;
                ErrorMessageText = "يجب الموافقه علي الشروط والاحكام";
                LoadingButtonText = "متابعة";
                IsLoading = false;
                return;
            }
            string filterEntryPhoneNumber = EntPhoneNumber;
            if (filterEntryPhoneNumber.StartsWith("0"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(1);
            }
            else if (filterEntryPhoneNumber.StartsWith("218"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(3);
            }
            else if (filterEntryPhoneNumber.StartsWith("+218"))
            {
                filterEntryPhoneNumber = filterEntryPhoneNumber.Substring(4);
            }

            var response = await ApiSerives.CheckUser("+218" + filterEntryPhoneNumber);
            if (response.check)
            {

                await Application.Current.MainPage.Navigation.PushModalAsync(new SMSPage("+218" + filterEntryPhoneNumber));
                Preferences.Set("Phone_Number", "0" + filterEntryPhoneNumber);

            }
            else
            {
                if (response.result == "")
                {
                    ErrorMessageText = "هدا الرقم غير صالح يرجي التأكد من الرقم";
                    ErrorLblVisible = true;
                    IsLoading = false;
                    LoadingButtonText = "متابعة";

                }
                else
                {

                    ErrorMessageText = response.result;
                    ErrorLblVisible = true;
                    IsLoading = false;
                    LoadingButtonText = "متابعة";
                }
            }
        }

        private void ReadPolicy()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PrivacyPolicyPage());
        }

        private void NavigateToReadMore()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ReadMorePage());
        }

        private void OnCheckTapped()
        {


            IsCheck = !IsCheck;

        }
        public string BtnLoginName
        {
            get => _BtnLoginName;
            set
            {
                _BtnLoginName = value;
                OnPropertyChanged(nameof(BtnLoginName));
            }
        }
        public string ErrorMessageText
        {
            get => _ErrorMessageText;
            set
            {
                _ErrorMessageText = value;
                OnPropertyChanged(nameof(ErrorMessageText));
            }
        }
        public string ErrorMessageLblText

        {
            get => _ErrorMessageLblText;
            set
            {
                _ErrorMessageLblText = value;
                OnPropertyChanged(nameof(ErrorMessageLblText));
            }
        }
        public bool IsPasswordBtn
        {
            get => _IsPasswordBtn;
            set
            {
                _IsPasswordBtn = value;
                OnPropertyChanged(nameof(IsPasswordBtn));
            }
        }
        public bool ErrorMessageLbl
        {
            get => _ErrorMessageLbl;
            set
            {
                _ErrorMessageLbl = value;
                OnPropertyChanged(nameof(ErrorMessageLbl));
            }
        }
        public string ImageshowHide
        {
            get => _ImageshowHide;
            set
            {
                _ImageshowHide = value;
                OnPropertyChanged(nameof(ImageshowHide));
            }
        }
        public bool IsLoading
        {
            get => _IsLoading;
            set
            {
                _IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool AnimationLoginBtn
        {
            get => _AnimationLoginBtn;
            set
            {
                _AnimationLoginBtn = value;
                OnPropertyChanged(nameof(AnimationLoginBtn));
            }
        }



        public string LoadingButtonText
        {
            get => _LoadingButtonText;
            set
            {
                _LoadingButtonText = value;
                OnPropertyChanged(nameof(LoadingButtonText));
            }
        }

        public bool IsCheck
        {
            get => _isCheck;
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    OnPropertyChanged(nameof(IsCheck));
                    OnPropertyChanged(nameof(CheckBoxBackgroundColor));
                }
            }
        }
        public string LoginPhoneEnt
        {
            get => _LoginPhoneEnt;
            set
            {
                _LoginPhoneEnt = value;
                OnPropertyChanged(nameof(LoginPhoneEnt));
            }
        }
        public string LoginPasswordEnt
        {
            get => _LoginPasswordEnt;
            set
            {
                _LoginPasswordEnt = value;
                OnPropertyChanged(nameof(LoginPasswordEnt));
            }
        }
        public string EntPhoneNumber
        {
            get => _EntryPhoneNumber;
            set
            {
               
                _EntryPhoneNumber = value;
                OnPropertyChanged(nameof(EntPhoneNumber));
            }
        }
        public bool ErrorLblVisible
        {
            get => _Errorlbl;
            set
            {
                _Errorlbl = value;
                OnPropertyChanged(nameof(ErrorLblVisible));
            }
        }
        public bool IsLoginVisible
            
        {
            get => _isLoginVisible;
            set
            {
                _isLoginVisible = value;
                OnPropertyChanged(nameof(IsLoginVisible));
            }
        }
        
        public string BtnLoginFontFamily
        {
            get => _BtnLoginFontFamily;
            set 
            {
                _BtnLoginFontFamily = value;
                OnPropertyChanged(nameof(BtnLoginFontFamily));
            }
        }
        public string BtnRegisterFontFamily
        {
            get => _BtnRegisterFontFamily;
            set
            {
                _BtnRegisterFontFamily = value;
                OnPropertyChanged(nameof(BtnRegisterFontFamily));
            }
        }

        public bool IsRegisterVisible
        {
            
            get => _isRegisterVisible;
            set
            {
                _isRegisterVisible = value;
                OnPropertyChanged(nameof(IsRegisterVisible));
            }
        }

        public ICommand BtnLoginPage { get; set; }
        public ICommand BtnRegisterPage { get; set; }

        public Command CommandToRegisterPage { get; set; }
        public ICommand CommandToLoginPage { get; set; }

        private void ShowLogin()
        {
            IsLoginVisible = true;
            IsRegisterVisible = false;
            BtnLoginFontFamily = "times-bold";
            BtnRegisterFontFamily = "times";
        }

        private void  ShowRegister()
        {
            IsRegisterVisible = true;
            IsLoginVisible = false;
            BtnLoginFontFamily = "times";
            BtnRegisterFontFamily = "times-bold";

        }
        public Color CheckBoxBackgroundColor => IsCheck ? Colors.Black : Colors.White;



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

