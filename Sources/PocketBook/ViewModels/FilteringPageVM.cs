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
        public ICommand ReverseCommand { get; set; }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    Manager.SearchFiltersCommand.Execute(searchText);
                }
            }
        }

        public FilteringPageVM(NavigationVM navVM, ManagerVM mgrVM)
        {
            Navigation = navVM;
            Manager = mgrVM;
            ReverseCommand = new Command( () =>
            {
                Manager.ReverseFilteringsCommand.Execute(null);
            });
        }
    }
}

