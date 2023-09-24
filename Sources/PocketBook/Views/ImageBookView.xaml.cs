namespace PocketBook.Views;

public partial class ImageBookView : ContentView
{
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageBookView), string.Empty);

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public ImageBookView()
	{
		InitializeComponent();
	}
}
