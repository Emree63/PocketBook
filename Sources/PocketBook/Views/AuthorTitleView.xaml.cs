namespace PocketBook.Views;

public partial class AuthorTitleView : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AuthorTitleView), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public AuthorTitleView()
	{
		InitializeComponent();
	}
}