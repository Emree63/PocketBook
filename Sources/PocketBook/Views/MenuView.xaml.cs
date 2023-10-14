using PocketBook.ViewModels;

namespace PocketBook.Views;

public partial class MenuView : ContentView
{
    public ScanMenuVM ScanMenuVM => (App.Current as App).ScanMenuVM;

    public MenuView()
	{
		InitializeComponent();
	}
}
