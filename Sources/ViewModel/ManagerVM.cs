using System.Collections.ObjectModel;
using System.Linq;
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
    public ICommand GetBooksByAuthorCommand { get; private set; }
    public ICommand AddBookByISBNCommand { get; private set; }
    public ICommand RefreshPaginationCommand { get; private set; }
    public ICommand NextBooksCollectionCommand { get; private set; }
    public ICommand PreviousBooksCollectionCommand { get; private set; }

    public ReadOnlyObservableCollection<IGrouping<string, BookVM>> Books { get; private set; }
    private readonly ObservableCollection<IGrouping<string, BookVM>> books = new ObservableCollection<IGrouping<string, BookVM>>();

    public ReadOnlyObservableCollection<Tuple<string, int>> Filters { get; private set; }
    private readonly ObservableCollection<Tuple<string, int>> filters = new ObservableCollection<Tuple<string, int>>();

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
        Books = new ReadOnlyObservableCollection<IGrouping<string, BookVM>>(books);
        Filters = new ReadOnlyObservableCollection<Tuple<string, int>>(filters);
        GetBooksFromCollectionCommand = new RelayCommandAsync(GetBooksFromCollection);
        GetAuthorsFromCollectionCommand = new RelayCommandAsync(GetAuthorsFromCollection);
        GetBooksByAuthorCommand = new RelayCommandAsync<string>(GetBooksByAuthor);
        GetBookByIdCommand = new RelayCommandAsync<string>(GetBookByIdFromCollection);
        AddBookByISBNCommand = new RelayCommandAsync<string>(AddBookByISBN);
        RefreshPaginationCommand = new RelayCommand(RefreshPagination);
        NextBooksCollectionCommand = new RelayCommandAsync(Next);
        PreviousBooksCollectionCommand = new RelayCommandAsync(Previous);
        GetBooksFromCollectionCommand.Execute(null);
    }

    private async Task GetBooksFromCollection()
    {
        var result = await Model.GetBooksFromCollection(Index, Count);
        IEnumerable<Book> resBooks = result.books;
        nbBooks = result.count;
        books.Clear();
        var booksVM = resBooks.Select(b => new BookVM(b));
        var groupedBooks = booksVM.GroupBy(book => book.FirstAuthor).OrderBy(group => group.Key);
        foreach (var group in groupedBooks)
        {
            books.Add(group);
        }
    }

    public async Task Next()
    {
        if (Index < nbPages)
        {
            Index++;
            await GetBooksFromCollection();
        }
    }

    public async Task Previous()
    {
        if (Index > 0)
        {
            Index--;
            await GetBooksFromCollection();
        }
    }

    private async Task GetAuthorsFromCollection()
    {
        var result = await Model.GetAuthorsFromCollection(0, 1000);
        IEnumerable<Author> resAuthors = result.authors;
        nbBooks = result.count;
        filters.Clear();
        foreach (var A in resAuthors)
        {
            var booksResult = await Model.GetBooksByAuthor(A.Name, 0, 1000);
            filters.Add(new Tuple<string, int>(A.Name, (int)booksResult.count));
        }
    }

    private async Task GetBooksByAuthor(string author)
    {
        var result = await Model.GetBooksByAuthor(author, Index, Count);
        IEnumerable<Book> resBooks = result.books;
        nbBooks = result.count;
        books.Clear();
        var booksVM = resBooks.Select(b => new BookVM(b));
        var groupedBooks = booksVM.GroupBy(book => book.FirstAuthor).OrderBy(group => group.Key);
        foreach (var group in groupedBooks)
        {
            books.Add(group);
        }
    }

    private async Task GetBooksBorrowings()
    {
        var result = await Model.GetCurrentBorrowings(Index, Count);
        nbBooks = result.count;
        IEnumerable<Borrowing> resBooks = result.borrowings;
        //borrowingBooks.Clear();
    }

    private async Task AddBookByISBN(string isbn)
    {
        Book result = await Model.GetBookByISBN(isbn);
        await Model.AddBookToCollection(result.Id);
        NbBooks++;

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

    private async Task GetBookByIdFromCollection(string Id)
    {
        var result = await Model.GetBookByIdFromCollection(Id);
        book = new BookVM(result);
    }

    public int Index { get; set; }
    public int Count { get; set; } = 10;
    public long NbBooks {
        get => nbBooks;
        set
        {
            if (nbBooks != value)
            {
                nbBooks = value;
                OnPropertyChanged(nameof(NbBooks));
            }
        }
    }
    private long nbBooks;
    public int nbPages => (int)(nbBooks / Count);
} 