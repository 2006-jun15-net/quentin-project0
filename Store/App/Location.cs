using System.Collections.Generic;

namespace StoreApp
{
    public class Location
    {
    static public bool  CheckInventory(DTO.Order o)
        {
            //Find the store/check inventory levels
            DTO.Location L = DTO.Data.Location.Find(
                delegate (DTO.Location l)
                {
                    return l.Name == o.Location;
                });
            //Create new Inventory object to swap with our previous inventory
            DTO.Inventory I = new DTO.Inventory();
            //Go through the keys on our inventory for order and location to make sure proper qty
            foreach (var entry in o.Items.Items.Keys)
            {
                var qty = L.Inventory.Items[entry] - o.Items.Items[entry];
                if (qty >= 0? ) {
                    I.Items[entry] = L.Inventory.Items[entry] - o.Items.Items[entry];
                }
                else if(qty < 0)
                {
                    break;
                }
            }
            if(I.Items.Keys.Count == L.Inventory.Items.Keys.Count)
            {
                //Swap our new inventory for our old
                L.Inventory = I;
                return true;
            }
            return false;
        }
    }


    }
}