using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatersAD.Services;

namespace WatersAD.ViewModels
{
	public partial class RecoverPasswordViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;
		[ObservableProperty]
		private string email = null!;

		[ObservableProperty]
		private string emailErrorMessage = null!;
		public IAsyncRelayCommand RecoverPasswordCommand { get; }
		public RecoverPasswordViewModel(ApiService apiService, INavigationService navigationService)
        {
			_apiService = apiService;
			_navigationService = navigationService;
			RecoverPasswordCommand = new AsyncRelayCommand(RecoverPassword);
		}

		private async Task RecoverPassword()
		{
			if(string.IsNullOrEmpty(Email))
			{
				EmailErrorMessage = "Tem de preencher o email.";
				return;
			}

			var response = await _apiService.RecoverPasswordAsync(Email);

			if (response.Data)
			{
				await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Verifique seu e-mail para instruções de recuperação de senha.", "OK");
				
			}
			else
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível enviar o e-mail de recuperação de senha. Tente novamente mais tarde.", "OK");
			}

			await _navigationService.SetMainPageAsync<AppShell>();
		}
	}
}
