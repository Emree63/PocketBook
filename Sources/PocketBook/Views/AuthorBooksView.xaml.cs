namespace PocketBook.Views;

public partial class AuthorBooksView : ContentView
{
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(AuthorBooksView), string.Empty);

    public string Icon
    {
        get => (string)GetValue(AuthorBooksView.IconProperty);
        set => SetValue(AuthorBooksView.IconProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AuthorBooksView), string.Empty);

    public string Title
    {
        get => (string)GetValue(AuthorBooksView.TitleProperty);
        set => SetValue(AuthorBooksView.TitleProperty, value);
    }

    public static readonly BindableProperty NumberProperty = BindableProperty.Create(nameof(Number), typeof(string), typeof(AuthorBooksView), string.Empty);
    public string Number
    {
        get => (string)GetValue(AuthorBooksView.NumberProperty);
        set => SetValue(AuthorBooksView.NumberProperty, value);
    }

    public AuthorBooksView()
	{
		InitializeComponent();
	}
}