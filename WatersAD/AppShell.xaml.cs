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

        public AppShell(ApiService apiService, IDataValidator validator, AuthService authService)
        {
            InitializeComponent();
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            _authService = authService;
            ConfigureShell();
        }

        private void ConfigureShell()
        {
            var homePage = new HomePage();
            var servicePage = new ServicePriceListPage(new TiersViewModel(_apiService));
            var requestPage = new RequestWaterMeterPage();
            var perfilPage = new ProfilePage(new ProfileViewModel(_authService, _validator, _apiService));
            var loginPage = new LoginPage(new LoginViewModel(_apiService, _validator, _authService));

            bool isUserLoggedIn = _authService.IsLoggedIn();
			

			Items.Add(new TabBar
            {
                Items =
            {
                new ShellContent {Title = "Home", Icon="home",Content = homePage  },
                new ShellContent { Title = "Preços", Icon="pricelist",Content = servicePage },
                new ShellContent { Title = "Favoritos",Icon="watertap",Content = requestPage },
			    new ShellContent { Title = isUserLoggedIn ? "Perfil" : "Login", Icon="account", Content = perfilPage }
			}
            });
        } 

    }
}
