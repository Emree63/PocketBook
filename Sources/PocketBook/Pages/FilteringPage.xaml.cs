using PocketBook.ViewModels;

namespace PocketBook.Pages;

public partial class FilteringPage : ContentPage
{
    public FilteringPageVM FilteringPageVM { get; set; }

    public FilteringPage(FilteringPageVM filteringPageVM)
	{
		FilteringPageVM = filteringPageVM;
		InitializeComponent();
        BindingContext = this;
    }

    void SearchBar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }
}