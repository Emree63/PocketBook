using System.Windows.Input;

namespace PocketBook.Views;

public partial class BookInfoView : ContentView
{
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(BookInfoView), string.Empty);

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(float), typeof(StarsView), (float)0);

    public float Value
    {
        get => (float)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(BookInfoView), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty AuthorProperty = BindableProperty.Create(nameof(Author), typeof(string), typeof(BookInfoView), string.Empty);
    public string Author
    {
        get => (string)GetValue(AuthorProperty);
        set => SetValue(AuthorProperty, value);
    }

    public static readonly BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(string), typeof(BookInfoView), string.Empty);
    public string Status
    {
        get => (string)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public static readonly BindableProperty BottomBorderProperty = BindableProperty.Create(nameof(BottomBorder), typeof(bool), typeof(BookInfoView), true);
    public bool BottomBorder
    {
        get => (bool)GetValue(BottomBorderProperty);
        set => SetValue(BottomBorderProperty, value);
    }

    public static readonly BindableProperty CanAddProperty = BindableProperty.Create(nameof(CanAdd), typeof(bool), typeof(BookInfoView), true);
    public bool CanAdd
    {
        get => (bool)GetValue(CanAddProperty);
        set => SetValue(CanAddProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FilterView), null);
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandAddProperty = BindableProperty.Create(nameof(CommandAdd), typeof(ICommand), typeof(FilterView), null);
    public ICommand CommandAdd
    {
        get => GetValue(CommandAddProperty) as ICommand;
        set => SetValue(CommandAddProperty, value);
    }

    public static readonly BindableProperty CommandDeleteProperty = BindableProperty.Create(nameof(CommandDelete), typeof(ICommand), typeof(FilterView), null);
    public ICommand CommandDelete
    {
        get => GetValue(CommandDeleteProperty) as ICommand;
        set => SetValue(CommandDeleteProperty, value);
    }


    public static readonly BindableProperty IdProperty = BindableProperty.Create(nameof(Id), typeof(string), typeof(FilterView), null);
    public string Id
    {
        get => (string)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public BookInfoView()
	{
		InitializeComponent();
	}
}