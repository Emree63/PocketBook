using System;
using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringPageVM
	{
		public NavigationVM Navigation { get; set; }
		public ManagerVM Manager { get; set; }
		public ICommand NavigatorToBooksPageCommand { get; set; }

        public FilteringPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
        }
    }
}

