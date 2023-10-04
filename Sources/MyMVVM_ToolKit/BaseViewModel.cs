using System;
namespace MyMVVM_ToolKit
{
	public class BaseViewModel<TModel> : ObservableObject
	{
		public TModel Model
		{
			get => Model;
			set
			{
                SetProperty(ref model, value);
			}
		}
		private TModel model;

		public BaseViewModel(TModel model)
		{
			Model = model;
		}

		public BaseViewModel()
		{ }
	}

	public class BaseViewModel: ObservableObject
	{

	}
}

