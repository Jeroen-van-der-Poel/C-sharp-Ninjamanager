using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class AddItemViewModel : ViewModelBase
    {
        private ShopListViewModel _items;

        public EquipmentVM Item { get; set; }

        public ICommand AddItemCommand { get; set; }

        public AddItemViewModel(ShopListViewModel items)
        {
            this._items = items;
            this.Item = new EquipmentVM();
            AddItemCommand = new RelayCommand(AddItem, CanAddItem);

            Item.EquipmentId = GetNextId();

        }

        private void AddItem()
        {
            if (Regex.IsMatch(Item.EquipmentName, @"^^(?! )[A-Za-z\s]+$") && isCategory())
            {

                _items.Equipment.Add(Item);
                using (var context = new MyEntities())
                {
                    context.Equipments.Add(Item.ToModel());
                    context.SaveChanges();
                }
                _items.CloseAddItem();

            }
        }

        private bool isCategory()
        {
            if (Item.EquipmentCategory == "Head" || Item.EquipmentCategory == "Shoulders" || Item.EquipmentCategory == "Chest" || Item.EquipmentCategory == "Legs" || Item.EquipmentCategory == "Belt" || Item.EquipmentCategory == "Boots")
            {
                return true;
            }
            return false;
        }

        public bool CanAddItem()
        {
            return true;
        }

        public int GetNextId()
        {
            var IdOfItem = 0;
            using (var context = new MyEntities())
            {
                var temp = context.Equipments
                        .Select(s => s.Id).Max();
                IdOfItem = temp;
            }
            return IdOfItem + 1;
        }
    }
}
