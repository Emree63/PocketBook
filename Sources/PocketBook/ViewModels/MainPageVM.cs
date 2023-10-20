﻿using System;
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
        public ICommand NavigatorToDatesPageCommand { get; set; }
        public ICommand NavigatorToFavoritePageCommand { get; set; }
        public ICommand NavigatorToLoanPageCommand { get; set; }

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
            NavigatorToDatesPageCommand = new Command(async () => {
                Manager.GetDatesFromCollectionCommand.Execute(null);
                Navigation.NavigatorToFilteringDatesPageCommand.Execute(null);
            });
            NavigatorToFavoritePageCommand = new Command(async () => {
                Manager.GetFavoriteBooksCommand.Execute(null);
                Navigation.NavigatorToBooksPageCommand.Execute(null);
            });
            NavigatorToLoanPageCommand = new Command(async () => {
                Manager.GetLoansBookCommand.Execute(null);
                Manager.GetBorrowingsBookCommand.Execute(null);
                Navigation.NavigatorToLoanPageCommand.Execute(null);
            });
        }
	}
}

