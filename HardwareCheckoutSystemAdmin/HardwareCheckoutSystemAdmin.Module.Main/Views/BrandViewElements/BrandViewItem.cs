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
        private Guid _id;
        public string Name { get; set; }

        public BrandViewItem(Brand b)
        {
            Name = b.Name;
            _id = b.Id;
        }

        public BrandViewItem()
        {
            _id = Guid.NewGuid();
            Name = string.Empty;
        }

        public Guid GetId()
        {
            return _id;
        }

        public static explicit operator Brand(BrandViewItem item)
        {
            Brand brand = new Brand();
            brand.Id = item._id;
            brand.Name = item.Name;
            return brand;
        }

    }
}
