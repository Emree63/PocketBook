
using System;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FavoriteBooksPageVM : BooksPageVM
	{
		public FavoriteBooksPageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM, mgrVM)
        {
			CanAdd = false;
			TitlePage = "Favoris";
            DeleteCommand = new Command(async (Id) => {
                Manager.DeleteBookToFavoriteCommand.Execute(Id);
            });
        }
	}
}

