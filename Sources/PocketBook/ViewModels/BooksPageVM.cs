using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class BooksPageVM
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public virtual bool CanAdd { get; set; }
        public string TitlePage { get; set; }
        public ICommand NavigatorToBookPageCommand { get; set; }
        public virtual ICommand ChangeCountCommand { get; set; }
        public virtual ICommand NextBooksCommand { get; set; }
        public virtual ICommand PreviousBooksCommand { get; set; }
        public virtual ICommand ReverseCommand { get; set; }
        public virtual ICommand AddCommand { get; set; }
        public virtual ICommand DeleteCommand { get; set; }

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

