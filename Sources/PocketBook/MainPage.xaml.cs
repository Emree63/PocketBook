using System.Windows.Input;
using PocketBook.ViewModels;
namespace PocketBook;

public partial class MainPage : ContentPage
{
    public MainPageVM MainPageVM { get; set; }

	public MainPage(MainPageVM mainPageVM)
	{
        MainPageVM = mainPageVM;
		InitializeComponent();
		BindingContext = this;
	}

}