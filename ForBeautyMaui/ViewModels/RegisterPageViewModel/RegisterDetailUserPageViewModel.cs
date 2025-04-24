using System;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels
{
    public class RegisterDetailUserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _PassEntry;
        private string _FnameEntry;
        private string _LnameEntry;
        private string _LblErrorText;
        private string _PassConfirmEntry;
        private string _PhoneNumber;
        private bool _LblErrorVisible;
        private Color _frameColorFname;
        private Color _frameColorLname;
        private Color _frameColorPassEntry;
        public Color _frameColorPassConfirmEntry;

        public Command CreateNewAccountBtn { get; }

        public string FnameEntry
        {
            get => _FnameEntry;
            set
            {
                _FnameEntry = value;
                OnPropertyChanged(nameof(FnameEntry));
            }
        }
        public string LnameEntry
        {
            get => _LnameEntry;
            set
            {
                _LnameEntry = value;
                OnPropertyChanged(nameof(LnameEntry));
            }
        }
        public string PassConfirmEntry
        {
            get => _PassConfirmEntry;
            set
            {
                _PassConfirmEntry = value;
                OnPropertyChanged(nameof(PassConfirmEntry));
            }
        }
        public Color frameColorFname
        {
            get => _frameColorFname;
            set
            {
                _frameColorFname = value;
                OnPropertyChanged(nameof(frameColorFname));
            }
        }
        public Color frameColorLname
        {
            get => _frameColorLname;
            set
            {
                _frameColorLname = value;
                OnPropertyChanged(nameof(frameColorLname));
            }
        }
        public Color frameColorPassEntry
        {
            get => _frameColorPassEntry;
            set
            {
                _frameColorPassEntry = value;
                OnPropertyChanged(nameof(frameColorPassEntry));
            }
        }
        public Color frameColorPassConfirmEntry
        {
            get => _frameColorPassConfirmEntry;
            set
            {
                _frameColorPassConfirmEntry = value;
                OnPropertyChanged(nameof(frameColorPassConfirmEntry));
            }
        }
        public string LblErrorText

        {
            get => _LblErrorText;
            set
            {
                _LblErrorText = value;
                OnPropertyChanged(nameof(LblErrorText));
            }
        }
        public bool LblErrorVisible

        {
            get => _LblErrorVisible;
            set
            {
                _LblErrorVisible = value;
                OnPropertyChanged(nameof(LblErrorVisible));
            }
        }

        public RegisterDetailUserPageViewModel(string PhoneNumber)
		{
            _PhoneNumber = PhoneNumber;
            CreateNewAccountBtn = new Command(createAccount);
		}

        private async void createAccount()
        {
            //var CurrentDevice = UIKit.UIDevice.CurrentDevice.Name + " " + UIKit.UIDevice.CurrentDevice.SystemName + " " + UIKit.UIDevice.CurrentDevice.SystemVersion;


            if (string.IsNullOrEmpty(FnameEntry))
            {
                frameColorFname = Colors.Red;
                LblErrorVisible = true;
                LblErrorText = "لايمكن ترك الحقل فارغا *";
                return;
            }
            else
            {
                frameColorFname = Colors.Gray;
                LblErrorVisible = false;
            }

            if (string.IsNullOrEmpty(LnameEntry))
            {
                frameColorLname = Colors.Red;
                LblErrorVisible = true;
                LblErrorText = "لايمكن ترك الحقل فارغا *";
                return;
            }
            else
            {
                frameColorLname = Colors.Gray;
                LblErrorVisible = false;
            }
            if (string.IsNullOrEmpty(PassEntry))
            {
                frameColorPassEntry = Colors.Red;
                LblErrorVisible = true;
                LblErrorText = "لايمكن ترك الحقل فارغا *";
                return;
            }
            else
            {
                frameColorPassEntry = Colors.Gray;
                LblErrorVisible = false;
            }
            if (string.IsNullOrEmpty(PassConfirmEntry))
            {
                frameColorPassConfirmEntry = Colors.Red;
                LblErrorVisible = true;
                LblErrorText = "لايمكن ترك الحقل فارغا *";
                return;
            }
            else
            {
                frameColorPassEntry = Colors.Gray;
                LblErrorVisible = false;
            }
            if (PassEntry != PassConfirmEntry)
            {
                LblErrorVisible = true;
                LblErrorText = "كلمة السر غير متطابقة *";
                return;
            }
            else
            {
                LblErrorVisible = false;
            }
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool atLeast8 = PassEntry.Length >= 8;
            foreach (char c in PassEntry)
            {
                if (char.IsLower(c))
                {
                    hasLowercase = true;

                }
                if (char.IsUpper(c))
                {
                    hasUppercase = true;

                }
            }

            if (!hasUppercase || !hasLowercase || !atLeast8)
            {
                LblErrorVisible = true;
                LblErrorText = "خطأ في إدخال كلمة المرور *";
                return;
            }
            else
            {
                LblErrorVisible = false;
                LblErrorText = "";
            }
            var response = await ApiSerives.Register(FnameEntry, LnameEntry, PassEntry, _PhoneNumber,null);
            if (response)
            {
                Login();
            }
            else
            {
                LblErrorVisible = true;
                LblErrorText = "حدث خطأ يرجي المحاوله مرة أخري ";

            }
        }
        private async void Login()
        {
            string filterEntryPhoneNumber = _PhoneNumber;
            
            var response = await ApiSerives.Login(filterEntryPhoneNumber, PassEntry);
            Preferences.Set("Phone_NumberForToken", filterEntryPhoneNumber);
            Preferences.Set("Password_ForToken", PassEntry);
            if (!response.Check)
            {
                await Application.Current.MainPage.DisplayAlert("", response.Message, "إلغاء");
            }
            else
            {
                Preferences.Set("Phone_Number", "0" + filterEntryPhoneNumber);
                Preferences.Set("Phone_NumberForChangePassword", filterEntryPhoneNumber);
               // Application.Current.MainPage = new NavigationPage(new TappedPages());
                var GetToken = new PushNotificationUserToken()
                {
                    UserId = Preferences.Get("user_Id", 0),
                    PushNotificationToken = Preferences.Get("PushNotificationToken", string.Empty)
                };
                Preferences.Set("IsRegister", true);
                await ApiSerives.GetPushNotificationTokenUser(GetToken);
            }
        }

        public string PassEntry
        {
            get => _PassEntry;
            set
            {
                _PassEntry = value;
                OnPropertyChanged(nameof(PassEntry));
            }
            
        }
        private void HandelPassEntryChanged()
        {
            
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

