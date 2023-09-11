namespace PocketBook.Views;

public partial class FilterView : ContentView
{
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(FilterView), string.Empty);

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(FilterView), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty NumberProperty = BindableProperty.Create(nameof(Number), typeof(string), typeof(FilterView), string.Empty);
    public string Number
    {
        get => (string)GetValue(NumberProperty);
        set => SetValue(NumberProperty, value);
    }

    public static readonly BindableProperty BottomBorderProperty = BindableProperty.Create(nameof(BottomBorder), typeof(bool), typeof(FilterView), true);
    public bool BottomBorder
    {
        get => (bool)GetValue(BottomBorderProperty);
        set => SetValue(BottomBorderProperty, value);
    }

    public FilterView()
	{
		InitializeComponent();
	}

}