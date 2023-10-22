using System;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FiltersBookPageVM : BooksPageVM
    {
        public FiltersBookPageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM, mgrVM)
        {
            CanAdd = true;
            TitlePage = "Filtres";
            AddCommand = new Command(async (Id) => {
                Manager.AddBookToFavoriteCommand.Execute(Id);
            });
        }
    }
}

