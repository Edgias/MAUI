using Rest.ViewModels;

namespace Rest.Pages;

public partial class PartsPage : ContentPage
{
	public PartsPage()
	{
		InitializeComponent();
        BindingContext = new PartsViewModel();
    }
}