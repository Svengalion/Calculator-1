using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataModels.Entities;
namespace DataModels.DataProviders.EFs.Core
{
    public  class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<History> Histories { get; set; }

        public DatabaseContext() => Database.EnsureCreated();
        
    }
}
