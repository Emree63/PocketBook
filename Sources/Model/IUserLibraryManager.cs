using System;
namespace Model
{
	public interface IUserLibraryManager : ILibraryManager
	{
        Task<Tuple<long, IEnumerable<Book>>> GetAllBooks(int index, int count, string sort = "");
	}
}

