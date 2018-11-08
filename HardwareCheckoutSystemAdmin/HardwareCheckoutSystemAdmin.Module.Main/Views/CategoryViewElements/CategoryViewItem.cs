using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.CategoryViewElements
{
    class CategoryViewItem : BindableBase
    {
        private Guid _id;
        public string Name { get; set; }

        public CategoryViewItem(Category c)
        {
            _id = c.Id;
            Name = c.Name;
        }

        public CategoryViewItem()
        {
            _id =Guid.NewGuid();
            Name = string.Empty;
        }
        public Guid GetId()
        {
            return _id;
        }
    }
}
