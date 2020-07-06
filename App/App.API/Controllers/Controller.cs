using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using App.DataAccess;
using App.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace App.API.Controllers
{
    public abstract class Controller<T> where T:class
    {
        internal GenericRepository<T> _repo;
        public DbContext _db;
        /*
        <summary>
       Init a repository and expose db to public
        </summary>
        */
        public Controller()
        {
            this._repo = new GenericRepository<T>();
            this._db = _repo.DB;
        }
        /*
        <summary>
       Insert a record of type T into db, save results
        </summary>
        */
        public virtual void Add(T o)
        {
            _repo.Insert(o);
            _repo.Save();
        }
        /*
        <summary>
       Get a record by Id
        </summary>
        */
        public virtual T Find(T o)
        {
            return _repo.GetById(o);
        }
        /*
        <summary>
       Query DB by input string
        </summary>
        */
        public virtual IQueryable<T> Where(string condition)
        {
            return _repo.Where(condition);
        }
        /*
        <summary>
       Convert a JObject to type T
        </summary>
        */
        public virtual T Format(JObject o)
        {
            return o.ToObject<T>();
        }
    }
}
