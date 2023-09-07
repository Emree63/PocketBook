namespace PocketBook.Views;

public partial class FilterView : ContentView
{
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(FilterView), string.Empty);

    public string Icon
    {
        get => (string)GetValue(FilterView.IconProperty);
        set => SetValue(FilterView.IconProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(FilterView), string.Empty);

    public string Title
    {
        get => (string)GetValue(FilterView.TitleProperty);
        set => SetValue(FilterView.TitleProperty, value);
    }

    public static readonly BindableProperty NumberProperty = BindableProperty.Create(nameof(Number), typeof(string), typeof(FilterView), string.Empty);
    public string Number
    {
        get => (string)GetValue(FilterView.NumberProperty);
        set => SetValue(FilterView.NumberProperty, value);
    }

    public static readonly BindableProperty BottomBorderProperty = BindableProperty.Create(nameof(BottomBorder), typeof(bool), typeof(FilterView), true);
    public bool BottomBorder
    {
        get => (bool)GetValue(FilterView.BottomBorderProperty);
        set => SetValue(FilterView.BottomBorderProperty, value);
    }

    public FilterView()
	{
		InitializeComponent();
	}

}