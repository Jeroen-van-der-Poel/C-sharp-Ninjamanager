using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using NinjaManager.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class EditNinjaViewModel : ViewModelBase
    {
        private NinjaVM _selectedNinja;

        private NinjaListViewModel _listViewModel;

        public ICommand SaveCommand { get; set; }

        public NinjaVM SelectedNinja
        {
            get { return _selectedNinja; }
        }

        public EditNinjaViewModel(NinjaVM selectedNinja, NinjaListViewModel listViewModel)
        {
            this._selectedNinja = selectedNinja;
            this._listViewModel = listViewModel;
            SaveCommand = new RelayCommand(saveCommand);
        }

        private void saveCommand()
        {
            if (Regex.IsMatch(_selectedNinja.Name, @"^^(?! )[A-Za-z\s]+$"))
            {
                using (var context = new MyEntities())
                {
                    context.Entry(_selectedNinja.ToModel()).State = EntityState.Modified;
                    context.SaveChanges();
                }
                _listViewModel.CloseEditNinja();
            }
        }
    }
}
