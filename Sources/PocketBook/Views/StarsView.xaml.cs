namespace PocketBook.Views;

public partial class StarsView : ContentView
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(float), typeof(StarsView), (float)0);

    public float Value
    {
        get => (float)GetValue(ValueProperty)/5*100;
        set => SetValue(ValueProperty, value);
    }

    public StarsView()
	{
		InitializeComponent();
	}
}