using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel
{
	public partial class BaseViewModel<TModel> : ObservableObject
    {
        [ObservableProperty]
        private TModel model;

        public BaseViewModel(TModel model)
        {
            Model = model;
        }

        public BaseViewModel()
        { }
    }

    public class BaseViewModel : ObservableObject
    {
    }
}

