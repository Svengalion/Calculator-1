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
    public  class HistoryRep : IHistoryRep
    {
        public IQueryable<History> Histories
        {
            get
            {
                using (var context = new DatabaseContext())
                {
                    return context.Histories;
                }
            }
        }

        public Task DeleteAsync(History item) => Task.Run(() =>
         {
             using (var context = new DatabaseContext())
             {
                 if (context.Histories.Contains(item))
                 {
                     context.Histories.Remove(item);
                     context.SaveChanges();
                 }
             }
         });

        public async Task<History> GetItemByIdAsync(Guid id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Histories.FindAsync(id);
            }
        }

        public async Task UpdateAsync(History item)
        {
            using (var context = new DatabaseContext())
            {
                var old= await context.Histories.FindAsync(item.Id);
                if(old!=null)
                {
                    context.Histories.Remove(old);
                }
                context.Histories.Add(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
