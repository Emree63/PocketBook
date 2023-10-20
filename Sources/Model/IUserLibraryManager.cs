using System;
namespace Model
{
	public interface IUserLibraryManager : ILibraryManager
	{
        Task<Tuple<long, IEnumerable<Book>>> GetBooksFromCollection(int index, int count, string sort = "");
        Task<Tuple<long, IEnumerable<Author>>> GetAuthorsFromCollection(int index, int count, string sort = "");

        Task<Book> AddBook(Book book);
		Task<Book> AddBook(string id);
		Task<Book> AddBookByIsbn(string isbn);
		Task<bool> RemoveBook(Book book);
		Task<bool> RemoveBook(string id);
		Task<bool> RemoveBookByIsbn(string isbn);

		Task<bool> AddToFavorites(Book book);
		Task<bool> AddToFavorites(string bookId);
		Task<bool> RemoveFromFavorites(Book book);
		Task<bool> RemoveFromFavorites(string bookId);
        Task<Tuple<long, IEnumerable<Book>>> GetFavoritesBooks(int index, int count);


		Task<Book> UpdateBook(Book updatedBook);

		Task<Contact> AddContact(Contact contact);
		Task<bool> RemoveContact(Contact contact);

		Task<bool> LendBook(Book book, Contact contact, DateTime? loanDate);
		Task<bool> GetBackBook(Book book, DateTime? returnedDate);
		Task<bool> BorrowBook(Book book, Contact owner, DateTime? borrowedDate);
		Task<bool> GiveBackBook(Book book, DateTime? returnedDate);

        Task<Tuple<long, IEnumerable<Loan>>> GetCurrentLoans(int index, int count);
        Task<Tuple<long, IEnumerable<Loan>>> GetPastLoans(int index, int count);

		Task<Tuple<long, IEnumerable<Borrowing>>> GetCurrentBorrowings(int index, int count);
        Task<Tuple<long, IEnumerable<Borrowing>>> GetPastBorrowings(int index, int count);

		Task<Tuple<long, IEnumerable<Contact>>> GetContacts(int index, int count);

	}
}

