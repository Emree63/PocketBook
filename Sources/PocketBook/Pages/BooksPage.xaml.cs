using PocketBook.ViewModels;
using ViewModel;

namespace PocketBook.Pages;

public partial class BooksPage : ContentPage
{
    public BooksPageVM BooksPageVM { get; set; }
    public ScanMenuVM ScanMenuVM => (App.Current as App).ScanMenuVM;

    public BooksPage(BooksPageVM booksPageVM)
	{
        BooksPageVM = booksPageVM;
        InitializeComponent();
        BindingContext = this;
    }
}