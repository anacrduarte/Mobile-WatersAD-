using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WatersAD.Config;
using WatersAD.Models;

namespace WatersAD.Services
{
	public class ApiService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<ApiService> _logger;
		JsonSerializerOptions _serializerOptions;

		public ApiService(HttpClient httpClient,
						  ILogger<ApiService> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
			_serializerOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
		}
		public async Task<ApiResponse<bool>> Login(Login login)
		{
			try
			{


				var json = JsonSerializer.Serialize(login, _serializerOptions);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await PostRequest("api/Users/login", content);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError($"Erro ao enviar requisição HTTP : {response.StatusCode}");
					return new ApiResponse<bool>
					{
						ErrorMessage = $"Erro ao enviar requisição HTTP : {response.StatusCode}"
					};
				}

				var jsonResult = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<UserToken>(jsonResult, _serializerOptions);

				Preferences.Set("accesstoken", result!.AccessToken);
				Preferences.Set("userid", result.UserId!);
				Preferences.Set("username", result.UserName);
				Preferences.Set("firstname", result.FirstName);
				Preferences.Set("lastname", result.LastName);
				Preferences.Set("imageurl",result.ImageUrl);


				return new ApiResponse<bool> { Data = true };
			}
			catch (Exception ex)
			{
				_logger.LogError($"Erro no login : {ex.Message}");
				return new ApiResponse<bool> { ErrorMessage = ex.Message };
			}
		}

		public async Task<(UserDetails? UserDetails, string? ErrorMessage)> GetUserDetails()
		{
			string endpoint = "api/Users/userdetails";
			return await GetAsync<UserDetails>(endpoint);
		}

		private async Task<HttpResponseMessage> PutRequest(string uri, HttpContent content)
		{
			var enderecoUrl = AppConfig.BaseUrl + uri;
			try
			{
				AddAuthorizationHeader();
				var result = await _httpClient.PutAsync(enderecoUrl, content);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Erro ao enviar requisição PUT para {uri}: {ex.Message}");
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}
		}

		private void AddAuthorizationHeader()
		{
			var token = Preferences.Get("accesstoken", string.Empty);
			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

		private async Task<HttpResponseMessage> PostRequest(string uri, HttpContent content)
		{
			var enderecoUrl = AppConfig.BaseUrl + uri;
			try
			{
				var result = await _httpClient.PostAsync(enderecoUrl, content);
				return result;
			}
			catch (Exception ex)
			{
				
				_logger.LogError($"Erro ao enviar requisição POST para {uri}: {ex.Message}");
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}
		}

		private async Task<(T? Data, string? ErrorMessage)> GetAsync<T>(string endpoint)
		{
			try
			{
				AddAuthorizationHeader();

				var response = await _httpClient.GetAsync(AppConfig.BaseUrl + endpoint);

				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();
					var data = JsonSerializer.Deserialize<T>(responseString, _serializerOptions);
					return (data ?? Activator.CreateInstance<T>(), null);
				}
				else
				{
					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
					{
						string errorMessage = "Unauthorized";
						_logger.LogWarning(errorMessage);
						return (default, errorMessage);
					}

					string generalErrorMessage = $"Erro na requisição: {response.ReasonPhrase}";
					_logger.LogError(generalErrorMessage);
					return (default, generalErrorMessage);
				}
			}
			catch (HttpRequestException ex)
			{
				string errorMessage = $"Erro de requisição HTTP: {ex.Message}";
				_logger.LogError(ex, errorMessage);
				return (default, errorMessage);
			}
			catch (JsonException ex)
			{
				string errorMessage = $"Erro de desserialização JSON: {ex.Message}";
				_logger.LogError(ex, errorMessage);
				return (default, errorMessage);
			}
			catch (Exception ex)
			{
				string errorMessage = $"Erro inesperado: {ex.Message}";
				_logger.LogError(ex, errorMessage);
				return (default, errorMessage);
			}
		}

		public async Task<(bool Success, string? ErrorMessage)> UpdateUserDataAsync(MultipartFormDataContent content)
		{
			try
			{
				
				//var jsonContent = JsonSerializer.Serialize(userData);
				//var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

				
				var response = await PutRequestImageAsync("api/Users/changeuser", content);

				if (response.IsSuccessStatusCode)
				{
					return (true, null);
				}
				else
				{
					
					if (response.StatusCode == HttpStatusCode.Unauthorized)
					{
						const string errorMessage = "Unauthorized";
						_logger.LogWarning(errorMessage);
						return (false, errorMessage);
					}

				
					string generalErrorMessage = $"Erro na requisição: {response.ReasonPhrase}";
					_logger.LogError(generalErrorMessage);
					return (false, generalErrorMessage);
				}
			}
			catch (HttpRequestException ex)
			{
				
				string errorMessage = $"Erro de requisição HTTP: {ex.Message}";
				_logger.LogError(ex, errorMessage);
				return (false, errorMessage);
			}
			catch (Exception ex)
			{
				
				string errorMessage = $"Erro inesperado: {ex.Message}";
				_logger.LogError(ex, errorMessage);
				return (false, errorMessage);
			}
		}

        public async Task<(List<Tiers>? Tiers, string? ErrorMessage)> GetTiers()
        {
            return await GetAsync<List<Tiers>>("api/TierPrice/GetAllTiers");
        }


        public async Task<HttpResponseMessage> PutRequestImageAsync(string uri, MultipartFormDataContent content)
        {
            var urlAddress = AppConfig.BaseUrl + uri;
            try
            {
                AddAuthorizationHeader();

                // Envia a requisição PUT com o conteúdo multipart
                var result = await _httpClient.PutAsync(urlAddress, content);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending PUT request to {uri}: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<bool> LogoutAsync()
        {
            var response = await PostRequest("api/Users/logout", null);

            if (response.IsSuccessStatusCode)
            {
             
                return true;
            }
            else
            {
              
                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Logout falhou: {errorMessage}");
                return false;
            }
        }

    }
}
