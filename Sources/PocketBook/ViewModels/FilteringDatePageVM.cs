﻿using System;
using Model;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class FilteringDatePageVM : FilteringPageVM
	{
        public FilteringDatePageVM(NavigationVM navVM, ManagerVM mgrVM) : base(navVM, mgrVM)
        {
            ExitText = false;
            TitlePage = "Date de publication";
            NavigatorToBooksPageCommand = new Command(async (date) => {
                Manager.RefreshPaginationCommand.Execute(null);
                Manager.GetBooksByDateCommand.Execute(date);
                Navigation.NavigatorToFiltersBooksPageCommand.Execute(null);
            });
        }
    }
}

