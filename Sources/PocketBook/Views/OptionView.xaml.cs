using System.Windows.Input;

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

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(OptionView), null);
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty IdProperty = BindableProperty.Create(nameof(Id), typeof(string), typeof(OptionView), string.Empty);
    public string Id
    {
        get => (string)GetValue(OptionView.IdProperty);
        set => SetValue(OptionView.IdProperty, value);
    }

    public OptionView()
	{
		InitializeComponent();
	}
}