

namespace ForBeautyMaui.ViewModels.ProfilePagesViewModel
{
	public class PrivacyPoliciesPageViewModel
	{
		private INavigation _navigation;
		public Command CommandPrivacyPoliciesBack { get; set; }

        public PrivacyPoliciesPageViewModel(INavigation navigation)
		{
			_navigation = navigation;
            CommandPrivacyPoliciesBack = new Command(NavigateBackPrivacyPolicies);

        }

        private void NavigateBackPrivacyPolicies()
        {
            _navigation.PopModalAsync();
        }
    }
}

