using System;
using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class BookPageVM
	{
        public NavigationVM Navigation { get; set; }
        public ManagerVM Manager { get; set; }

        public BookPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
        }
    }
}

