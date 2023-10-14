using PocketBook.ViewModels;

namespace PocketBook;

public partial class App : Application
{
    public ScanMenuVM ScanMenuVM { get; set; }
    public App(ScanMenuVM scanMenuVM)
	{
        ScanMenuVM = scanMenuVM;
		InitializeComponent();

		MainPage = new AppShell();
	}
}
