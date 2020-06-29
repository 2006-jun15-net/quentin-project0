using System;
using System.Collections.Generic;

namespace App.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            Customer = new HashSet<Customer>();
            Inventory = new HashSet<Inventory>();
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
