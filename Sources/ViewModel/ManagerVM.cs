using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace ViewModel;

public partial class ManagerVM : BaseViewModel<Manager>
{
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

    [ObservableProperty]
    private BookVM? book = null;

    public ManagerVM(Manager model) : base(model)
    {
        Books = new ReadOnlyObservableCollection<BookVM>(books);
        Loans = new ReadOnlyObservableCollection<IGrouping<string, LoanVM>>(loans);
        GetBooksFromCollectionCommand.Execute(null);
        books.CollectionChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(GroupedBooks));
        };
    }

    [RelayCommand]
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

    [RelayCommand]
    private void ChangeCount(int number)
    {
        Index = 0;
        Count = number;
        OnPropertyChanged(nameof(nbPages));
        OnPropertyChanged(nameof(possibilityNext));
        OnPropertyChanged(nameof(possibilityPrevious));
    }

    [RelayCommand]
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

    [RelayCommand]
    private async Task ReverseFavoriteBooks()
    {
        if (sortBooks == "asc")
        {
            sortBooks = "desc";
            await GetFavoriteBooks();
        }
        else
        {
            sortBooks = "asc";
            await GetFavoriteBooks();
        }
        OnPropertyChanged(nameof(GroupedBooks));
    }

    [RelayCommand]
    public async Task NextBooks()
    {
        if (Index < nbPages)
        {
            Index++;
            await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
        }
    }

    [RelayCommand]
    public async Task NextFavoriteBooks()
    {
        if (Index < nbPages)
        {
            Index++;
            await GetFavoriteBooks();
        }
    }

    [RelayCommand]
    public void SearchFilters(string search)
    {
        this.search = search;
        OnPropertyChanged(nameof(Filters));
    }

    [RelayCommand]
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

    [RelayCommand]
    public async Task PreviousBooks()
    {
        if (Index > 0)
        {
            Index--;
            await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
        }
    }

    [RelayCommand]
    public void RefreshSearch()
    {
        search = "";
    }

    [RelayCommand]
    public async Task PreviousFavoriteBooks()
    {
        if (Index > 0)
        {
            Index--;
            await GetFavoriteBooks();
        }
    }

    [RelayCommand]
    private async Task GetAuthorsFromCollection()
    {
        var result = await Model.GetAuthorsFromCollection(0, 1000);
        IEnumerable<Author> resAuthors = result.authors.OrderBy(a => a.Name);
        filters.Clear();
        foreach (var A in resAuthors)
        {
            var booksResult = await Model.GetBooksByAuthor(A.Name, 0, 1000);
            filters.Add(new Tuple<string, int>(A.Name, (int)booksResult.count));
        }
    }

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
    private void ReverseFilterings()
    {
        List<Tuple<string, int>> tempList = new List<Tuple<string, int>>(filters);
        tempList.Reverse();
        filters.Clear();
        foreach (var item in tempList)
        {
            filters.Add(item);
        }
        OnPropertyChanged(nameof(Filters));
    }

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
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

    [RelayCommand]
    private async Task AddBookToFavorite(string id)
        => await Model.AddToFavorites(id);


    [RelayCommand]
    private async Task DeleteBookToCollection(string id)
    {
        await Model.RemoveBook(id);
        await GetBooksFromCollection(sortBooks == "asc" ? "author" : "author_reverse");
    }

    [RelayCommand]
    private async Task DeleteBookToFavorite(string id)
    {
        await Model.RemoveFromFavorites(id);
        await GetFavoriteBooks();
    }

    [RelayCommand]
    private void RefreshPagination()
    {
        Index = 0;
        Count = 5;
    }

    [RelayCommand]
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

    [RelayCommand]
    private async Task GetBookByIdFromCollection(string Id)
    {
        var result = await Model.GetBookByIdFromCollection(Id);
        book = new BookVM(result);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(possibilityNext))]
    [NotifyPropertyChangedFor(nameof(possibilityPrevious))]
    private int index;

    public int Count { get; set; } = 5;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(nbPages))]
    [NotifyPropertyChangedFor(nameof(possibilityNext))]
    [NotifyPropertyChangedFor(nameof(possibilityPrevious))]
    private long nbBooks;

    [ObservableProperty]
    private long nbBooksAll;

    public int nbPages => (int)(nbBooks / Count);
    public bool possibilityNext => Index != nbPages;
    public bool possibilityPrevious => Index != 0;
}