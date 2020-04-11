using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using NinjaManager.Domain.NinjaManager.Domain;

namespace NinjaManager.ViewModel
{
    public class NinjaVM : ViewModelBase
    {
        private Ninja _ninja;

        public ObservableCollection<EquipmentVM> Equipment { get; set; }

        public NinjaVM(Ninja ninja)
        {
            this._ninja = ninja;
            var EquipmentList = _ninja.Equipments.Select(s => new EquipmentVM(s, this));
            Equipment = new ObservableCollection<EquipmentVM>(EquipmentList);
        }

        public NinjaVM()
        {
            this._ninja = new Ninja();
            Equipment = new ObservableCollection<EquipmentVM>();
        }

        public int NinjaId
        {
            get { return _ninja.Id; }
            set { _ninja.Id = value; }
        }
        public string Name
        {
            get { return _ninja.Name?.Trim(); }
            set
            {
                _ninja.Name = value;
                base.RaisePropertyChanged();
            }
        }
        public int GoldAmount
        {
            get { return _ninja.Gold_amount; }
            set
            {
                _ninja.Gold_amount = value;
                base.RaisePropertyChanged();
            }
        }
        internal Ninja ToModel()
        {
            return _ninja;
        }
    }
}
