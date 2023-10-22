using System.Windows.Input;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class ScanMenuVM : BaseViewModel
	{
        public bool MenuIsVisible
        {
            get => menuIsVisible;
            set
            {
                if (menuIsVisible != value)
                {
                    menuIsVisible = value;
                    OnPropertyChanged(nameof(MenuIsVisible));
                }
            }
        }
        private bool menuIsVisible { get; set; }

        public ICommand ToggleScanMenu { get; set; }
        public ICommand DisplayScanMenu { get; set; }

        public ScanMenuVM(ManagerVM mgrVM)
		{
            MenuIsVisible = false;
            DisplayScanMenu = new Command(async () =>
            {
                string isbn = await (App.Current as App).MainPage.DisplayPromptAsync("Saisir l'ISBN", "Entrez l'ISBN pour ajouter le livre à votre collection");

                if (!string.IsNullOrEmpty(isbn))
                {
                    mgrVM.AddBookByISBNCommand.Execute(isbn);
                }
            });

            ToggleScanMenu = new Command(() => {
                MenuIsVisible = !MenuIsVisible;
            });
        }
	}
}

