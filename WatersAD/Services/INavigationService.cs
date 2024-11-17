using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.Services
{
	public interface INavigationService
	{
		Task NavigateToAsync<TPage>() where TPage : Page;

		Task SetMainPageAsync<TPage>() where TPage : Page;

		Task NavigateToAsync<TPage>(Dictionary<string, object> parameters = null) where TPage : Page;

	
	}
}
