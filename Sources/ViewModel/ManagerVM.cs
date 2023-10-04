using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;
using MyMVVM_ToolKit;

namespace ViewModel;

public class ManagerVM : BaseViewModel<Manager>
{
    public ICommand GetBooksFromCollectionCommand { get; private set; }

    public ReadOnlyObservableCollection<BookVM> Books { get; private set; }
    private readonly ObservableCollection<BookVM> books = new ObservableCollection<BookVM>();

    public ManagerVM(Manager model) : base(model)
    {
        Books = new ReadOnlyObservableCollection<BookVM>(books);
        GetBooksFromCollectionCommand = new RelayCommand(async () => {
            await GetBooksFromCollection();
        });
    }

    public ManagerVM(ILibraryManager libMgr, IUserLibraryManager userMgr)
    {
        //Model = new Manager(libMgr, userMgr);
    }

    private async Task GetBooksFromCollection()
    {
        var result = await Model.GetBooksByTitle("", 0, 100);
        IEnumerable<Book> resBooks = result.books;
        books.Clear();
        foreach (var B in resBooks)
        {
            books.Add(new BookVM(B));
        }
    }

    public int Index { get; set; }
    public int Count { get; set; } = 5;
    public long nbBooks { get; set; }
    public int nbPages => (int)(nbBooks / Count);
} 