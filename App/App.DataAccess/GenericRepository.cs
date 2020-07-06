using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace App.DataAccess
{
    public class GenericRepository<T>  where T : class
    {
        private static readonly DbContextOptions<MyDBContext> Options = new DbContextOptionsBuilder<MyDBContext>()
            .UseSqlServer(App.DataAccess.SQLConfig.ConnectionString)
            .Options;

        private DbContext _context = null;
        private DbSet<T> table = null;
        public DbContext DB => _context;
        /*
        <summary>
        Create a db connection of Type T
        </summary>
        */
        public GenericRepository()
        {
            this._context = new MyDBContext(Options);
            table = _context.Set<T>();
        }
        /*
        <summary>
        Create a db connection of Type T, given a context
        </summary>
        */
        public GenericRepository(DbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        /*
        <summary>
       Return current table of T to list
        </summary>
        */
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        /*
        <summary>
       Return record given ID
        </summary>
        */
        public T GetById(object id)
        {
            return table.Find(id);
        }
        /*
        <summary>
        Insert a record
        </summary>
        */
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public List<T> Where(Expression<Func<T, bool>> f)
        {
            return table.Where(f).ToList();
        }
        public IQueryable<T> Where(string f)
        {
            return table.Where(f);
        }
        public List<T> Include(string s)
        {
            return table.Include(s).ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
