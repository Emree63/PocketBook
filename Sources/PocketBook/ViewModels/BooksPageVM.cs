using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class BooksPageVM
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public ICommand NavigatorToBookPageCommand { get; set; }

        public BooksPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
            NavigatorToBookPageCommand = new Command(async (Id) => {
                Manager.GetBookByIdCommand.Execute(Id);
                Navigation.NavigatorToBookPageCommand.Execute(null);
            });
        }
    }
}

