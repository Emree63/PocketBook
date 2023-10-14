using System;
using System.Windows.Input;
using Model;
using MyMVVM_ToolKit;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class MainPageVM: BaseViewModel
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public ICommand NavigatorToBooksPageCommand { get; set; }
        public ICommand NavigatorToAuthorsPageCommand { get; set; }

        public MainPageVM(NavigationVM navVM, ManagerVM mgrVM)
		{
            Navigation = navVM;
            Manager = mgrVM;
            NavigatorToBooksPageCommand = new Command(async () => {
                Manager.GetBooksFromCollectionCommand.Execute(null);
                Navigation.NavigatorToBooksPageCommand.Execute(null);
            });
            NavigatorToAuthorsPageCommand = new Command(async () => {
                Manager.GetAuthorsFromCollectionCommand.Execute(null);
                Navigation.NavigatorToFilteringAuthorsPageCommand.Execute(null);
            });
        }
	}
}

