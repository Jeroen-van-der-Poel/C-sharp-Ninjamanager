using GalaSoft.MvvmLight;
using NinjaManager.Domain.NinjaManager.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class EquipmentVM : ViewModelBase
    {
        private Equipment _equipment;
        public ObservableCollection<NinjaVM> Ninja { get; set; }

        public EquipmentVM(Equipment equipment)
        {
            this._equipment = equipment;
            Ninja = new ObservableCollection<NinjaVM>();
        }

        public EquipmentVM()
        {
            this._equipment = new Equipment();
            Ninja = new ObservableCollection<NinjaVM>();
        }

        public EquipmentVM(Equipment equipment, NinjaVM ninja)
        {
            this._equipment = equipment;
            Ninja = new ObservableCollection<NinjaVM>();
            Ninja.Add(ninja);
        }

        public int EquipmentId
        {
            get { return _equipment.Id; }
            set { _equipment.Id = value; }
        }

        public string EquipmentName
        {
            get { return _equipment.Name?.Trim(); }
            set
            {
                _equipment.Name = value;
                base.RaisePropertyChanged();
            }
        }

        public int EquipmentPrice
        {
            get { return _equipment.Price; }
            set
            {
                _equipment.Price = value;
                base.RaisePropertyChanged();
            }
        }

        public string EquipmentCategory
        {
            get { return _equipment.Category; }
            set
            {
                _equipment.Category = value;
                base.RaisePropertyChanged();
            }
        }

        public int EquipmentStrength
        {
            get { return _equipment.Strength; }
            set
            {
                _equipment.Strength = value;
                base.RaisePropertyChanged();
            }
        }

        public int EquipmentIntel
        {
            get { return _equipment.Intel; }
            set
            {
                _equipment.Intel = value;
                base.RaisePropertyChanged();
            }
        }

        public int EquipmentAgility
        {
            get { return _equipment.Agility; }
            set
            {
                _equipment.Agility = value;
                base.RaisePropertyChanged();
            }
        }

        internal Equipment ToModel()
        {
            return _equipment;
        }
    }
}
