using System;
using App.DataAccess.Entities;
using App.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Dynamic;
namespace App.API
{
    public class API
    {
        private static readonly DbContextOptions<MyDBContext> Options = new DbContextOptionsBuilder<MyDBContext>()
            .UseSqlServer(App.DataAccess.SQLConfig.ConnectionString)
            .Options;
        public API()
        {
        }
        public void Add<T>(T payload) where T : class
        {
          var ctx = new MyDBContext();
          var controls = new Controller<T>(ctx);
          controls.Add(payload);
        }
        public T Find<T>(T payload) where T : class
        {
            var ctx = new MyDBContext();
            var controls = new Controller<T>(ctx);
            return controls.Find(payload);
        }

        public List<T> Search<T>(string s) where T : class
        {
            var ctx = new MyDBContext();
            var controls = new Controller<T>(ctx);
            return controls.Where(s);
        }
    }
}
