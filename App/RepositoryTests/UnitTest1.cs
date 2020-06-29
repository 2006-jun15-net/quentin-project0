using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using App.DataAccess.Entities;
using App.DataAccess;
using App.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace RepositoryTests{
    

    [TestClass]
    public class RepositoryTests
    {
        public static readonly DbContextOptions<MyDBContext> Options = new DbContextOptionsBuilder<MyDBContext>()
            .UseSqlServer(App.DataAccess.SQLConfig.ConnectionString)
            .Options;

        [TestMethod]
        public void InitGenericRepository()
        {
            var ctx = new MyDBContext(Options);
            var Repo = new GenericRepository<Location>(ctx);
        }
        [TestMethod]
        public void InsertSomeData()
        {
            var ctx = new MyDBContext(Options);
            var Repo = new GenericRepository<Location>(ctx);
            Repo.Insert(new Location() { Name = "New York"});
            ctx.SaveChanges();
        }
        [TestMethod]
        public void GetAllData()
        {
            var ctx = new MyDBContext(Options);
            var Repo = new GenericRepository<Location>(ctx);
            Console.WriteLine(Repo.GetAll());
            
        }
    }
    [TestClass]
    public class APITests
    {
        public void APIAdd()
        {
            var payload = new Location() { Name = "LA" };
            var a = new App.API.API();
            a.Add<Location>(payload);


        }
    }
}
