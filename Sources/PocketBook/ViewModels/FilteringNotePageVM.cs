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
                Manager.GetBooksByNoteCommand.Execute(note);
                Navigation.NavigatorToAllBooksPageCommand.Execute(null);
            });
        }
    }
}

