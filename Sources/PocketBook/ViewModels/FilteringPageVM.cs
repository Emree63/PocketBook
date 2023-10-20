using System;
using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringPageVM
	{
        public string TitlePage { get; set; }
        public bool ExitText { get; set; }
        public NavigationVM Navigation { get; set; }
		public ManagerVM Manager { get; set; }
		public virtual ICommand NavigatorToBooksPageCommand { get; set; }

        public FilteringPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
        }
    }
}

