﻿using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class RequestService : IRequestService
    {
        #region [CREATE]
        public async Task Insert(Request request)
        {
            using (var context = new DataContext())
            {
                context.Requests.Add(request);
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region [READ]
        public async Task<List<Request>> FindAll()
        {
            using (var context = new DataContext())
            {
                return await context.Requests.ToListAsync();
            }
        }

        public async Task<Request> FindById(Guid requestId)
        {
            using (var context = new DataContext())
            {
                return await context.Requests.FirstOrDefaultAsync(d => d.Id == requestId);
            }
        }
        #endregion

        #region [UPDATE]
        public async Task Update(Request request)
        {
            using (var context = new DataContext())
            {
                var deviceToUpdate = (from d in context.Requests
                                      where d.Id == request.Id
                                      select d).FirstOrDefault();
                deviceToUpdate = request;
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region [DELETE]
        public async Task Delete(Guid key)
        {
            using (var context = new DataContext())
            {
                var requestToDelete = (from d in context.Requests
                                       where d.Id == key
                                       select d).FirstOrDefault();
                context.Requests.Remove(requestToDelete);
                await context.SaveChangesAsync();
            }
        }
        #endregion

    }
}
