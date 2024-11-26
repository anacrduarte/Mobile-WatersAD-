using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
	public partial class LoginViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly IDataValidator _validator;
		private readonly AuthService _authService;
		private readonly INavigationService _navigationService;
		[ObservableProperty]
		private string email = null!;

		[ObservableProperty]
		private string password = null!;

		[ObservableProperty]
		private string emailErrorMessage = null!;

		[ObservableProperty]
		private string passwordErrorMessage = null!;


		public IAsyncRelayCommand SignInCommand { get; }

		public IAsyncRelayCommand ForgotPasswordCommand { get; }


		public LoginViewModel(ApiService apiService, IDataValidator dataValidator, AuthService authService, INavigationService navigationService)
		{
			_apiService = apiService;
			_validator = dataValidator;
			_authService = authService;
			_navigationService = navigationService;
			SignInCommand = new AsyncRelayCommand(OnSignIn);
			ForgotPasswordCommand = new AsyncRelayCommand(async () => await NavigateToForgotPassword()); ;
		}


        private async Task OnSignIn()
        {
            try
            {
                bool isEmailValid = _validator.ValidateEmail(Email);
                bool isPasswordValid = _validator.ValidatePassword(Password);

                EmailErrorMessage = _validator.EmailError;
                PasswordErrorMessage = _validator.PasswordError;

                if (!isEmailValid || !isPasswordValid)
                {
                    return;
                }

                var loginModel = new Login
                {
                    UserName = Email,
                    Password = Password,
                };

                var response = await _apiService.Login(loginModel);

                if (!response.HasError)
                {
                    _authService.Login();
                    EmailErrorMessage = string.Empty;
                    PasswordErrorMessage = string.Empty;
                    await ReconfigureShellAsync();
                    //await NavigateToHome();
                    //await _navigationService.SetMainPageAsync<AppShell>();
                    //Application.Current!.MainPage = new AppShell(_apiService, _validator, _authService, _navigationService);
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Erro", $"{response.ErrorMessage}.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"{ex.Message}.", "OK");
            }
        }


        private async Task NavigateToForgotPassword()
		{

			await _navigationService.NavigateToAsync<ForgotPasswordPage>();
		}

        private async Task NavigateToHome()
        {

            await _navigationService.NavigateToAsync<HomePage>();
        }

        private async Task ReconfigureShellAsync()
        {
            // Obtenha o estado de login novamente
            bool isUserLoggedIn = _authService.IsLoggedIn();

            // Redefine o Shell
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
                new ShellContent { Title = "Conta", Icon="account", Content = new ProfileSettingsPage(new ProfileSettingsViewModel(_authService, _apiService, _validator, _navigationService)) }
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
                new ShellContent { Title = "Login", Icon="login", Content = new LoginPage(new LoginViewModel(_apiService, _validator, _authService, _navigationService)) }
            }
                });
            }

            await NavigateToHome();
        }

    }
}
