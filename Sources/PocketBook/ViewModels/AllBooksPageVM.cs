using System;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class AllBooksPageVM : BooksPageVM
	{
		public AllBooksPageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM, mgrVM)

        {
            CanAdd = true;
            TitlePage = "Tous";
            NextBooksCommand = new Command(async () => {
                Manager.NextBooksCommand.Execute(null);
            });
            PreviousBooksCommand = new Command(async () => {
                Manager.PreviousBooksCommand.Execute(null);
            });
            ReverseCommand = new Command(() => {
                Manager.ReverseBooksCommand.Execute(null);
            });
            AddCommand = new Command(async (Id) => {
                Manager.AddBookToFavoriteCommand.Execute(Id);
            });
            DeleteCommand = new Command(async (Id) => {
                Manager.DeleteBookToCollectionCommand.Execute(Id);
            });
            ChangeCountCommand = new Command(async (Id) => {
                Manager.ChangeCountCommand.Execute(Id);
                Manager.GetBooksFromCollectionCommand.Execute(null);
            });
        }
	}
}

