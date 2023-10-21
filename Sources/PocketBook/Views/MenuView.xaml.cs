using System.Windows.Input;
using PocketBook.ViewModels;

namespace PocketBook.Views;

public partial class MenuView : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FilterView), null);
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    public MenuView()
	{
		InitializeComponent();
	}
}
