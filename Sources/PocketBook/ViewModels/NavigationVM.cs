using System.Windows.Input;
using PocketBook.Pages;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class NavigationVM
	{
		public INavigation navigation = (App.Current as App).MainPage.Navigation;
		public ICommand NavigatorToBooksPageCommand { get; set; }
        public ICommand NavigatorToLoanPageCommand { get; set; }
        public ICommand NavigatorToFilteringAuthorsPageCommand { get; set; }
        public ICommand NavigatorToFilteringDatesPageCommand { get; set; }
        public ICommand NavigatorToBookPageCommand { get; set; }

        public NavigationVM(ManagerVM manager)
		{
			NavigatorToBooksPageCommand = new Command(async () => {
				await navigation.PushAsync(new BooksPage(new BooksPageVM(this, manager)));
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
                await navigation.PushAsync(new BookPage(new BookPageVM(this, manager)));
            });
        }
	}
}

