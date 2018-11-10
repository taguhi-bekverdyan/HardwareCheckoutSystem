using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses
{
    public class Param<T>
    {
        public ViewMode _ViewMode { get; set; }
        public T _ViewItem { get; set; }
        public Param(ViewMode viewmode, T viewitem)
        {
            _ViewMode = viewmode;
            _ViewItem = viewitem;
        }

    }
}

