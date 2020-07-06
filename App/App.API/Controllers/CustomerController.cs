using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using App.DataAccess;
using Newtonsoft.Json.Linq;
namespace App.API.Controllers
{
    public class CustomerController : Controller<Customer>
    {
        public CustomerController() : base() { }
        public virtual Customer Add(Customer o)
        {
            _repo.Insert(o);
            _repo.Save();
            return o;
        }
    }
}