using System;
using System.Windows.Input;
using MyMVVM_ToolKit;

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

        public ScanMenuVM()
		{
            MenuIsVisible = false;
            DisplayScanMenu = new Command(async () =>
            {
                await (App.Current as App).MainPage.DisplayPromptAsync("Saisir l'ISBN", "Entrez l'ISBN pour ajouter le livre à votre collection");
            });

            ToggleScanMenu = new Command(() => {
                MenuIsVisible = !MenuIsVisible;
            });
        }
	}
}

