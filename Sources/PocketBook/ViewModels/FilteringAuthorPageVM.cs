using System;
using System.Windows.Input;
using MyMVVM_ToolKit;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringAuthorPageVM : FilteringPageVM
	{

        public FilteringAuthorPageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM,mgrVM)
        {
            NavigatorToBooksPageCommand = new Command(async (author) => {
                Manager.GetBooksByAuthorCommand.Execute(author);
                Navigation.NavigatorToBooksPageCommand.Execute(null);
            });
        }
	}
}

