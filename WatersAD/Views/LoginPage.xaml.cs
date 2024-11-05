using WatersAD.Services;
using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class LoginPage : ContentPage
{
    

    public LoginPage(LoginViewModel model)
	{
		InitializeComponent();
         BindingContext = model;
    }
}