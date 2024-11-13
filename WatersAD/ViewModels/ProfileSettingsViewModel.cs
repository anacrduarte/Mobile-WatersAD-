using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WatersAD.Config;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
    public partial class ProfileSettingsViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly IDataValidator _dataValidator;
		private readonly INavigationService _navigationService;
		
        private readonly ApiService _apiService;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private string firstName = null!;
        [ObservableProperty]
        private string lastName = null!;
        [ObservableProperty]
        private string imageUrl = null!;
        [ObservableProperty]
        public string? imagePath;
        [ObservableProperty]
        public string? fullName;

        public ProfileSettingsViewModel(AuthService authService, ApiService apiService, IDataValidator dataValidator, INavigationService navigationService)
        {
            _authService = authService;
            _dataValidator = dataValidator;
			_navigationService = navigationService;
			_apiService = apiService;
            LogoutCommand = new AsyncRelayCommand(ExecuteLogoutAsync);
			NavigateToProfileCommand = new AsyncRelayCommand(async () => await NavigateToProfile());
		}

        public IAsyncRelayCommand LogoutCommand { get; }

        public IAsyncRelayCommand NavigateToProfileCommand { get; }

        public void Initialize()
        {

            FirstName = Preferences.Get("firstname", string.Empty);
            LastName = Preferences.Get("lastname", string.Empty);
            ImageUrl = Preferences.Get("imageurl", string.Empty);

            FullName = $"{FirstName} {LastName}";

            if (!string.IsNullOrEmpty(ImageUrl))
            {
                ImagePath = $"{AppConfig.BaseUrl.TrimEnd('/')}/{ImageUrl.TrimStart('~', '/')}";
            }
            else
            {
                ImagePath = AppConfig.ProfileDefaultImage;
            }

        }

        private async Task ExecuteLogoutAsync()
        {
            try
            {
                 _authService.Logout(); 
                var response = await _apiService.LogoutAsync(); ;
                if (response)
                {
                    Preferences.Set("accesstoken", string.Empty);
                    Preferences.Set("userid", string.Empty);
                    Preferences.Set("username", string.Empty);
                    Preferences.Set("firstname", string.Empty);
                    Preferences.Set("lastname", string.Empty);
                    Preferences.Set("imageurl", string.Empty);

                   

					await _navigationService.SetMainPageAsync<AppShell>();


				}
                else
                {
                    ErrorMessage = "Failed to upload data";
                }
                

            
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine($"Logout failed: {ex.Message}");
            }
        }

		private async Task NavigateToProfile()
		{
			
			await _navigationService.NavigateToAsync<ProfilePage>();
		}
	}
}
