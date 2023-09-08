namespace PocketBook.Views;

public partial class OptionView : ContentView
{
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(OptionView), string.Empty);

    public string Icon
    {
        get => (string)GetValue(OptionView.IconProperty);
        set => SetValue(OptionView.IconProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(OptionView), string.Empty);

    public string Title
    {
        get => (string)GetValue(OptionView.TitleProperty);
        set => SetValue(OptionView.TitleProperty, value);
    }
    public OptionView()
	{
		InitializeComponent();
	}
}