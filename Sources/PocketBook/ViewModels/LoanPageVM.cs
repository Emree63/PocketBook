using System;
using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class LoanPageVM
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public ICommand NavigatorToBookPageCommand { get; set; }

        public LoanPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
            NavigatorToBookPageCommand = new Command(async (Id) => {
                Manager.GetBookByIdFromCollectionCommand.Execute(Id);
                Navigation.NavigatorToBookPageCommand.Execute(null);
            });
        }
    }
}

