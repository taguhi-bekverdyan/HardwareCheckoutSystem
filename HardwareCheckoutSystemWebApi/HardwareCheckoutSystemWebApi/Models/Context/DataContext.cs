using HardwareCheckoutSystemWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Context.Models
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        #region DbSets

        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}
