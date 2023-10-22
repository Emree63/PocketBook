using System.Collections.ObjectModel;
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
    public ICommand GetBooksByDateCommand { get; private set; }
    public ICommand GetBooksByNoteCommand { get; private set; }
    public ICommand GetDatesFromCollectionCommand { get; private set; }
    public ICommand GetNotesFromCollectionCommand { get; private set; }
    public ICommand AddBookByISBNCommand { get; private set; }
    public ICommand RefreshPaginationCommand { get; private set; }
    public ICommand NextBooksCollectionCommand { get; private set; }
    public ICommand PreviousBooksCollectionCommand { get; private set; }
    public ICommand GetLoansBookCommand { get; private set; }
    public ICommand GetBorrowingsBookCommand { get; private set; }
    public ICommand ReverseBooksCommand { get; private set; }
    public ICommand ReverseFilteringsCommand { get; private set; }
    public ICommand AddBookToFavoriteCommand { get; private set; }
    public ICommand DeleteBookToCollectionCommand { get; private set; }
    public ICommand DeleteBookToFavoriteCommand { get; private set; }
    public ICommand UpdateStatusCommand { get; private set; }
    public ICommand SearchFiltersCommand { get; private set; }

    public ReadOnlyObservableCollection<BookVM> Books { get; private set; }
    private readonly ObservableCollection<BookVM> books = new ObservableCollection<BookVM>();

    public ReadOnlyObservableCollection<IGrouping<string, LoanVM>> Loans { get; private set; }
    private readonly ObservableCollection<IGrouping<string, LoanVM>> loans = new ObservableCollection<IGrouping<string, LoanVM>>();

    public ReadOnlyObservableCollection<IGrouping<string, BorrowingVM>> Borrowings { get; private set; }
    private readonly ObservableCollection<IGrouping<string, BorrowingVM>> borrowings = new ObservableCollection<IGrouping<string, BorrowingVM>>();

    private string sortBooks = "asc";

    public IEnumerable<IGrouping<string, BookVM>> GroupedBooks
        => books.MyGroupBy(book => book.FirstAuthor);

    public ReadOnlyObservableCollection<Tuple<string, int>> Filters
    {
        get
        {
            var filteredList = filters.Where(filter => filter.Item1.Contains(search));

            return new ReadOnlyObservableCollection<Tuple<string, int>>(new ObservableCollection<Tuple<string, int>>(filteredList));
        }
    }

    private readonly ObservableCollection<Tuple<string, int>> filters = new ObservableCollection<Tuple<string, int>>();

    private string search = "";

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
        Loans = new ReadOnlyObservableCollection<IGrouping<string, LoanVM>>(loans);
        GetBooksFromCollectionCommand = new RelayCommandAsync<string>(GetBooksFromCollection);
        GetAuthorsFromCollectionCommand = new RelayCommandAsync(GetAuthorsFromCollection);
        GetDatesFromCollectionCommand = new RelayCommandAsync(GetDatesFromCollection);
        GetNotesFromCollectionCommand = new RelayCommandAsync(GetNotesFromCollection);
        GetBooksByAuthorCommand = new RelayCommandAsync<string>(GetBooksByAuthor);
        GetBooksByDateCommand = new RelayCommandAsync<string>(GetBooksByDate);
        GetBooksByNoteCommand = new RelayCommandAsync<string>(GetBooksByNote);
        GetBookByIdCommand = new RelayCommandAsync<string>(GetBookByIdFromCollection);
        AddBookByISBNCommand = new RelayCommandAsync<string>(AddBookByISBN);
        RefreshPaginationCommand = new RelayCommand(RefreshPagination);
        GetFavoriteBooksCommand = new RelayCommandAsync(GetFavoriteBooks);
        NextBooksCollectionCommand = new RelayCommandAsync(NextBooks);
        PreviousBooksCollectionCommand = new RelayCommandAsync(PreviousBooks);
        ReverseBooksCommand = new RelayCommandAsync(ReverseBooks);
        ReverseFilteringsCommand = new RelayCommand(ReverseFilterings);
        GetBooksFromCollectionCommand.Execute("author");
        GetLoansBookCommand = new RelayCommandAsync(GetLoansBook);
        GetBorrowingsBookCommand = new RelayCommandAsync(GetBorrowingsBook);
        AddBookToFavoriteCommand = new RelayCommandAsync<string>(AddBookToFavorite);
        DeleteBookToCollectionCommand = new RelayCommandAsync<string>(DeleteBookToCollection);
        DeleteBookToFavoriteCommand = new RelayCommandAsync<string>(DeleteBookToFavorite);
        UpdateStatusCommand = new RelayCommand<string>(UpdateStatus);
        SearchFiltersCommand = new RelayCommand<string>(SearchFilters);
        books.CollectionChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(GroupedBooks));
        };
    }

    private async Task GetBooksFromCollection(string sort)
    {
        var result = await Model.GetBooksFromCollection(Index, Count, sort);
        NbBooks = result.count;
        NbBooksAll = result.count;
        books.Clear();
        var resBooks = result.books.Select(b => new BookVM(b));
        foreach (var book in resBooks)
        {
            books.Add(book);
        }
    }

    private async Task ReverseBooks()
    {
        if (sortBooks == "asc")
        {
            sortBooks = "desc";
            await GetBooksFromCollection("author_reverse");
        }
        else
        {
            sortBooks = "asc";
            await GetBooksFromCollection("author");
        }
        OnPropertyChanged(nameof(GroupedBooks));
    }

    public async Task NextBooks()
    {
        if (Index < nbPages)
        {
            Index++;
            await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
        }
    }

    public void SearchFilters(string search)
    {
        this.search = search;
        OnPropertyChanged(nameof(Filters));
    }

    public void UpdateStatus(string status)
    {
        switch (status)
        {
            case "Unknown":
                Book.Status = Status.Unknown;
                break;
            case "Finished":
                Book.Status = Status.Finished;
                break;
            case "Reading":
                Book.Status = Status.Reading;
                break;
            case "NotRead":
                Book.Status = Status.NotRead;
                break;
            case "ToBeRead":
                Book.Status = Status.ToBeRead;
                break;
            default:
                Book.Status = Status.Unknown;
                break;
        }
        OnPropertyChanged(nameof(GroupedBooks));
    }

    public async Task PreviousBooks()
    {
        if (Index > 0)
        {
            Index--;
            await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
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
        var result = await Model.GetBooksByTitle("", 0, 1000);
        IEnumerable<BookVM> resAuthors = result.books.Select(b => new BookVM(b));

        var groupedBooksByDate = resAuthors.GroupBy(book => book.Year).OrderByDescending(group => group.Key);

        var dates = groupedBooksByDate.Select(group =>
            new Tuple<string, int>(group.Key, group.Count())
        );

        filters.Clear();

        foreach (var date in dates)
        {
            filters.Add(date);
        }
    }

    private async Task GetNotesFromCollection()
    {
        var result = await Model.GetBooksFromCollection(0, 1000);
        IEnumerable<BookVM> resAuthors = result.books.Select(b => new BookVM(b));

        var groupedBooksByDate = resAuthors.GroupBy(book => book.UserRating.ToString()).OrderByDescending(group => group.Key);

        var notes = groupedBooksByDate.Select(group =>
            new Tuple<string, int>(string.IsNullOrEmpty(group.Key) ? "0 étoiles" : group.Key + " étoiles", group.Count())
        );

        filters.Clear();

        foreach (var note in notes)
        {
            filters.Add(note);
        }
    }

    private async Task GetFavoriteBooks()
    {
        var result = await Model.GetFavoritesBooks(Index, Count);
        IEnumerable<BookVM> resBooks = result.Item2.Select(b => new BookVM(b));
        NbBooks = result.Item1;
        books.Clear();
        foreach (var book in resBooks)
        {
            books.Add(book);
        }
    }

    private void ReverseFilterings()
    {
        List<Tuple<string, int>> tempList = new List<Tuple<string, int>>(filters);
        tempList.Reverse();
        filters.Clear();
        foreach (var item in tempList)
        {
            filters.Add(item);
        }
    }

    private async Task GetBooksByAuthor(string author)
    {
        var result = await Model.GetBooksByAuthor(author, Index, Count);
        IEnumerable<Book> resBooks = result.books;
        NbBooks = result.count;
        books.Clear();
        var booksVM = resBooks.Select(b => new BookVM(b));
        foreach (var book in booksVM)
        {
            books.Add(book);
        }
    }

    private async Task GetBooksByDate(string date)
    {
        var result = await Model.GetBooksByDate(date, Index, Count);
        IEnumerable<Book> resBooks = result.books;
        NbBooks = result.count;
        books.Clear();
        var booksVM = resBooks.Select(b => new BookVM(b));
        foreach (var book in booksVM)
        {
            books.Add(book);
        }
    }

    private async Task GetBooksByNote(string note)
    {
        var result = await Model.GetBooksByNoteFromCollection(note, Index, Count);
        IEnumerable<Book> resBooks = result.books;
        NbBooks = result.count;
        books.Clear();
        var booksVM = resBooks.Select(b => new BookVM(b));
        foreach (var book in booksVM)
        {
            books.Add(book);
        }
    }

    private async Task GetBorrowingsBook()
    {
        var result = await Model.GetCurrentBorrowings(Index, Count);
        IEnumerable<Borrowing> resBorrowings = result.borrowings;
        NbBooks = result.count;
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
        Book check = await Model.GetBookByIdFromCollection(result.Id);
        if (check == null)
        {
            await Model.AddBookToCollection(result.Id);
            await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
        }
    }

    private async Task AddBookToFavorite(string id)
        => await Model.AddToFavorites(id);



    private async Task DeleteBookToCollection(string id)
    {
        await Model.RemoveBook(id);
        await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
    }

    private async Task DeleteBookToFavorite(string id)
    {
        await Model.RemoveFromFavorites(id);
        await GetFavoriteBooks();
    }


    private void RefreshPagination()
    {
        Index = 0;
        Count = 5;
    }

    private async Task GetLoansBook()
    {
        var result = await Model.GetCurrentLoans(index, Count);
        IEnumerable<Loan> resLoans = result.loans;
        loans.Clear();
        NbBooks = result.count;
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

    public int Index
    {
        get => index;
        set
        {
            if (index != value)
            {
                index = value;
                OnPropertyChanged(nameof(Index));
                OnPropertyChanged(nameof(possibilityNext));
                OnPropertyChanged(nameof(possibilityPrevious));
            }
        }
    }
    private int index { get; set; }
    public int Count { get; set; } = 5;
    public long NbBooks {
        get => nbBooks;
        set
        {
            if (nbBooks != value)
            {
                nbBooks = value;
                OnPropertyChanged(nameof(NbBooks));
                OnPropertyChanged(nameof(nbPages));
            }
        }
    }
    private long nbBooks;
    public long NbBooksAll
    {
        get => nbBooksAll;
        set
        {
            if (nbBooksAll != value)
            {
                nbBooksAll = value;
                OnPropertyChanged(nameof(NbBooksAll));
            }
        }
    }
    private long nbBooksAll;
    public int nbPages => (int)(nbBooks / Count);
    public bool possibilityNext => Index != nbPages;
    public bool possibilityPrevious => Index != 0;
}