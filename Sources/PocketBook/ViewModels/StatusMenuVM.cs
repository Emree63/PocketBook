using System;
using System.Diagnostics;
using System.Windows.Input;
using MyMVVM_ToolKit;
using ViewModel;

namespace PocketBook.ViewModels
{
	public class StatusMenuVM : BaseViewModel
    {
        public ICommand DisplayStatusMenu { get; set; }

        public StatusMenuVM(ManagerVM mgrVM)
		{
            DisplayStatusMenu = new Command(async (Id) =>
            {
                var statusValues = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
                string[] statusOptions = statusValues.Select(status => status.ToString()).ToArray();
                string selectedStatus = await App.Current.MainPage.DisplayActionSheet("Changer le Statut", "Annuler", null, statusOptions);

                if (selectedStatus != "Annuler")
                {
                    mgrVM.UpdateStatusCommand.Execute(selectedStatus);
                }
            });
        }

        public enum Status
        {
            Unknown,
            Finished,
            Reading,
            NotRead,
            ToBeRead
        }
    }
}

