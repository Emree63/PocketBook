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
    public ICommand GetFavoriteBooksCommand { get; private set; }
    public ICommand GetBookByIdCommand { get; private set; }
    public ICommand GetBooksByAuthorCommand { get; private set; }
    public ICommand GetDatesFromCollectionCommand { get; private set; }
    public ICommand AddBookByISBNCommand { get; private set; }
    public ICommand RefreshPaginationCommand { get; private set; }
    public ICommand NextBooksCollectionCommand { get; private set; }
    public ICommand PreviousBooksCollectionCommand { get; private set; }
    public ICommand GetLoansBookCommand { get; private set; }
    public ICommand GetBorrowingsBookCommand { get; private set; }
    public ICommand ReverseBooksCommand { get; private set; }

    public ReadOnlyObservableCollection<IGrouping<string, BookVM>> Books { get; private set; }
    private readonly ObservableCollection<IGrouping<string, BookVM>> books = new ObservableCollection<IGrouping<string, BookVM>>();

    public ReadOnlyObservableCollection<IGrouping<string, LoanVM>> Loans { get; private set; }
    private readonly ObservableCollection<IGrouping<string, LoanVM>> loans = new ObservableCollection<IGrouping<string, LoanVM>>();

    public ReadOnlyObservableCollection<IGrouping<string, BorrowingVM>> Borrowings { get; private set; }
    private readonly ObservableCollection<IGrouping<string, BorrowingVM>> borrowings = new ObservableCollection<IGrouping<string, BorrowingVM>>();

    //public IEnumerable<IGrouping<string, BookVM>> groupedBooks
    //    => books.MyGroupBy(keyGroup).MySort("asc")

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
        Loans = new ReadOnlyObservableCollection<IGrouping<string, LoanVM>>(loans);
        Filters = new ReadOnlyObservableCollection<Tuple<string, int>>(filters);
        GetBooksFromCollectionCommand = new RelayCommandAsync(GetBooksFromCollection);
        GetAuthorsFromCollectionCommand = new RelayCommandAsync(GetAuthorsFromCollection);
        GetDatesFromCollectionCommand = new RelayCommandAsync(GetDatesFromCollection);
        GetBooksByAuthorCommand = new RelayCommandAsync<string>(GetBooksByAuthor);
        GetBookByIdCommand = new RelayCommandAsync<string>(GetBookByIdFromCollection);
        AddBookByISBNCommand = new RelayCommandAsync<string>(AddBookByISBN);
        RefreshPaginationCommand = new RelayCommand(RefreshPagination);
        GetFavoriteBooksCommand = new RelayCommandAsync(GetFavoriteBooks);
        NextBooksCollectionCommand = new RelayCommandAsync(Next);
        PreviousBooksCollectionCommand = new RelayCommandAsync(Previous);
        ReverseBooksCommand = new RelayCommand(ReverseBooks);
        GetBooksFromCollectionCommand.Execute(null);
        GetLoansBookCommand = new RelayCommandAsync(GetLoansBook);
        GetBorrowingsBookCommand = new RelayCommandAsync(GetBorrowingsBook);
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

    private void ReverseBooks()
    {
        books.Reverse();
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
        filters.Clear();
        foreach (var A in resAuthors)
        {
            var booksResult = await Model.GetBooksByAuthor(A.Name, 0, 1000);
            filters.Add(new Tuple<string, int>(A.Name, (int)booksResult.count));
        }
    }

    private async Task GetDatesFromCollection()
    {
        var result = await Model.GetBooksFromCollection(0, 1000);
        IEnumerable<BookVM> resAuthors = result.books.Select(b => new BookVM(b));

        var groupedBooksByDate = resAuthors.GroupBy(book => book.Year).OrderByDescending(group => group.Key);

        var dateCountTuples = groupedBooksByDate.Select(group =>
            new Tuple<string, int>(group.Key, group.Count())
        );

        filters.Clear();

        foreach (var tuple in dateCountTuples)
        {
            filters.Add(tuple);
        }
    }

    private async Task GetFavoriteBooks()
    {
        var result = await Model.GetFavoritesBooks(Index, Count);
        IEnumerable<BookVM> resBooks = result.Item2.Select(b => new BookVM(b));
        nbBooks = result.Item1;
        books.Clear();
        var groupedBooks = resBooks.GroupBy(book => book.FirstAuthor).OrderBy(group => group.Key);
        foreach (var group in groupedBooks)
        {
            books.Add(group);
        }
    }

    private void ReverseAuthors()
    {
        filters.Reverse();
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

    private async Task GetBorrowingsBook()
    {
        var result = await Model.GetCurrentBorrowings(0, 1000);
        IEnumerable<Borrowing> resBorrowings = result.borrowings;
        borrowings.Clear();
        var borrowingsVM = resBorrowings.Select(b => new BorrowingVM(b));
        var groupedBorrowings = borrowingsVM.GroupBy(book => book.FirstAuthor).OrderBy(group => group.Key);
        foreach (var group in groupedBorrowings)
        {
            borrowings.Add(group);
        }
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
        Count = 5;
    }

    private async Task GetLoansBook()
    {
        var result = await Model.GetCurrentLoans(0, 1000);
        IEnumerable<Loan> resLoans = result.loans;
        loans.Clear();
        var loansVM = resLoans.Select(b => new LoanVM(b));
        var groupedLoans = loansVM.GroupBy(book => book.FirstAuthor).OrderBy(group => group.Key);
        foreach (var group in groupedLoans)
        {
            loans.Add(group);
        }
    }

    private async Task GetBookByIdFromCollection(string Id)
    {
        var result = await Model.GetBookByIdFromCollection(Id);
        book = new BookVM(result);
    }

    public int Index { get; set; }
    public int IndexForPagination => Index + 1;
    public int Count { get; set; } = 5;
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
    public bool possibilityNext => Index != nbPages;
    public bool possibilityPrevious => Index != 0;
    public int nbPagesForPagination => nbPages + 1;

}