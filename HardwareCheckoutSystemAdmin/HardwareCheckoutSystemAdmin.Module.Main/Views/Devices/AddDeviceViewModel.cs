using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using HardwareCheckoutSystemAdmin.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
    public class AddDeviceViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _ieventaggregator;
        private readonly IDeviceService _ideviceservice;
        private readonly IBrandService _ibrandservice;
        private readonly ICategoryService _icategoryservice;
        private readonly IShellService _ishellservice;
        private ViewMode mode;
        private Device device;

        public AddDeviceViewModel(IEventAggregator eventaggregator, IDeviceService deviceservice, IShellService shellservice, IBrandService brandservice, ICategoryService categoryService)
        {
            _ieventaggregator = eventaggregator;
            _ideviceservice = deviceservice;
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            _icategoryservice = categoryService;
            Brands = new List<Brand>();
            Categories = new List<Category>();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private Device _newdevice;
        public Device NewDevice
        {
            get { return _newdevice; }

            set { SetProperty(ref _newdevice, value); }
        }

        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get { return _brands; }

            set { SetProperty(ref _brands, value); }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }

            set { SetProperty(ref _image, value); }
        }

        public async Task FindBrands()
        {
            Brands = await _ibrandservice.FindAll();
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }

            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedcategory;
        public Category SelectedCategory
        {
            get { return _selectedcategory; }

            set { SetProperty(ref _selectedcategory, value); }
        }

        public async Task FindCategories()
        {
            Categories = await _icategoryservice.FindAll();
        }

        private string _serialnumber;
        public string SerialNumber
        {
            get { return _serialnumber; }

            set { SetProperty(ref _serialnumber, value); }
        }



        private string _model;
        public string Model
        {
            get { return _model; }

            set { SetProperty(ref _model, value); }
        }

        private string _describtion;
        public string Describtion
        {
            get { return _describtion; }

            set { SetProperty(ref _describtion, value); }
        }

        private Permission _permission;
        public Permission Permission
        {
            get { return _permission; }

            set { SetProperty(ref _permission, value); }
        }

        private DateTime _maxperiod;
        public DateTime MaxPeriod
        {
            get { return _maxperiod; }

            set { SetProperty(ref _maxperiod, value); }
        }

        private DeviceStatus _status;
        public DeviceStatus Status
        {
            get { return _status; }

            set { SetProperty(ref _status, value); }
        }

        private Brand _selectedbrand;
        public Brand SelectedBrand
        {
            get { return _selectedbrand; }

            set { SetProperty(ref _selectedbrand, value); }
        }

        private DelegateCommand _CancelAddingDeviceCommand;
        public DelegateCommand CancelAddingDeviceCommand => _CancelAddingDeviceCommand ?? (_CancelAddingDeviceCommand = new DelegateCommand(CancelAddingDeviceAction));

        public void CancelAddingDeviceAction()
        {
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Publish(new DeviceAddedOrEditedEventArgs { Device = null });
        }

        private DelegateCommand _ChooseImageCommand;
        public DelegateCommand ChooseImageCommand => _ChooseImageCommand ?? (_ChooseImageCommand = new DelegateCommand(ChooseImageAction));

        public void ChooseImageAction()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var result = dialog.ShowDialog();
            Image = File.ReadAllBytes(dialog.FileName);
        }

        private DelegateCommand _AddNewDeviceCommand;
        public DelegateCommand AddNewDeviceCommand => _AddNewDeviceCommand ?? (_AddNewDeviceCommand = new DelegateCommand(AddNewDeviceAction));

        public void AddNewDeviceAction()
        {
            device.Permission = Permission;
            device.Model = Model;
            device.SerialNumber = SerialNumber;
            device.Brand = SelectedBrand;
            device.Category = SelectedCategory;
            device.Description = Describtion;
            device.Image = Image;
            device.Status = Status;
            if (mode == ViewMode.Edit)
            {
                _ideviceservice.Update(device);
            }
            else
            {
                device.Id = new Guid();
                _ideviceservice.Insert(device);
            }
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Publish(new DeviceAddedOrEditedEventArgs { Device = device });
        }


        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await FindBrands();
            await FindCategories();
            var param = (Param<DeviceViewItem>)navigationContext.Parameters["request"];
            device = new Device(param._ViewItem);
            mode = param._ViewMode;
            if (param._ViewMode.Equals(ViewMode.Edit))
            {
                SelectedCategory = device.Category;
                SelectedBrand = device.Brand;
                Describtion = device.Description;
                Model = device.Model;
                SelectedCategory = device.Category;
                Permission = device.Permission;
            }
            else
            {
                Model = null;
                Describtion = null;
                SelectedCategory = null;
                Permission = Permission.Other;
                SelectedBrand = null;
            }
        }

        public class DeviceAddedOrEditedEvent : PubSubEvent<DeviceAddedOrEditedEventArgs> { }

        public class DeviceAddedOrEditedEventArgs
        {
            public Device Device { get; set; }
        }
    }
}
