using System.ComponentModel.DataAnnotations;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.ViewModels;
using WatersAD.Views;

namespace WatersAD
{
    public partial class AppShell : Shell
    {
        private readonly ApiService _apiService;
        private readonly IDataValidator _validator;
        private readonly AuthService _authService;
		private readonly INavigationService _navigationService;

		public AppShell(ApiService apiService, IDataValidator validator, AuthService authService, INavigationService navigationService)
        {
            InitializeComponent();
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            _authService = authService;
			_navigationService = navigationService;
			ConfigureShell();
        }

        private void ConfigureShell()
        {
            var homePage = new HomePage();
            var servicePage = new ServicePriceListPage(new TiersViewModel(_apiService));
            var requestPage = new RequestWaterMeterPage(new RequestWaterMeterViewModel(_apiService, _navigationService));
            var perfilPage = new ProfileSettingsPage(new ProfileSettingsViewModel(_authService, _apiService, _validator, _navigationService));
            var loginPage = new LoginPage(new LoginViewModel(_apiService, _validator, _authService, _navigationService));

            bool isUserLoggedIn = _authService.IsLoggedIn();

            if (isUserLoggedIn)
            {
                Items.Add(new TabBar
                {
                    Items =
                    {
                        new ShellContent {Title = "Home", Icon="home",Content = homePage  },
                        new ShellContent { Title = "Preços", Icon="pricelist",Content = servicePage },
                        new ShellContent { Title = "Favoritos",Icon="watertap",Content = requestPage },
                        new ShellContent { Title = "Perfil", Icon="account", Content = perfilPage }
                     }
                });
            }
            else
            {
                Items.Add(new TabBar
                {
                    Items =
                    {
                        new ShellContent {Title = "Home", Icon="home",Content = homePage  },
                        new ShellContent { Title = "Preços", Icon="pricelist",Content = servicePage },
                        new ShellContent { Title = "Favoritos",Icon="watertap",Content = requestPage },
                        new ShellContent { Title = "Login", Icon="login", Content = loginPage }
                     }
                });

            }
		
        } 

    }
}
