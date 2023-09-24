using System.Windows.Input;
using PocketBook.ViewModels;

namespace PocketBook;

public partial class MainPage : ContentPage
{
	public NavigationVM Navigator {get; set;} = new NavigationVM();
	
	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

}