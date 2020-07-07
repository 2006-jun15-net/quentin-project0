using System.Collections.Generic;
using System;
using System.Linq.Dynamic;
using System.Linq;
using DTO;
namespace StoreApp
{
    public class Order : Insert<DTO.Order>, FindOne<Order>, FindAll<Order>
    {   /// <summary>
        /// Implementation of Insert: Takes in an order, checks inventory of store. If all good inserts order else throws Unavailable resources
        /// </summary>
        /// <param>DTO.Order order, DBO DB</param>
        public Response Insert(DTO.Order order, DBO DB)
        {
            if (Location.CheckInventory(order)) {
                DBO.Add(order);
                return new DTO.Response()
                {
                    message = $"Successful Order Insert",
                    status = true
                };
            }
            throw (Exception);
        }
        /// <summary>
        /// Implementation of FindOne: Finds an order based on DT.Order fields. If only name:Searches by name, If only id: gets id if location: gets all orders for location
        /// </summary>
        /// <param>DTO.Order order, DBO DB</param>
        public Response FindOne(DTO.Order o, DBO DB)
        {
            //Need to format repsonse
            return DBO.Find(o);
        }
    }
}