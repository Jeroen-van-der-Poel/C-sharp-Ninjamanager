using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using NinjaManager.View;
using NinjaManager.ViewModel.Dataservice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class ShopListViewModel : ViewModelBase
    {
        private EquipmentVM _selectedEquipment;
        private IDataService _service;

        public AddItemWindow _addItemWindow;
        public EditItemWindow _editItemWindow;

        public ObservableCollection<EquipmentVM> Equipment { get; set; }

        public ICommand HeadCommand { get; set; }
        public ICommand ShoulderCommand { get; set; }
        public ICommand ChestCommand { get; set; }
        public ICommand BeltCommand { get; set; }
        public ICommand LegsCommand { get; set; }
        public ICommand BootsCommand { get; set; }
        public ICommand BuyCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand ShowAddItemCommand { get; set; }

        public EquipmentVM SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                if (value != null)
                {
                    _selectedEquipment = value;
                }
                base.RaisePropertyChanged();
            }
        }

        public NinjaVM SelectedNinja
        {
            get { return _service.SelectedNinja; }
        }
        public ShopListViewModel(IDataService service)
        {
            _service = service;
            Equipment = new ObservableCollection<EquipmentVM>();

            HeadCommand = new RelayCommand(setHead);
            ShoulderCommand = new RelayCommand(setShoulder);
            ChestCommand = new RelayCommand(setChest);
            BeltCommand = new RelayCommand(setBelt);
            LegsCommand = new RelayCommand(setLegs);
            BootsCommand = new RelayCommand(setBoots);

            BuyCommand = new RelayCommand(BuyItem, CanBuy);
            EditItemCommand = new RelayCommand(ShowEditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            ShowAddItemCommand = new RelayCommand(ShowAddItem);
        }

        #region Private voids ICommands   
        private void ShowAddItem()
        {
            CloseAddItem();
            _addItemWindow = new AddItemWindow();
            _addItemWindow.Show();
        }

        private void ShowEditItem()
        {
            CloseEditItem();
            _editItemWindow = new EditItemWindow();
            _editItemWindow.Show();
        }

        private void DeleteItem()
        {
            using (var context = new MyEntities())
            {
                context.Entry(_selectedEquipment.ToModel()).State = EntityState.Deleted;
                context.SaveChanges();
            }
            Equipment.Remove(_selectedEquipment);
        }

        private bool CanBuy()
        {
            if (_selectedEquipment != null && SelectedNinja.Equipment != null)
            {
                if (SelectedNinja.GoldAmount >= _selectedEquipment.EquipmentPrice)
                {
                    return SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Equals(_selectedEquipment.EquipmentCategory)).ToList().Count() == 0;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void BuyItem()
        {
            if (_selectedEquipment != null)
            {
                Equipment.Remove(_selectedEquipment);
                using (var context = new MyEntities())
                {
                    if (_editItemWindow != null)
                    {
                        _editItemWindow.Close();
                    }
                    context.Ninjas.Attach(SelectedNinja.ToModel());
                    context.Equipments.Attach(_selectedEquipment.ToModel());

                    SelectedNinja.Equipment.Add(SelectedEquipment);
                    SelectedEquipment.ToModel().Ninjas.Add(SelectedNinja.ToModel());
                    SelectedNinja.GoldAmount -= _selectedEquipment.EquipmentPrice;

                    context.SaveChanges();
                }
                base.RaisePropertyChanged();
            }
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().UpdateItem();
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().TotalGold += _selectedEquipment.EquipmentPrice;
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().TotalStrength += _selectedEquipment.EquipmentStrength;
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().TotalAgility += _selectedEquipment.EquipmentAgility;
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().TotalIntel += _selectedEquipment.EquipmentIntel;
        }

        private void setHead()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Head")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        private void setShoulder()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Shoulders")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        private void setChest()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Chest")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        private void setBelt()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Belt")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        private void setLegs()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Legs")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        private void setBoots()
        {
            Equipment.Clear();
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());
                context.Equipments.Where(s => s.Category.Equals("Boots")).ToList().Where(s => !s.Ninjas.Contains(SelectedNinja.ToModel())).ToList().ForEach(s => Equipment.Add(new EquipmentVM(s)));
            }
        }

        #endregion

        #region Public voids close windows
        public void CloseAddItem()
        {
            if(_addItemWindow != null)
            {
                _addItemWindow.Close();
            }
        }

        public void CloseEditItem()
        {
            if (_editItemWindow != null)
            {
                _editItemWindow.Close();
            }
        }

        #endregion
    }
}
