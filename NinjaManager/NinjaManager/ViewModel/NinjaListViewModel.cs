using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.Domain.NinjaManager.Domain;
using NinjaManager.View;
using NinjaManager.ViewModel.Dataservice;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{

    public class NinjaListViewModel : ViewModelBase
    {
        private IDataService _service;

        public AddNinjaWindow _addNinjaWindow;
        public NinjaInventoryWindow _ninjaInventoryWindow;
        public EditNinjaWindow _editNinjaWindow;
        public ObservableCollection<NinjaVM> Ninjas { get; set; }

        public ICommand ShowAddNinjaCommand { get; set; }
        public ICommand DeleteNinjaCommand { get; set; }
        public ICommand EditNinjaCommand { get; set; }
        public ICommand ShowNinjaInventoryCommand { get; set; }

        public NinjaVM SelectedNinja
        {
            get { return _service.SelectedNinja; }
            set
            {
                _service.SelectedNinja = value;
                RaisePropertyChanged();
            }
        }

        public NinjaListViewModel(IDataService service)
        {
            _service = service;
            using (var context = new MyEntities())
            {
                var ninjas = context.Ninjas.ToList()
                             .Select(ninja => new NinjaVM(ninja));

                Ninjas = new ObservableCollection<NinjaVM>(ninjas);
            }

            ShowAddNinjaCommand = new RelayCommand(ShowAddNinja);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);
            ShowNinjaInventoryCommand = new RelayCommand(ShowInventoryNinja);
            EditNinjaCommand = new RelayCommand(EditNinja);
        }

        #region Private voids ICommands    
        private void EditNinja()
        {
            CloseEditNinja();
            _editNinjaWindow = new EditNinjaWindow();
            _editNinjaWindow.Show();
        }

        private void ShowAddNinja()
        {
            CloseAddNinja();
            _addNinjaWindow = new AddNinjaWindow();
            _addNinjaWindow.Show();
        }

        private void ShowInventoryNinja()
        {
            if(_ninjaInventoryWindow != null)
            {
                _ninjaInventoryWindow.Close();
            }
            _ninjaInventoryWindow = new NinjaInventoryWindow();
            _ninjaInventoryWindow.Show();
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().UpdateItem();
            ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().UpdateStats();
        }

        private void DeleteNinja()
        {
            using (var context = new MyEntities())
            {
                if (_ninjaInventoryWindow != null)
                {
                    _ninjaInventoryWindow.Close();
                }              
                ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>().CloseShop();
                context.Entry(SelectedNinja.ToModel()).State = EntityState.Deleted;
                context.SaveChanges();
            }
            Ninjas.Remove(SelectedNinja);
        }
        #endregion

        #region Public voids close windows
        public void CloseAddNinja()
        {
            if (_addNinjaWindow != null)
            {
                _addNinjaWindow.Close();
            }
        }

        public void CloseEditNinja()
        {
            if (_editNinjaWindow != null)
            {
                _editNinjaWindow.Close();
            }
        }
        #endregion

    }
}