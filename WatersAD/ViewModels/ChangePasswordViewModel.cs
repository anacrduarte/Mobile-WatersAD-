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
				ErrorMessage = "Email não encontrado.";
				return;
			}
			if(string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(Confirm))
			{
				ErrorMessage = "Tem de preencher os dados.";
				return;
			}

			var response =await _apiService.ChangePassword(email, OldPassword, NewPassword, Confirm);

			if (response.Data)
			{
				await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Alteração de palavra-passe efectuda com sucesso.", "OK");

			}
			else
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível alterar a palavra passe. Tente novamente mais tarde.", "OK");
			}

			await _navigationService.NavigateToAsync<ProfileSettingsPage>();
		}
	}
}
