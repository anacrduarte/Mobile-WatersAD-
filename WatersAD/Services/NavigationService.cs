using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatersAD.Views;

namespace WatersAD.Services
{
	public class NavigationService :INavigationService
	{
		private readonly IServiceProvider _serviceProvider;

		public NavigationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task NavigateToAsync<TPage>() where TPage : Page
		{
			var page = _serviceProvider.GetRequiredService<TPage>();
			await Application.Current!.MainPage!.Navigation.PushAsync(page);
		}

		public Task SetMainPageAsync<TPage>() where TPage : Page
		{
			var page = _serviceProvider.GetRequiredService<TPage>();
			Application.Current!.MainPage = new NavigationPage(page); 
			return Task.CompletedTask;
		}

		public async Task NavigateToAsync<TPage>(Dictionary<string, object> parameters = null) where TPage : Page
		{
			
			var page = _serviceProvider.GetRequiredService<TPage>();

		
			if (parameters != null)
			{
				
				page.BindingContext = parameters;
			}

		
			await Application.Current!.MainPage!.Navigation.PushAsync(page);
		}





	}
}
