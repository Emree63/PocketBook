using PocketBook.ViewModels;

namespace PocketBook.Pages;

public partial class LoanPage : ContentPage
{
    public LoanPageVM LoanPageVM { get; set; }

    public LoanPage(LoanPageVM loanPageVM)
	{
        LoanPageVM = loanPageVM;
		InitializeComponent();
        BindingContext = this;
    }
}