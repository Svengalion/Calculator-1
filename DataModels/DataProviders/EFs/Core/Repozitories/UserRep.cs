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
    public  class UserRep : IUserRep
    {
        public IQueryable<User> Users
        {
            get
            {
                using (var context = new DatabaseContext())
                {
                    return context.Users;
                }
            }
        }

        public Task DeleteAsync(User item) => Task.Run(() =>
         {
             using (var context = new DatabaseContext())
             {
                 if (context.Users.Contains(item))
                 {
                     context.Users.Remove(item);
                     context.SaveChanges();
                 }
             }
         });

        public async Task<User> GetItemByIdAsync(Guid id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Users.FindAsync(id);
            }
        }

        public async Task UpdateAsync(User item)
        {
            using (var context = new DatabaseContext())
            {
                var old= await context.Users.FindAsync(item.Id);
                if(old!=null)
                {
                    context.Users.Remove(old);
                }
                context.Users.Add(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
