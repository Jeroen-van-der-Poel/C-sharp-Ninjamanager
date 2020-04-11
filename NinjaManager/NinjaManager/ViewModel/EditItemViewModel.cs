using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
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
    public class EditItemViewModel : ViewModelBase
    {
        private EquipmentVM _selectedEquipment;

        private ShopListViewModel _listViewModel;

        public ICommand SaveCommand { get; set; }

        public EquipmentVM SelectedEquipment
        {
            get { return _selectedEquipment; }
        }

        public EditItemViewModel(EquipmentVM selectedEquipment, ShopListViewModel listViewModel)
        {
            this._selectedEquipment = selectedEquipment;
            this._listViewModel = listViewModel;
            SaveCommand = new RelayCommand(saveCommand);
        }

        private void saveCommand()
        {
            if (Regex.IsMatch(_selectedEquipment.EquipmentName, @"^^(?! )[A-Za-z\s]+$"))
            {
                using (var context = new MyEntities())
                {
                    context.Entry(_selectedEquipment.ToModel()).State = EntityState.Modified;
                    context.SaveChanges();
                }
                _listViewModel.CloseEditItem();

            }
        }
    }
}
