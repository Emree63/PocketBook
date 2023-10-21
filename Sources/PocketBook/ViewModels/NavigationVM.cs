using System.Windows.Input;
using PocketBook.Pages;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class NavigationVM
	{
		public INavigation navigation = (App.Current as App).MainPage.Navigation;
		public ICommand NavigatorToAllBooksPageCommand { get; set; }
        public ICommand NavigatorToFavoriteBooksPageCommand { get; set; }
        public ICommand NavigatorToLoanPageCommand { get; set; }
        public ICommand NavigatorToFilteringAuthorsPageCommand { get; set; }
        public ICommand NavigatorToFilteringDatesPageCommand { get; set; }
        public ICommand NavigatorToBookPageCommand { get; set; }

        public NavigationVM(ManagerVM manager)
		{
			NavigatorToAllBooksPageCommand = new Command(async () => {
				await navigation.PushAsync(new BooksPage(new AllBooksPageVM(this, manager)));
			});
            NavigatorToFavoriteBooksPageCommand = new Command(async () => {
                await navigation.PushAsync(new BooksPage(new FavoriteBooksPageVM(this, manager)));
            });
            NavigatorToLoanPageCommand = new Command(async () => {
                await navigation.PushAsync(new LoanPage(new LoanPageVM(this, manager)));
            });
            NavigatorToFilteringAuthorsPageCommand = new Command(async () => {
                await navigation.PushAsync(new FilteringPage(new FilteringAuthorPageVM(this, manager)));
            });
            NavigatorToFilteringDatesPageCommand = new Command(async () => {
                await navigation.PushAsync(new FilteringPage(new FilteringDatePageVM(this, manager)));
            });
            NavigatorToBookPageCommand = new Command(async () => {
                await navigation.PushAsync(new BookPage(new BookPageVM(this, manager), new StatusMenuVM(manager)));
            });
        }
	}
}

