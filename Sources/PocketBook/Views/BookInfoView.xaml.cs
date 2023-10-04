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

    public BookInfoView()
	{
		InitializeComponent();
	}
}