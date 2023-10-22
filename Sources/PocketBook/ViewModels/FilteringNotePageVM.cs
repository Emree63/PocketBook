using System;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringNotePageVM : FilteringPageVM
    {
        public FilteringNotePageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM, mgrVM)
        {
            ExitText = true;
            TitlePage = "Notes";
            NavigatorToBooksPageCommand = new Command(async (note) =>
            {
                Manager.RefreshPaginationCommand.Execute(null);
                Manager.GetBooksByNoteCommand.Execute(note);
                Navigation.NavigatorToFiltersBooksPageCommand.Execute(null);
            });
        }
    }
}

