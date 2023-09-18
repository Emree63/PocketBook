using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Model
{
	public class Manager
	{
		private ILibraryManager LibraryManager { get; set; }

		public ReadOnlyCollection<Book> Books { get; private set; }
		private List<Book> books = new();
 
		public Manager(ILibraryManager libMgr)
		{
			LibraryManager = libMgr;
			Books = new ReadOnlyCollection<Book>(books);
		}

		public async Task<Book> GetBookById(string id)
			=> await LibraryManager.GetBookById(id);

        public async Task<Book> GetBookByISBN(string isbn)
			=> await LibraryManager.GetBookByISBN(isbn);

        public async Task<(long count, IEnumerable<Book> books)> GetBooksByTitle(string title, int index, int count, string sort = "")
		{
			var result = await LibraryManager.GetBooksByTitle(title, index, count, sort);
			return (result.Item1, result.Item2);
		}

        public async Task<(long count, IEnumerable<Book> books)> GetBooksByAuthorId(string authorId, int index, int count, string sort = "")
		{
			var result = await LibraryManager.GetBooksByAuthorId(authorId, index, count, sort);
			return (result.Item1, result.Item2);
		}

        public async Task<(long count, IEnumerable<Book> books)> GetBooksByAuthor(string author, int index, int count, string sort = "")
		{
			var result = await LibraryManager.GetBooksByAuthor(author, index, count, sort);
			return (result.Item1, result.Item2);
		}

        public async Task<Author> GetAuthorById(string id)
			=> await LibraryManager.GetAuthorById(id);

        public async Task<(long count, IEnumerable<Author> authors)> GetAuthorsByName(string substring, int index, int count, string sort = "")
		{
			var result = await LibraryManager.GetAuthorsByName(substring, index, count, sort);
			return (result.Item1, result.Item2);
		}
	}
}

