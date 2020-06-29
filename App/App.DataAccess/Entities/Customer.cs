using System;
using System.Collections.Generic;

namespace App.DataAccess.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DefaultLocationId { get; set; }

        public virtual Location DefaultLocation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
