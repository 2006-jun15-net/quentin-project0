namespace StoreApp
{
    public class Customer: FindOne<DTO.Customer>, Insert<DTO.Customer>
    {
        /// <summary>
        /// Implementation of Insert: Takes Customer Object, inserts into DBO
        /// </summary>
        /// <param>DTO.Customer c, DBO DB</param>
        public DTO.Response Insert(DTO.Customer c, DBO DB)
        {
        DBO.Add(c);
        return new DTO.Response()
            {
                message = $"Successfully inserted {c.Name}",
                status = true
            };
        }
        /// <summary>
        /// Implementation of FindOne: Takes DTO.Customer Object gets result back from DBO and Formats
        /// </summary>
        /// <param>DTO.Customer c, DBO DB</param>
        public DTO.Response FindOne(DTO.Customer c, DBO DB)
    {
            DTO.Customer result = DBO.Find(c);
            if (result != null)
            {
                return new DTO.Response()
                {
                    message = $"{result.Name}",
                    status = true
                };
            }
            throw (Exception);
        }
    
}
}