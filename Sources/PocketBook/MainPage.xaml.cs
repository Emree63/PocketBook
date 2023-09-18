using System.Windows.Input;

namespace PocketBook;

public partial class MainPage : ContentPage
{
	// public NavigationManagerVM Navigator {get; set;} = new NavigatorManagerVM();
	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	// Command="{Binding Navigator.NavigatorToBooksPagCommand}"

}

