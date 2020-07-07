using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using App.DataAccess.Entities;
using App.DataAccess;
using App.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using App.ConsoleUI;
using Newtonsoft.Json.Linq;
using System.Linq;
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
        public void GenericRepositoryInsertSomeData()
        {
            var ctx = new MyDBContext(Options);
            var Repo = new GenericRepository<Location>(ctx);
            Repo.Insert(new Location() { Name = "New York"});
            ctx.SaveChanges();
        }
        [TestMethod]
        public void GenericRepositoryGetAllData()
        {
            var ctx = new MyDBContext(Options);
            var Repo = new GenericRepository<Location>(ctx);
            Console.WriteLine(Repo.GetAll());
            
        }
    }
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void ControllerAddPayload()
        {
            var payload = new Customer() { FirstName = "Danny", LastName="Jones"};
            var CustomerControls = new CustomerController();
            CustomerControls.Add(payload);
        }
        [TestMethod]
        public void ControllerConvertJObject()
        {
            var payload = new JObject();
            payload["FirstName"] = "Bill";
            payload["LastName"] = "Clinton";
            var CustomerControls = new CustomerController();
            var conversion = CustomerControls.Format(payload);
            Assert.IsInstanceOfType(conversion, typeof(Customer));
        }
        [TestMethod]
        public void ControllerConvertJObjectWithIntegers()
        {
            var payload = new JObject();
            payload["CustomerId"] = "1";
            payload["LocationId"] = "15";
            payload["Date"] = DateTime.Now;
            payload["Qty"] = "22";
            payload["OrderId"] = "200000";
            payload["ProductId"] = "10";
            var CustomerControls = new OrderController();
            var conversion = CustomerControls.Format(payload);
            Assert.IsInstanceOfType(conversion,typeof(Order));
        }
        [TestMethod]
        public void ControllerSearchForString()
        {
            var CustomerControls = new CustomerController();
            var conversion = CustomerControls.Where($"FirstName == \"{"Dew"}\" && LastName == \"{"Daw"}\"").ToList();
            Assert.IsInstanceOfType(conversion[0], typeof(Customer));
        }
        [TestMethod]
        public void OrderControllerGetMax()
        {
            var OrderControls = new OrderController();
            var conversion = OrderControls.max();
            Assert.IsInstanceOfType(conversion, typeof(int));
        }
        //QTY: 146 ProductId:4 LocationId:70
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderControllerFailedInventoryInsert()
        {
            var OrderControls = new OrderController();
            var order = new Order()
            {
                Qty = 10000,
                ProductId = 4,
                LocationId = 70,
                CustomerId = 231
            };
            OrderControls.Add(order);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FunctionParserFailedInput()
        {
            var fn = new Functions();
            fn.Parse("Jibberish");
        }
    }
}
