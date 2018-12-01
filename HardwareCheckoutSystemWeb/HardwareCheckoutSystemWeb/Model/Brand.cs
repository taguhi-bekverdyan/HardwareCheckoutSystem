using System;
using System.Collections.Generic;

namespace HardwareCheckoutSystemWeb.Model
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public Brand()
        {
            Id = Guid.NewGuid();
        }

    }
}