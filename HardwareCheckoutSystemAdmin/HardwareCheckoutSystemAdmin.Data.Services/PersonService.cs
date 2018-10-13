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

        #region [CREATE]
        public async Task Insert(Person person)
        {
            using (var context = new DataContext())
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region [READ]
        public async Task<List<Person>> FindAll()
        {
            using (var context = new DataContext())
            {
                return await context.Persons.ToListAsync();
            }
        }

        public async Task<Person> FindOne(int personId)
        {
            using (var context = new DataContext())
            {
                return await context.Persons.FirstOrDefaultAsync(p=>p.Id==personId);
            }
        }
        #endregion

        #region [UPDATE]




        #endregion

        #region [DELETE]



        #endregion

    }
}
