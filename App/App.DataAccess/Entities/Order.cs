using System;
using System.Collections.Generic;

namespace App.DataAccess.Entities
{
    public partial class Order
    {
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public int Qty { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
