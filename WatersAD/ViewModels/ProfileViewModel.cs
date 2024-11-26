using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Headers;
using WatersAD.Config;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
	public partial class ProfileViewModel : ObservableObject
	{
		private readonly AuthService _authService;
		private readonly IDataValidator _dataValidator;
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;

		[ObservableProperty]
		private string firstName = null!;

		[ObservableProperty]
		private string lastName = null!;

		[ObservableProperty]
		private string imageUrl = null!;

		[ObservableProperty]
		private string? errorMessage;

        private string userName = null!;

        [ObservableProperty]
        private string? imagePath;

     


        private byte[]? _selectedImageBytes;

		public IAsyncRelayCommand ChangeDataInCommand { get; }

		public IAsyncRelayCommand ChangeImageInCommand { get; }

		public ProfileViewModel(AuthService authService, IDataValidator dataValidator, ApiService apiService, INavigationService navigationService)
		{

			_authService = authService;
			_dataValidator = dataValidator;
			_apiService = apiService;
			_navigationService = navigationService;
			ChangeImageInCommand = new AsyncRelayCommand(async () => await SelectImage());
            ChangeDataInCommand = new AsyncRelayCommand(async () => await ChangeData());

        }


		public void Initialize()
		{

			 FirstName =  Preferences.Get("firstname", string.Empty);
			LastName = Preferences.Get("lastname", string.Empty);
			ImageUrl = Preferences.Get("imageurl", string.Empty);
           
     

            if( !string.IsNullOrEmpty( ImageUrl ) && !ImageUrl.Contains("noimage.png"))
            {
                ImagePath = $"{AppConfig.BaseUrl.TrimEnd('/')}/{ImageUrl.TrimStart('~', '/')}";
            }
            else
            {
                ImagePath = AppConfig.ProfileDefaultImage;
            }

        }
       
   

        private async Task SelectImage()
        {
         
            _selectedImageBytes = await SelectImageAsync();
            if (_selectedImageBytes == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", "Nenhuma imagem selecionada.", "OK");
            }
        }

        private async Task ChangeData()
        {
            //if (_selectedImageBytes == null)
            //{
            //    await Application.Current!.MainPage!.DisplayAlert("Erro", "Tem que ter imagem.", "OK");
            //    return;
            //}


            await UploadUserDataWithImageAsync(FirstName, LastName, _selectedImageBytes);
        }

        public async Task UploadUserDataWithImageAsync(string firstName, string lastName, byte[] imageBytes)
        {
            try
            {
              
                string fileName = Path.GetFileName(ImageUrl);
                await UploadUserDataAsync(firstName, lastName, imageBytes, fileName);
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"{ex.Message}.", "OK");
            }
        }

        private async Task<byte[]?> SelectImageAsync()
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync();

                if (file == null) return null;

                ImagePath = file.FullPath;

                using (var stream = await file.OpenReadAsync())
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray(); 
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"{ex.Message}.", "OK");
            }
            return null;
        }

        public async Task UploadUserDataAsync(string firstName, string lastName, byte[]? imageBytes, string fileName)
        {
            using (var client = new HttpClient())
            {
                userName = Preferences.Get("username", string.Empty);

                var content = new MultipartFormDataContent();

 
                content.Add(new StringContent(firstName), "FirstName");
                content.Add(new StringContent(lastName), "LastName");
                content.Add(new StringContent(userName), "UserName");


                if (imageBytes != null)
                {
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    content.Add(imageContent, "ImageFile", fileName);
                }

                try
                {
                    var response = await _apiService.UpdateUserDataAsync(content);


                    if (response.Success)
                    {
                    
                        await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Dados atualizados com sucesso!", "OK");
                    }
                    else
                    {
                        await Application.Current!.MainPage!.DisplayAlert("Erro", "Falha ao atualizar dados!", "OK");
                    }
                }
                catch (Exception ex)
                {
                    
                    await Application.Current!.MainPage!.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                }
            }
        }



        private async void NavigateToLogin()
        {
			await _navigationService.NavigateToAsync<LoginPage>();
		}
    }
}
