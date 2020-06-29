using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using App.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace App.API
{
    class Controller<T> where T : class
    {
        private GenericRepository<T> _repo;
        public Controller(MyDBContext db)
        {
            this._repo = new GenericRepository <T>(db);
        }
        public void Add(T o)
        {
            _repo.Insert(o);
        }
        public T Find(T o)
        {
            return _repo.GetById(o);
        }
        public List<T> Where(string condition)
        {
            return _repo.Where(condition);
        }

    }
}
