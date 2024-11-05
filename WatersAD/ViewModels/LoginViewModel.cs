using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Validator;

namespace WatersAD.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly IDataValidator _validator;
        private readonly AuthService _authService;
        [ObservableProperty]
        private string email = null!;

        [ObservableProperty]
        private string password = null!;

        [ObservableProperty]
        private string emailErrorMessage = null!;

        [ObservableProperty]
        private string passwordErrorMessage = null!;


        public IAsyncRelayCommand SignInCommand { get; }

      
        public LoginViewModel(ApiService apiService, IDataValidator dataValidator, AuthService authService)
        {
            _apiService = apiService;
            _validator = dataValidator;
            _authService = authService;
            SignInCommand = new AsyncRelayCommand(OnSignIn);
        }


        private async Task OnSignIn()
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

                Application.Current!.MainPage = new AppShell(_apiService, _validator, _authService);
                EmailErrorMessage = string.Empty;
                PasswordErrorMessage = string.Empty;
            }
            else
            {

                PasswordErrorMessage = response.ErrorMessage;
            }
        }
    }
}
