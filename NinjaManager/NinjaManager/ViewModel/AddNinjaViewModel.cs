using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class AddNinjaViewModel : ViewModelBase
    {
        private NinjaListViewModel _ninjas;

        public NinjaVM Ninja { get; set; }

        public ICommand AddNinjaCommand { get; set; }

        public AddNinjaViewModel(NinjaListViewModel ninjas)
        {
            this._ninjas = ninjas;
            this.Ninja = new NinjaVM();
            AddNinjaCommand = new RelayCommand(AddNinja, CanAddNinja);

            Ninja.NinjaId = GetNextId();
            Ninja.GoldAmount = 5000;
        }

        private void AddNinja()
        {
            if (Regex.IsMatch(Ninja.Name, @"^^(?! )[A-Za-z\s]+$"))
            {

                _ninjas.Ninjas.Add(Ninja);

                using (var context = new MyEntities())
                {
                    context.Ninjas.Add(Ninja.ToModel());
                    context.SaveChanges();
                }
                _ninjas.CloseAddNinja();
            }

        }

        public bool CanAddNinja()
        {
            return true;
        }

        public int GetNextId()
        {
            var IdOfNinja = 0;
            using (var context = new MyEntities())
            {
                var temp = context.Ninjas
                        .Select(s => s.Id).Max();
                IdOfNinja = temp;
            }
            return IdOfNinja + 1;
        }
    }
}
