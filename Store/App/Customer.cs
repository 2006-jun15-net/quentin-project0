namespace StoreApp
{
    public class Customer: FindOne<DTO.Customer>, Insert<DTO.Customer>
    { 
     public DTO.Response Insert(DTO.Customer c)
    {
        DTO.Data.Customer.Add(c);
        return new DTO.Response()
            {
                message = $"Successfully inserted {c.Name}",
                status = true
            };
        }
    public DTO.Response FindOne(string name)
    {
        DTO.Customer result = DTO.Data.Customer.Find(
        delegate (DTO.Customer c)
        {
            return c.Name == name;
        });
            if (result != null)
            {
                return new DTO.Response()
                {
                    message = $"{result.Name}",
                    status = true
                };
            }
            return new DTO.Response()
            {
                message = $"No Customer Named {name}",
                status = true
            };
        }
    
}
}