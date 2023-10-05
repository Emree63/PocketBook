using ViewModel;

namespace PocketBook.ViewModels
{
	public class BooksPageVM
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public BooksPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
        }
    }
}

