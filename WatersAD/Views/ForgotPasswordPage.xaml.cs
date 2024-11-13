using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(RecoverPasswordViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}