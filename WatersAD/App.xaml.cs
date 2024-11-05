using System.ComponentModel.DataAnnotations;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.ViewModels;
using WatersAD.Views;

namespace WatersAD
{
    public partial class App : Application
    {
        private readonly IDataValidator _dataValidator;
        private readonly AuthService _authService;
        private readonly ApiService _apiService;

        public App(ApiService apiService, IDataValidator dataValidator,AuthService authService)
        {
            InitializeComponent();

            
            _dataValidator = dataValidator;
            _authService = authService;
            _apiService = apiService;

            SetMainPage();
        }

        private void SetMainPage()
        {
            var accessToken = Preferences.Get("accesstoken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new NavigationPage(new LoginPage(new LoginViewModel(_apiService, _dataValidator, _authService)));
                return;
            }

            MainPage = new AppShell(_apiService, _dataValidator, _authService);
        }
    }
}
