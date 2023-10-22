
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
            NextBooksCommand = new Command(async () => {
                Manager.NextFavoriteBooksCollectionCommand.Execute(null);
            });
            PreviousBooksCommand = new Command(async () => {
                Manager.PreviousFavoriteBooksCollectionCommand.Execute(null);
            });
            ReverseCommand = new Command(() => {
                Manager.ReverseFavoriteBooksCommand.Execute(null);
            });
            DeleteCommand = new Command(async (Id) => {
                Manager.DeleteBookToFavoriteCommand.Execute(Id);
            });
            ChangeCountCommand = new Command(async (Id) => {
                Manager.ChangeCountCommand.Execute(Id);
                Manager.GetFavoriteBooksCommand.Execute(null);
            });
        }
	}
}

