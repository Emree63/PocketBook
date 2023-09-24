using System.Windows.Input;
using Model;

namespace ViewModel;

public class ManagerVM
{
    public Manager Model { get; set; }
    private Manager model;

    public List<Book> Books { get; set; }

    public ICommand GetBooksByTitleCommand { get; set; }

    public ManagerVM(Manager model)
    {
        Model = model;
        /*GetBooksByTitleCommand = new Command(async () => {
            var result = await Model.GetBooksByTitle();
            Books?.Clear();
            Books?.AddRange(result.books.Select(base => new BookVM(b)));
        });$*/
    }


}

