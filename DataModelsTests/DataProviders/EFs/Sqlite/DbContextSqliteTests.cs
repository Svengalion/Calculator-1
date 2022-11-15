using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataModels.DataProviders.EFs.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataModels.DataProviders.EFs.Sqlite.Tests
{
    [TestClass()]
    public class DbContextSqliteTests
    {
        [TestMethod()]
        public void SqliteConnectionTest()
        {
            DbContextSqlite context = new DbContextSqlite();
            Assert.IsTrue(File.Exists(@"C:\Users\MordovetsTA\source\repos\Calculator\Data\Sqlite.db"));
        }
    }
}