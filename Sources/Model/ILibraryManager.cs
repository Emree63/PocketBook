using System;
namespace Model
{
	public interface ILibraryManager
	{
        Task<Book> GetBookById(string id);
        Task<Book> GetBookByISBN(string isbn);
        Task<Tuple<long, IEnumerable<Book>>> GetBooksByTitle(string title, int index, int count, string sort = "");
        Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthorId(string authorId, int index, int count, string sort = "");
        Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthor(string author, int index, int count, string sort = "");
        Task<Tuple<long, IEnumerable<Book>>> GetBooksByDate(string date, int index, int count, string sort = "");
        Task<Author> GetAuthorById(string id);
        Task<Tuple<long, IEnumerable<Author>>> GetAuthorsByName(string substring, int index, int count, string sort = "");
	}
}

