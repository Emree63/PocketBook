using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using Model;
using MyMVVM_ToolKit;

namespace ViewModel;

public class ManagerVM : BaseViewModel<Manager>
{
    public ICommand GetBooksFromCollectionCommand { get; private set; }
    public ICommand GetAuthorsFromCollectionCommand { get; private set; }
    public ICommand GetBookByIdCommand { get; private set; }

    public ReadOnlyObservableCollection<BookVM> Books { get; private set; }
    private readonly ObservableCollection<BookVM> books = new ObservableCollection<BookVM>();

    public ReadOnlyObservableCollection<Tuple<String, List<BookVM>>> Filters { get; private set; }
    private readonly ObservableCollection<Tuple<String, List<BookVM>>> filters = new ObservableCollection<Tuple<String, List<BookVM>>>();

    public BookVM? Book {
        get => book;
        set
        {
            SetProperty(ref book, value);
        }
    }
    public BookVM? book = null;

    public ManagerVM(Manager model) : base(model)
    {
        Books = new ReadOnlyObservableCollection<BookVM>(books);
        Filters = new ReadOnlyObservableCollection<Tuple<String, List<BookVM>>>(filters);
        GetBooksFromCollectionCommand = new RelayCommandAsync(GetBooksFromCollection);
        GetAuthorsFromCollectionCommand = new RelayCommandAsync(GetAuthorsFromCollection);
        GetBookByIdCommand = new RelayCommandAsync<string>(GetBookById);
        GetBooksFromCollectionCommand.Execute(null);
    }

    private async Task GetBooksFromCollection()
    {
        var result = await Model.GetBooksFromCollection(Index, Count);
        IEnumerable<Book> resBooks = result.books;
        nbBooks = result.count;
        books.Clear();
        foreach (var B in resBooks)
        {
            books.Add(new BookVM(B));
        }
    }

    private async Task GetAuthorsFromCollection()
    {
        var result = await Model.GetAuthorsFromCollection(Index, Count);
        IEnumerable<Author> resAuthors = result.authors;
        nbBooks = result.count;
        filters.Clear();
        foreach (var A in resAuthors)
        {
            var booksResult = await Model.GetBooksByAuthorId(A.Id, 0, 10);
            filters.Add(new Tuple<String, List<BookVM>>(A.Name, (List<BookVM>)booksResult.books.Select(b => new BookVM(b))));
        }
    }

    private async Task GetBooksBorrowings()
    {
        var result = await Model.GetCurrentBorrowings(Index, Count);
        nbBooks = result.count;
        IEnumerable<Borrowing> resBooks = result.borrowings;
        //borrowingBooks.Clear();
    }

    private void RefreshPagination()
    {
        Index = 0;
        Count = 10;
    }

    private async Task GetBooksLoans()
    {
        var result = await Model.GetCurrentLoans(Index, Count);
        IEnumerable<Loan> resBooks = result.loans;
        nbBooks = result.count;
        //borrowingBooks.Clear();
    }

    private async Task GetBookById(string Id)
    {
        var result = await Model.GetBookById(Id);
        book = new BookVM(result);
    }

    public int Index { get; set; }
    public int Count { get; set; } = 10;
    public long nbBooks { get; set; }
    public int nbPages => (int)(nbBooks / Count);
} 