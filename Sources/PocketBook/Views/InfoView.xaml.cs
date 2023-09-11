namespace PocketBook.Views;

public partial class InfoView : ContentView
{
    public static readonly BindableProperty TitleProperty =
    BindableProperty.Create(nameof(Title), typeof(string), typeof(InfoView), string.Empty);
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public InfoView()
	{
		InitializeComponent();
	}
}