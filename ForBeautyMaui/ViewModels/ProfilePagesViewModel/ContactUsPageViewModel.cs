

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
    internal class ContactUsPageViewModel
    {
        private INavigation _navigation;
        public Command CommandContactUsBack { get; set; }
        public Command CommandHelpCall { get; set; }
        public ContactUsPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            CommandContactUsBack = new Command(NavigateBackContactUs);
            CommandHelpCall = new Command(ContactUs);
        }

        private void NavigateBackContactUs()
        {
            _navigation.PopModalAsync();
        }

        private void ContactUs()
        {
            string Phone = "0929500090";
            try
            {
                PhoneDialer.Open(Phone);
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}