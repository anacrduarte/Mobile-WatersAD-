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
			NavigateToChangePasswordCommand = new AsyncRelayCommand(async () => await NavigateToChangePassword());
			NavigateToQuestionsCommand = new AsyncRelayCommand(async () => await NavigateToQuestions());
		}

	

		public IAsyncRelayCommand LogoutCommand { get; }

		public IAsyncRelayCommand NavigateToProfileCommand { get; }

		public IAsyncRelayCommand NavigateToChangePasswordCommand { get; }

		public IAsyncRelayCommand NavigateToQuestionsCommand { get; }
		public void Initialize()
		{

			FirstName = Preferences.Get("firstname", string.Empty);
			LastName = Preferences.Get("lastname", string.Empty);
			ImageUrl = Preferences.Get("imageurl", string.Empty);

			FullName = $"{FirstName} {LastName}";

			if (!string.IsNullOrEmpty(ImageUrl) && !ImageUrl.Contains("noimage.png"))
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

					

                    await ReconfigureShellAsync();

                


                }
				else
				{
                    await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro a receber os dados", "OK");
                }



			}
			catch (Exception ex)
			{

				await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro {ex.Message}", "OK");
			}
		}

        private async Task NavigateToHome()
        {

            await _navigationService.NavigateToAsync<HomePage>();
        }
        private async Task NavigateToProfile()
		{

			await _navigationService.NavigateToAsync<ProfilePage>();
		}

		private async Task NavigateToChangePassword()
		{

			await _navigationService.NavigateToAsync<ChangePasswordPage>();
		}

		private async Task NavigateToQuestions()
		{
			await _navigationService.NavigateToAsync<QuestionsPage>();
		}

        private async Task ReconfigureShellAsync()
        {
            
            bool isUserLoggedIn = _authService.IsLoggedIn();

            
            var shell = Application.Current.MainPage as AppShell;
            shell.Items.Clear();

            if (isUserLoggedIn)
            {
                shell.Items.Add(new TabBar
                {
                    Items =
            {
                new ShellContent { Title = "Home", Icon="home", Content = new HomePage(new HomePageViewModel()) },
                new ShellContent { Title = "Preços", Icon="pricelist", Content = new ServicePriceListPage(new TiersViewModel(_apiService)) },
                new ShellContent { Title = "Leitura", Icon="watermeter", Content = new AddConsumptionPage(new AddCosumptionViewModel(_apiService, _navigationService)) },
                new ShellContent { Title = "Consumos", Icon="watertap", Content = new ConsumptionsAndInvoicesPage(new ConsumptionInvoiceViewModel(_apiService, _navigationService)) },
                new ShellContent { Title = "Conta", Icon="account", Content = new ProfileSettingsPage(new ProfileSettingsViewModel(_authService, _apiService, _dataValidator, _navigationService)) }
            }
                });
            }
            else
            {
                shell.Items.Add(new TabBar
                {
                    Items =
            {
                new ShellContent { Title = "Home", Icon="home", Content = new HomePage(new HomePageViewModel()) },
                new ShellContent { Title = "Preços", Icon="pricelist", Content = new ServicePriceListPage(new TiersViewModel(_apiService)) },
                new ShellContent { Title = "Contador", Icon="watertap", Content = new RequestWaterMeterPage(new RequestWaterMeterViewModel(_apiService, _navigationService)) },
                new ShellContent { Title = "Info", Icon="info", Content = new InformationPage() },
                new ShellContent { Title = "Login", Icon="login", Content = new LoginPage(new LoginViewModel(_apiService, _dataValidator, _authService, _navigationService)) }
            }
                });
            }

            await NavigateToHome();
        }

    }
}
