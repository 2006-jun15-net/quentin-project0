/// <summary>
/// Data Base Object
/// Instantiable instance of a Data Transfer Object
/// Holds the DTO in memory and add some fancy operations 
/// like indexing, ids etc to the static DTO
/// </summary>
/// <param>DTO.DTO</param>
using DTO;
using System.Collections.Generic;
using System.Linq;
public class DBO
{
    private DTO.Data data;
    private Dictionary<string, int> Ids;

    /// <summary>
    /// Generic Add Function, this overload adds an order to the orders section
    /// </summary>
    /// <param>DTO.Order</param>
    public Add(DTO.Order o) {
        o.id = ++this.Ids["Order"];
        this.data.Order.Add(o);
    }
    /// <summary>
    /// Generic Add Function, this overload adds a customer to the customers section
    /// </summary>
    /// <param>DTO.Customer</param>
    public Add(DTO.Customer c) {
        c.id = ++this.Ids["Customer"];
        this.data.Customer.Add(c);
    }
    /// <summary>
    /// Generic Find Function, this overload finds a function from a DTO.Customer if has name field
    /// </summary>
    /// <param>DTO.Customer</param>
    public DTO.Customer Find(DTO.Customer c)
    {
        this.data.Customer.Select(C => c.Name == C.Name);
    }
    /// <summary>
    /// Generic Find Function, Find order or order history based on the DTO.Order field provided
    /// </summary>
    /// <param>DTO.Order</param>
    public List<DTO.Order> Find(DTO.Order o)
    {
        var search;
        if (o.id != null) search = x => x.id == o.id;
        if (o.Location != null) search = x => x.Location == o.Location;
        if (o.Customer != null) search = x => x.Customer == o.Customer;
        return this.data.Order.Select(search);    
    }
    /// <summary>
    /// Generic Find Function, Find Location for filling orders
    /// </summary>
    /// <param>DTO.Location</param>
    public List<DTO.Location> Find(DTO.Location l)
    {
        this.data.Location.Select(L => L.Name == l.name);
    }
    /// <summary>
    /// DBO Constructor, creates dictionary of ids.
    /// </summary>
    /// <param>DTO.Location</param>
    DBO(DTO.DTO data)
    {
        this.data = data.data;
        this.Ids["Customer"] = this.data.Customer.Select(o => o.id).Sort().Last();
        this.Ids["Location"] = this.data.Location.Select(o => o.id).Sort().Last();
        this.Ids["Product"] = this.data.Product.Select(o => o.id).Sort().Last();
        this.Ids["Order"] = this.data.Order.Select(o => o.id).Sort().Last();
    }
}