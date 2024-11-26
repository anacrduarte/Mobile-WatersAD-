using WatersAD.Services;
using WatersAD.Validator;

namespace WatersAD
{
    public partial class App : Application
    {
        private readonly IDataValidator _dataValidator;
        private readonly AuthService _authService;
        private readonly INavigationService _navigationService;
        private readonly ApiService _apiService;

        public App(ApiService apiService, IDataValidator dataValidator, AuthService authService, INavigationService navigationService)
        {
            InitializeComponent();


            _dataValidator = dataValidator;
            _authService = authService;
            _navigationService = navigationService;
            _apiService = apiService;

            SetMainPage();
        }

        private void SetMainPage()
        {


            MainPage = new AppShell(_apiService, _dataValidator, _authService, _navigationService);
        }
    }
}
