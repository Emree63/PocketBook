using Model;
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
        PickerCount.SelectedIndexChanged += PickerSelectedIndexChanged;
    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedValue = (int)PickerCount.SelectedItem;

        BooksPageVM.ChangeCountCommand.Execute(selectedValue);
    }
}