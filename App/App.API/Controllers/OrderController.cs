using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using App.DataAccess;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace App.API.Controllers
{
    public class OrderController: Controller<Order>
    {
        public OrderController() : base()
        {

        }
        /*
        <summary>
        Get the max id from a table to apply to multiple orders
        </summary>
        */
        public  virtual int max()
        {
            return _repo.DB.Set<Order>().Select(o => o.OrderId).Max();
        }
        /*
        <summary>
        Add an Order. Checks inventory levels and updates accordingly
        </summary>
        */
        public virtual void Add(Order o)
        {
            o.Date = DateTime.Now;
            var I_Repo = _repo.DB.Set<Inventory>();
            var I = I_Repo
                .Where(x => x.ProductId == o.ProductId && x.LocationId == o.LocationId)
                .FirstOrDefault();
            if(I == null)
            {
                throw new InvalidOperationException($"LocationID:{o.LocationId} is currently out of stock of ProductID:{o.ProductId}");
            };
            if (I.Qty >= o.Qty)
            {
                I.Qty -= o.Qty;
                I_Repo.Update(I);
                _repo.DB.Set<Order>().Add(o);
                _repo.Save();
            }
            else throw new ArgumentException($"Value is too great for current Location. Location Inventory on hand is:{I.Qty}") ;
            
        }
    }
}
