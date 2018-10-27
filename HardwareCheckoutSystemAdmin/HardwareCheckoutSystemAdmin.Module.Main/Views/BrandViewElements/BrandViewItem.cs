using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements
{
    class BrandViewItem : BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private Guid _id;

        public BrandViewItem()
        {
            _id = Guid.NewGuid();
        }

        public BrandViewItem(Brand brand)
        {
            Name = brand.Name;
            _id = brand.Id;
        }
        

        public Guid GetId()
        {
            return _id;
        }

    }
}
