using System.Collections.Generic;
using System;
using System.Linq.Dynamic;
using System.Linq;
using DTO;
namespace StoreApp
{
    public class Order : Insert<DTO.Order>, FindOne<Order>, FindAll<Order>
    {        
        public Response Insert(DTO.Order order)
        {
            if (Location.CheckInventory(order)) {
                DTO.Data.Order.Add(order);
                return new DTO.Response()
                {
                    message = $"Successful Order Insert",
                    status = true
                };
            }
            return new DTO.Response()
            {
                message = $"Unable to Insert Order",
                status = false
            };
        }
        public Response FindOne(string id)
        {
            if (Int32.TryParse(id, out int j))
            {
                DTO.Order result = DTO.Data.Order.Find(
                delegate (DTO.Order o)
                {
                    return o.Id == j;
                });
                if(result != null)
                {

                }
                return result;
            }
            else
                Console.WriteLine("Invalid Number Provided");
            
        }
        public Response FindAll(string field, string match)
        {
            IEnumerable<DTO.Order> query = DTO.Data.Order.AsQueryable().Where(field + " == @0", match);
            return query.ToList();
        }
    }
}