using PocketBook.ViewModels;
using ViewModel;

namespace PocketBook.Pages;

public partial class BooksPage : ContentPage
{
    public BooksPage()
	{
        InitializeComponent();
        BindingContext = this;
    }
}