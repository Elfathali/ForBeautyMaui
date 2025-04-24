using System;
using System.ComponentModel;
using ForBeautyMaui.ApiServices;
using ForBeauty.Models;

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class PersonalDeatilPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        public Command BtnSaveData { get; set;}
        private string _lblpersonalErrorText;
        private bool _lblpersonalError;
		private string _EntryFName;
		private string _EntryLName;
		private string _LblPhoneNumber;

		public bool lblpersonalError
		{
			get => _lblpersonalError;
			set
			{
				if (_lblpersonalError != value)
				{
					_lblpersonalError = value;
					OnPropertyChanged(nameof(lblpersonalError));
				}
            }

        }
        public string lblpersonalErrorText
        {
            get => _lblpersonalErrorText;
            set
            {
                if (_lblpersonalErrorText != value)
                {
                    _lblpersonalErrorText = value;
                    OnPropertyChanged(nameof(lblpersonalErrorText));
                }
            }

        }
        public string EntryFName
        {
            get => _EntryFName;
            set
            {
				if (_EntryFName != value)
				{
					_EntryFName = value;
					OnPropertyChanged(nameof(EntryFName));
				}

            }

        }
        public string EntryLName
        {
            get => _EntryLName;
            set
            {
                if (_EntryLName != value)
                {
                    _EntryLName = value;
                    OnPropertyChanged(nameof(EntryLName));
                }

            }

        }
        public string LblPhoneNumber
        {
            get => _LblPhoneNumber;
            set
            {
                if (_LblPhoneNumber != value)
                {
                    _LblPhoneNumber = value;
                    OnPropertyChanged(nameof(LblPhoneNumber));
                }

            }

        }


        public PersonalDeatilPageViewModel()
		{
            GetPersonalData();
            BtnSaveData = new Command(SavePersonalData);
            

        }
        private async void SavePersonalData()
        {
            if (string.IsNullOrEmpty(EntryFName))
            {
                lblpersonalErrorText = "لا يمكن ترك الحقول فارغة *";
                lblpersonalError = true;
                return;
            }
            else
            {
                lblpersonalErrorText = "";
                lblpersonalError = false;
            }

            if (string.IsNullOrEmpty(EntryLName))
            {
                lblpersonalErrorText = "لا يمكن ترك الحقول فارغة *";
                lblpersonalError = true;
                return;
            }
            else
            {
                lblpersonalErrorText = "";
                lblpersonalError = false;
            }
            var up = new UserInformation()
            {
                FName = EntryFName,
                LName = EntryLName,


            };
            var update = await ApiSerives.UpdateUserInformation(Preferences.Get("user_Id", 0), up);
            if (update)
            {
                Preferences.Set("user_name", up.FName);
               // await DisplayAlert("", "تم تغير اسمك بنجاح", "متابعة");
                Preferences.Get("user_name", string.Empty);
            }
            else
            {
              //  await DisplayAlert("", "حدث خطأ يرجي اعادة المحاولة", "الغاء");
            }
        }
        private async void GetPersonalData()
        {
            var response = await ApiSerives.GetPersonalDeatil(Preferences.Get("user_Id", 0));
            EntryFName = response.FName;
            EntryLName = response.LName;
            LblPhoneNumber = response.PhoneNumber;
        }
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}

