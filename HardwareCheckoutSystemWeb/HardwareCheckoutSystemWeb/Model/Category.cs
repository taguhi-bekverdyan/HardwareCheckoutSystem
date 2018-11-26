using System;
using System.Collections.Generic;

namespace HardwareCheckoutSystemWeb.Model
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public Category()
        {
            Id = Guid.NewGuid();
        }


    }
}