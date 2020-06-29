using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
/*
 * 
 * Yanked from https://dotnettutorials.net/lesson/generic-repository-pattern-csharp-mvc/
 * 
 */
namespace App.DataAccess
{
    public class GenericRepository<T>  where T : class
    {
        private DbContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new MyDBContext();
            table = _context.Set<T>();
        }
        public GenericRepository(DbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
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
        public List<T> Where(string f)
        {
            return table.Where(f).ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
