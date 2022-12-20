using System;
using System.Collections.Generic;
using System.Text;
using DataModels.DataProviders.EFs.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace DataModels.DataProviders.EFs.Sqlite
{
    public class SqliteDbContext : DatabaseContext
    {
        public const string DataSource = @"C:\_Data\Sqlite.db";
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite($"Data Source = {DataSource}");
        }
    }
}
