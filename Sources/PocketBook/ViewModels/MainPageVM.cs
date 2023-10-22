using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class MainPageVM: BaseViewModel
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public ICommand NavigatorToBooksPageCommand { get; set; }
        public ICommand NavigatorToAuthorsPageCommand { get; set; }
        public ICommand NavigatorToDatesPageCommand { get; set; }
        public ICommand NavigatorToNotesPageCommand { get; set; }
        public ICommand NavigatorToFavoritePageCommand { get; set; }
        public ICommand NavigatorToLoanPageCommand { get; set; }

        public MainPageVM(NavigationVM navVM, ManagerVM mgrVM)
		{
            Navigation = navVM;
            Manager = mgrVM;
            NavigatorToBooksPageCommand = new Command(async () => {
                Manager.RefreshPaginationCommand.Execute(null);
                Manager.GetBooksFromCollectionCommand.Execute("author");
                Navigation.NavigatorToAllBooksPageCommand.Execute(null);
            });
            NavigatorToAuthorsPageCommand = new Command(async () => {
                Manager.RefreshSearchCommand.Execute(null);
                Manager.GetAuthorsFromCollectionCommand.Execute(null);
                Navigation.NavigatorToFilteringAuthorsPageCommand.Execute(null);
            });
            NavigatorToDatesPageCommand = new Command(async () => {
                Manager.RefreshSearchCommand.Execute(null);
                Manager.GetDatesFromCollectionCommand.Execute(null);
                Navigation.NavigatorToFilteringDatesPageCommand.Execute(null);
            });
            NavigatorToNotesPageCommand = new Command(async () => {
                Manager.RefreshSearchCommand.Execute(null);
                Manager.GetNotesFromCollectionCommand.Execute(null);
                Navigation.NavigatorToFilteringNotesPageCommand.Execute(null);
            });
            NavigatorToFavoritePageCommand = new Command(async () => {
                Manager.RefreshPaginationCommand.Execute(null);
                Manager.GetFavoriteBooksCommand.Execute(null);
                Navigation.NavigatorToFavoriteBooksPageCommand.Execute(null);
            });
            NavigatorToLoanPageCommand = new Command(async () => {
                Manager.GetLoansBookCommand.Execute(null);
                Manager.GetBorrowingsBookCommand.Execute(null);
                Navigation.NavigatorToLoanPageCommand.Execute(null);
            });
        }
	}
}

