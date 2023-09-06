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

    public FilterView()
	{
		InitializeComponent();
	}

}