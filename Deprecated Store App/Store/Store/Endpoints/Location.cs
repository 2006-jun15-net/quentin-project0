using System.Collections.Generic;

namespace StoreApp
{
    public class Location: FindOne<Order>
    {
        /// <summary>
        /// Checks inventory based on DTO.Order.Location
        /// </summary>
        /// <param>DTO.Order order, DBO DB</param>
        static public bool CheckInventory(DTO.Order o, DBO DB)
        {
            //Find the store/check inventory levels
            DTO.Location L = DB.Find(new DTO.Location() { name = o.Location });
            //Create new Inventory object to swap with our previous inventory
            DTO.Inventory I = new DTO.Inventory();
            //Go through the keys on our inventory for order and location to make sure proper qty
            foreach (var entry in o.Items.Items.Keys)
            {
                var qty = L.Inventory.Items[entry] - o.Items.Items[entry];
                if (qty >= 0) {
                    I.Items[entry] = L.Inventory.Items[entry] - o.Items.Items[entry];
                }
                else if(qty < 0)
                {
                    break;
                }
            }
            if(I.Items.Keys.Count == L.Inventory.Items.Keys.Count)
            {
                //Swap new inventory for old
                L.Inventory = I;
                return true;
            }
            throw(UnavailableResourcesException);
        }
    }


    }
}