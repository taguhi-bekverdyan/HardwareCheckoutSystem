using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IPersonService
    {
        Task Insert(Person person);
        Task<List<Person>> FindAll();
        Task<Person> FindOne(int personId);

    }
}
