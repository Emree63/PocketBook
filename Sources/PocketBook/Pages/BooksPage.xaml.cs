using PocketBook.ViewModels;
using ViewModel;

namespace PocketBook.Pages;

public partial class BooksPage : ContentPage
{
    public BooksPageVM BooksPageVM { get; set; }

    public BooksPage(BooksPageVM booksPageVM)
	{
        BooksPageVM = booksPageVM;
        InitializeComponent();
        BindingContext = this;
    }
}