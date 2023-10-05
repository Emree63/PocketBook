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
        public ICommand NavigatorToFilteringPageCommand { get; set; }
        public ICommand NavigatorToBookPageCommand { get; set; }

        public NavigationVM(ManagerVM manager)
		{
			NavigatorToBooksPageCommand = new Command(async () => {
				await navigation.PushAsync(new BooksPage(new BooksPageVM(this, manager)));
			});
            NavigatorToLoanPageCommand = new Command(async () => {
                await navigation.PushAsync(new LoanPage());
            });
            NavigatorToFilteringPageCommand = new Command(async () => {
                await navigation.PushAsync(new FilteringPage());
            });
            NavigatorToBookPageCommand = new Command(async () => {
                await navigation.PushAsync(new BookPage());
            });
        }
	}
}

