using PocketBook.ViewModels;

namespace PocketBook.Pages;

public partial class BookPage : ContentPage
{
    public BookPageVM BookPageVM { get; set; }
    public StatusMenuVM StatusMenuVM { get; set; }

    public BookPage(BookPageVM bookPageVM, StatusMenuVM statusMenuVM)
	{
        BookPageVM = bookPageVM;
        StatusMenuVM = statusMenuVM;
        InitializeComponent();
        BindingContext = this;
    }
}