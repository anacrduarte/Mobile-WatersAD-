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

		public ProfileViewModel(AuthService authService, IDataValidator dataValidator, ApiService apiService)
		{

			_authService = authService;
			_dataValidator = dataValidator;
			_apiService = apiService;



			
			ChangeImageInCommand = new AsyncRelayCommand(SelectImage);
            ChangeDataInCommand = new AsyncRelayCommand(ChangeData);

        }
		public void Initialize()
		{

			 FirstName =  Preferences.Get("firstname", string.Empty);
			LastName = Preferences.Get("lastname", string.Empty);
			ImageUrl = Preferences.Get("imageurl", string.Empty);
           
     

            if( !string.IsNullOrEmpty( ImageUrl ))
            {
                ImagePath = $"{AppConfig.BaseUrl.TrimEnd('/')}/{ImageUrl.TrimStart('~', '/')}";
            }
            else
            {
                ImagePath = "Resources/Images/aaaa.jpg";
            }

        }
       
   

        private async Task SelectImage()
        {
         
            _selectedImageBytes = await SelectImageAsync();
            if (_selectedImageBytes == null)
            {
                ErrorMessage = "No image selected";
            }
        }

        private async Task ChangeData()
        {
            if (_selectedImageBytes == null)
            {
                ErrorMessage = "Please select an image first.";
                return;
            }

   
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
                ErrorMessage = $"An error occurred: {ex.Message}";
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
                ErrorMessage = $"Error selecting image: {ex.Message}";
            }
            return null;
        }

        public async Task UploadUserDataAsync(string firstName, string lastName, byte[] imageBytes, string fileName)
        {
            using (var client = new HttpClient())
            {
                userName = Preferences.Get("username", string.Empty);
                
                var content = new MultipartFormDataContent();

               
                content.Add(new StringContent(firstName), "FirstName");
                content.Add(new StringContent(lastName), "LastName");
                content.Add(new StringContent(userName), "UserName");
              
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                content.Add(imageContent, "ImageFile", fileName);

                var response = await _apiService.UpdateUserDataAsync(content);
                if (response.Success)
                {
                   ErrorMessage = "Data uploaded successfully!";
                   
                }
                else
                {
                    ErrorMessage = "Failed to upload data";
                }
            }
        }
     

        private void NavigateToLogin()
		{
			Application.Current!.MainPage = new NavigationPage(new LoginPage(new LoginViewModel(_apiService, _dataValidator, _authService)));
		}
	}
}
