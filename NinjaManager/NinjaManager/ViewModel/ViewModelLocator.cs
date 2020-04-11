using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NinjaManager.ViewModel.Dataservice;

namespace NinjaManager.ViewModel
{

    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<NinjaListViewModel>();
            SimpleIoc.Default.Register<ShopListViewModel>();
            SimpleIoc.Default.Register<NinjaInventoryViewModel>();
            SimpleIoc.Default.Register<IDataService, DataService>();
        }

        public NinjaListViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NinjaListViewModel>();
            }
        }

        public AddNinjaViewModel AddNinja
        {
            get
            {
                return new AddNinjaViewModel(Main);
            }
        }

        public NinjaInventoryViewModel NinjaInventory => ServiceLocator.Current.GetInstance<NinjaInventoryViewModel>();

        public EditNinjaViewModel EditNinja
        {
            get
            {
                return new EditNinjaViewModel(Main.SelectedNinja, Main);
            }
        }

        public ShopListViewModel Shop => ServiceLocator.Current.GetInstance<ShopListViewModel>();


        public AddItemViewModel AddItem
        {
            get
            {
                return new AddItemViewModel(Shop);
            }
        }

        public EditItemViewModel EditItem
        {
            get
            {
                return new EditItemViewModel(Shop.SelectedEquipment, Shop);
            }
        }

        public static void Cleanup()
        {

        }
    }
}