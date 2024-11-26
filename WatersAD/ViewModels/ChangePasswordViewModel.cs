using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatersAD.Services;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
	public partial class ChangePasswordViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;
		[ObservableProperty]
		private string oldPassword = null!;

		[ObservableProperty]
		private string newPassword = null!;

		[ObservableProperty]
		private string confirm = null!;

		[ObservableProperty]
		private string errorMessage = null!;
		public IAsyncRelayCommand ChangePasswordCommand { get; }

		public ChangePasswordViewModel(ApiService apiService, INavigationService navigationService)
        {
			_apiService = apiService;
			_navigationService = navigationService;
			ChangePasswordCommand = new AsyncRelayCommand(ChangePassword);
		}

		private async Task ChangePassword()
		{
			var email = Preferences.Get("username", string.Empty);
			if (string.IsNullOrEmpty(email))
			{
                await Application.Current!.MainPage!.DisplayAlert("Erro", "Email não encontrado!", "OK");
                return;
			}
			if(string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(Confirm))
			{
                await Application.Current!.MainPage!.DisplayAlert("Erro", "Preencha todos os dados, por favor.", "OK");
                return;
			}
            var errors = ValidatePassword(NewPassword , Confirm);
            if(errors.Any())
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"{errors}", "OK");
                return;
            }

			var response =await _apiService.ChangePassword(email, OldPassword, NewPassword, Confirm);

			if (response.Data)
			{
				await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Alteração de palavra-passe efectuda com sucesso.", "OK");
                await _navigationService.NavigateToAsync<ProfileSettingsPage>();

            }
			else
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível alterar a palavra passe. Tente novamente mais tarde.", "OK");
                return;
			}

			
		}


        public string ValidatePassword(string newPassword, string confirmPassword)
        {
            var errors = new List<string>();

            if (newPassword != confirmPassword)
            {
                errors.Add("As senhas não coincidem.");
            }

   
            if (newPassword.Length < 8)
            {
                errors.Add("A senha deve ter no mínimo 8 caracteres.");
            }

            if (!newPassword.Any(char.IsUpper))
            {
                errors.Add("A senha deve conter pelo menos uma letra maiúscula.");
            }

   
            if (!newPassword.Any(char.IsLower))
            {
                errors.Add("A senha deve conter pelo menos uma letra minúscula.");
            }

            if (!newPassword.Any(char.IsDigit))
            {
                errors.Add("A senha deve conter pelo menos um dígito.");
            }


            if (!newPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                errors.Add("A senha deve conter pelo menos um caractere especial.");
            }

            if (errors.Any())
            {
                return string.Join("\n", errors);
            }
            return string.Empty;
        }

    }
}
