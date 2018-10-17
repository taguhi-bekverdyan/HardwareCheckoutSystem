﻿using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IBrandService
    {
        Task Insert(Brand brand);
        Task Update(Brand brand);
        Task Delete(Brand brand);
        Task<Brand> FindBrandByIdAsync(Guid id);
        Task<List<Brand>> FindAll();
    }
}
