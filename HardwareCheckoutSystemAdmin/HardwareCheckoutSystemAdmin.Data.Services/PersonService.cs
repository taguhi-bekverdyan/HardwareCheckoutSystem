using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class PersonService : IPersonService
    {
        public async Task<List<Person>> FindAll()
        {
            using (var context = new DataContext())
            {
                return await context.Persons.ToListAsync();
            }
        }
    }
}
