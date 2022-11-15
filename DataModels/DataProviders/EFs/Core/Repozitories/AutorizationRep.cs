using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataModels.Repozitories;
using System.Threading.Tasks;
using DataModels.Entities;
using System.Linq;

namespace DataModels.DataProviders.EFs.Core.Repozitories
{
    public class AuthorizationRep : IAuthorizationRep
    {
        public Task DeleteAsync(Authorization item) => Task.Run(() =>
        {
            using (var context = new DatabaseContext())
            {
                if (context.Authorizations.Contains(item))
                {
                    context.Authorizations.Remove(item);
                    context.SaveChanges();
                }
            }
        });

        public async Task<Authorization> GetItemByIdAsync(Guid id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Authorizations.FindAsync(id);
            }
        }

        public async Task UpdateAsync(Authorization item)
        {
            using (var context = new DatabaseContext())
            {
                var old = await context.Authorizations.FindAsync(item.UserId);
                if (old != null)
                {
                    context.Authorizations.Remove(old);
                }
                context.Authorizations.Add(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
