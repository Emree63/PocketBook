using PocketBook.ViewModels;

namespace PocketBook.Pages;

public partial class BookPage : ContentPage
{
    public BookPageVM BookPageVM { get; set; }

    public BookPage(BookPageVM bookPageVM)
	{
        BookPageVM = bookPageVM;
        InitializeComponent();
        BindingContext = this;
    }
}