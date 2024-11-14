using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage(ChangePasswordViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}