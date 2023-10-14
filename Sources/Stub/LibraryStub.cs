using Model;
using Stub;

namespace StubLib;

public class LibraryStub : ILibraryManager
{
    private StubbedDTO.Stub StubDTO { get; set; } = new StubbedDTO.Stub();

    public async Task<Author> GetAuthorById(string id)
    {
        return (await StubDTO.GetAuthorById(id)).ToPoco();
    }

    public async Task<Tuple<long, IEnumerable<Author>>> GetAuthorsByName(string substring, int index, int count, string sort = "")
    {
        var result = await StubDTO.GetAuthorsByName(substring, index, count, sort);
        return Tuple.Create(result.Item1, result.Item2.ToPocos());
    }

    public async Task<Book> GetBookById(string id)
    {
        return (await StubDTO.GetBookById(id)).ToPoco();
    }

    public async Task<Book> GetBookByISBN(string isbn)
    {
        return (await StubDTO.GetBookByISBN(isbn)).ToPoco();
    }

    public async Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthor(string author, int index, int count, string sort = "")
    {
        var result = await StubDTO.GetBooksByAuthor(author, index, count, sort);
        return Tuple.Create(result.Item1, result.Item2.ToPocos());
    }

    public async Task<Tuple<long, IEnumerable<Book>>> GetBooksByAuthorId(string authorId, int index, int count, string sort = "")
    {
        var result = await StubDTO.GetBooksByAuthor(authorId, index, count, sort);
        return Tuple.Create(result.Item1, result.Item2.ToPocos());
    }

    public async Task<Tuple<long, IEnumerable<Book>>> GetBooksByTitle(string title, int index, int count, string sort = "")
    {
        var result = await StubDTO.GetBooksByTitle(title, index, count, sort);
        return Tuple.Create(result.Item1, result.Item2.ToPocos());
    }
}

