using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HCSWebApi.Models.Context
{

        public class DataContext : DbContext
        {


            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }


            public DbSet<User> Users { get; set; }
            public DbSet<Device> Devices { get; set; }
            public DbSet<Response> Responses { get; set; }
            public DbSet<Request> Requests { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<Category> Categories { get; set; }

        }
}
