using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using NinjaManager.View;
using NinjaManager.ViewModel.Dataservice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class NinjaInventoryViewModel : ViewModelBase
    {

        private ShopWindow _showWindow;
        private int _totalgold;
        private int _totalstrength;
        private int _totalintel;
        private int _totalagility;
        private IDataService _service;

        public ICommand ShowShopCommand { get; set; }
        public ICommand SellHeadCommand { get; set; }
        public ICommand SellShouldersCommand { get; set; }
        public ICommand SellChestCommand { get; set; }
        public ICommand SellBeltCommand { get; set; }
        public ICommand SellLegsCommand { get; set; }
        public ICommand SellBootsCommand { get; set; }

        public NinjaVM SelectedNinja
        {
            get { return _service.SelectedNinja; }
        }

        #region Properties totalstats
        public int TotalGold
        {
            get
            {
                return _totalgold;
            }
            set
            {
                _totalgold = value;
                RaisePropertyChanged();
            }
        }

        public int TotalStrength
        {
            get
            {
                return _totalstrength;
            }
            set
            {
                _totalstrength = value;
                RaisePropertyChanged();
            }
        }

        public int TotalAgility
        {
            get
            {
                return _totalagility;
            }
            set
            {
                _totalagility = value;
                RaisePropertyChanged();
            }
        }

        public int TotalIntel
        {
            get
            {
                return _totalintel;
            }
            set
            {
                _totalintel = value;
                RaisePropertyChanged();
            }
        }

        public void UpdateStats()
        {
            TotalGold = TotalStrength = TotalAgility = TotalIntel = 0;

            SelectedNinja.Equipment.ToList().ForEach(e => TotalGold += e.EquipmentPrice);
            SelectedNinja.Equipment.ToList().ForEach(e => TotalStrength += e.EquipmentStrength);
            SelectedNinja.Equipment.ToList().ForEach(e => TotalAgility += e.EquipmentAgility);
            SelectedNinja.Equipment.ToList().ForEach(e => TotalIntel += e.EquipmentIntel);
        }
        #endregion

        #region Equipment ViewItem 

        private EquipmentVM _headitem;
        private EquipmentVM _shoulderItem;
        private EquipmentVM _chestItem;
        private EquipmentVM _beltItem;
        private EquipmentVM _legItem;
        private EquipmentVM _bootsItem;

        public EquipmentVM HeadItem
        {
            get { return _headitem; }
            set
            {
                _headitem = value;
                RaisePropertyChanged();
            }
        }

        public EquipmentVM ShoulderItem
        {
            get { return _shoulderItem; }
            set
            {
                _shoulderItem = value;
                RaisePropertyChanged();
            }
        }

        public EquipmentVM ChestItem
        {
            get { return _chestItem; }
            set
            {
                _chestItem = value;
                RaisePropertyChanged();
            }
        }

        public EquipmentVM BeltItem
        {
            get { return _beltItem; }
            set
            {
                _beltItem = value;
                RaisePropertyChanged();
            }
        }

        public EquipmentVM LegsItem
        {
            get { return _legItem; }
            set
            {
                _legItem = value;
                RaisePropertyChanged();
            }
        }

        public EquipmentVM BootsItem
        {
            get { return _bootsItem; }
            set
            {
                _bootsItem = value;
                RaisePropertyChanged();
            }
        }

        public void UpdateItem()
        {
            HeadItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Head")).FirstOrDefault();
            ShoulderItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Shoulders")).FirstOrDefault();
            ChestItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Chest")).FirstOrDefault();
            BeltItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Belt")).FirstOrDefault();
            LegsItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Legs")).FirstOrDefault();
            BootsItem = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Boots")).FirstOrDefault();
        }

        #endregion

        public NinjaInventoryViewModel(IDataService service)
        {
            _service = service;

            ShowShopCommand = new RelayCommand(ShowShop);
            SellHeadCommand = new RelayCommand(SellHead, CanSellHead);
            SellShouldersCommand = new RelayCommand(SellShoulders, CanSellShoulders);
            SellChestCommand = new RelayCommand(SellChest, CanSellChest);
            SellBeltCommand = new RelayCommand(SellBelt, CanSellBelt);
            SellLegsCommand = new RelayCommand(SellLegs, CanSellLegs);
            SellBootsCommand = new RelayCommand(SellBoots, CanSellBoots);

            UpdateItem();
            UpdateStats();
        }

        #region Private voids for SellItem
        private void SellHead()
        {
            sellItem("Head");
        }

        private void SellShoulders()
        {
            sellItem("Shoulders");
        }

        private void SellChest()
        {
            sellItem("Chest");
        }

        private void SellBelt()
        {
            sellItem("Belt");
        }

        private void SellLegs()
        {
            sellItem("Legs");
        }

        private void SellBoots()
        {
            sellItem("Boots");
        }

        private void sellItem(String category)
        {
            using (var context = new MyEntities())
            {
                context.Ninjas.Attach(SelectedNinja.ToModel());

                var temps = SelectedNinja.Equipment.Where(s => s.EquipmentCategory.Trim().Equals(category)).ToList();
                if (temps.Count > 0)
                {
                    var temp = temps.First();
                    SelectedNinja.GoldAmount += temp.EquipmentPrice;
                    TotalGold -= temp.EquipmentPrice;
                    TotalStrength -= temp.EquipmentStrength;
                    TotalAgility -= temp.EquipmentAgility;
                    TotalIntel -= temp.EquipmentIntel;
                    SelectedNinja.Equipment.Remove(temp);

                    SelectedNinja.ToModel().Equipments.Remove(temp.ToModel());
                    context.SaveChanges();
                }
            }
            RaisePropertyChanged();
            UpdateItem();
        }

        private bool CanSellHead()
        {
            return SelectedNinja?.Equipment.Where(s => s.EquipmentCategory.Trim().Equals("Head")).ToList().Count() > 0 || true;
        }

        private bool CanSellShoulders()
        {
            return SelectedNinja.Equipment?.Where(s => s.EquipmentCategory.Trim().Equals("Shoulders")).ToList().Count() > 0 || true; ;
        }

        private bool CanSellChest()
        {
            return SelectedNinja.Equipment?.Where(s => s.EquipmentCategory.Trim().Equals("Chest")).ToList().Count() > 0 || true; ;

        }

        private bool CanSellBelt()
        {
            return SelectedNinja.Equipment?.Where(s => s.EquipmentCategory.Trim().Equals("Belt")).ToList().Count() > 0 || true; ;

        }
        private bool CanSellLegs()
        {
            return SelectedNinja.Equipment?.Where(s => s.EquipmentCategory.Trim().Equals("Legs")).ToList().Count() > 0 || true; ;

        }
        private bool CanSellBoots()
        {
            return SelectedNinja.Equipment?.Where(s => s.EquipmentCategory.Trim().Equals("Boots")).ToList().Count() > 0 || true; ;

        }
        #endregion

        #region voids close windows
        private void ShowShop()
        {
            CloseShop();
            _showWindow = new ShopWindow();          
            _showWindow.Show();
        }

        public void CloseShop()
        {
            if (_showWindow != null)
            {
                _showWindow.Close();
            }
        }
        #endregion

    }
}
