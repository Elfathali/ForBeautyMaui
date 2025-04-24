using System;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;
using ForBeautyMaui.ViewPages;

namespace ForBeautyMaui.ViewModels
{
	public class SMSPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _otpText;
        private string _phoneNumber;
        private bool _isOtpEnabled;
        private bool _isIncorrectCodeVisible;
        private string _incorrectCodeMessage;
        private Color _boxOtp1Color;
        private Color _boxOtp2Color;
        private Color _boxOtp3Color;
        private Color _boxOtp4Color;

        public string OtpText
        {
            get => _otpText;
            set
            {
                if (_otpText != value)
                {
                    _otpText = value;
                    OnPropertyChanged(nameof(OtpText));
                    HandleOtpTextChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public bool IsOtpEnabled
        {
            get => _isOtpEnabled;
            set
            {
                _isOtpEnabled = value;
                OnPropertyChanged(nameof(IsOtpEnabled));
            }
        }

        public bool IsIncorrectCodeVisible
        {
            get => _isIncorrectCodeVisible;
            set
            {
                _isIncorrectCodeVisible = value;
                OnPropertyChanged(nameof(IsIncorrectCodeVisible));
            }
        }

        public string IncorrectCodeMessage
        {
            get => _incorrectCodeMessage;
            set
            {
                _incorrectCodeMessage = value;
                OnPropertyChanged(nameof(IncorrectCodeMessage));
            }
        }

        public Color BoxOtp1Color
        {
            get => _boxOtp1Color;
            set
            {
                _boxOtp1Color = value;
                OnPropertyChanged(nameof(BoxOtp1Color));
            }
        }

        public Color BoxOtp2Color
        {
            get => _boxOtp2Color;
            set
            {
                _boxOtp2Color = value;
                OnPropertyChanged(nameof(BoxOtp2Color));
            }
        }

        public Color BoxOtp3Color
        {
            get => _boxOtp3Color;
            set
            {
                _boxOtp3Color = value;
                OnPropertyChanged(nameof(BoxOtp3Color));
            }
        }

        public Color BoxOtp4Color
        {
            get => _boxOtp4Color;
            set
            {
                _boxOtp4Color = value;
                OnPropertyChanged(nameof(BoxOtp4Color));
            }
        }

        public Command ConfirmCommand { get; }
        public Command ResendCodeCommand { get; }

        public SMSPageViewModel(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ConfirmCommand = new Command(async () => await CheckCode());
            ResendCodeCommand = new Command(async () => await ResendCode());
            IsOtpEnabled = true;

            // Initialize default colors
            BoxOtp1Color = Colors.Black;
            BoxOtp2Color = Colors.Black;
            BoxOtp3Color = Colors.Black;
            BoxOtp4Color = Colors.Black;
        }

        private async Task CheckCode()
        {
            IsOtpEnabled = false;
            IsIncorrectCodeVisible = false;
            var confirm = new Confirm
            {
                Code = OtpText,
                phoneNumber = PhoneNumber
            };
            var response = await ApiSerives.Confirm(confirm);
            if (response == "approved")
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterDetailUserPage(PhoneNumber));
            }
            else
            {
                IncorrectCodeMessage = "رقم التأكيد غير صالح *";
                OtpText = string.Empty;
                IsIncorrectCodeVisible = true;
                IsOtpEnabled = true;
            }
        }

        private async Task ResendCode()
        {
            var response = await Application.Current.MainPage.DisplayAlert("إعادة الارسال", "", "نعم", "لا");
            if (response)
            {
               // Application.Current.MainPage = new NavigationPage(new RegisterAccount("Register"));
            }
        }

        private void HandleOtpTextChanged()
        {
            switch (OtpText?.Length)
            {
                case 1:
                    BoxOtp1Color = Colors.Green;
                    BoxOtp2Color = Colors.Black;
                    BoxOtp3Color = Colors.Black;
                    BoxOtp4Color = Colors.Black;
                    break;
                case 2:
                    BoxOtp1Color = Colors.Green;
                    BoxOtp2Color = Colors.Green;
                    BoxOtp3Color = Colors.Black;
                    BoxOtp4Color = Colors.Black;
                    break;
                case 3:
                    BoxOtp1Color = Colors.Green;
                    BoxOtp2Color = Colors.Green;
                    BoxOtp3Color = Colors.Green;
                    BoxOtp4Color = Colors.Black;
                    break;
                case 4:
                    BoxOtp1Color = Colors.Green;
                    BoxOtp2Color = Colors.Green;
                    BoxOtp3Color = Colors.Green;
                    BoxOtp4Color = Colors.Green;
                    ConfirmCommand.Execute(null);
                    break;
                default:
                    BoxOtp1Color = Colors.Black;
                    BoxOtp2Color = Colors.Black;
                    BoxOtp3Color = Colors.Black;
                    BoxOtp4Color = Colors.Black;
                    break;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

