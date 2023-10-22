using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringAuthorPageVM : FilteringPageVM
	{

        public FilteringAuthorPageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM,mgrVM)
        {
            ExitText = true;
            TitlePage = "Auteur";
            NavigatorToBooksPageCommand = new Command(async (author) => {
                Manager.RefreshPaginationCommand.Execute(null);
                Manager.GetBooksByAuthorCommand.Execute(author);
                Navigation.NavigatorToFiltersBooksPageCommand.Execute(null);
            });
        }
	}
}

