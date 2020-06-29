using System;
using System.Collections.Generic;

namespace App.DataAccess.Entities
{
    public partial class Inventory
    {
        public int Qty { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
