namespace DTO
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class DTO
    {
        [JsonProperty("Data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("Customer")]
        public static List<Customer> Customer { get; set; }

        [JsonProperty("Location")]
        public static List<Location> Location { get; set; }

        [JsonProperty("Product")]
        public static List<Product> Product { get; set; }

        [JsonProperty("Order")]
        public static List<Order> Order { get; set; }
    }

    public partial class Customer : AbstractData
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }
    }

    public partial class Location : AbstractData
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Inventory")]
        public Inventory Inventory { get; set; }
    }

    public partial class Inventory : AbstractData
    {
       public Dictionary<string,int> Items { get; set; }
    }

    public partial class Order : AbstractData
    {
        [JsonProperty("Name")]
        public string Customer { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Items")]
        public Inventory Items { get; set; }
        
        [JsonProperty("Id")]
        public int Id { get; set; }
    }

    public partial class Product : AbstractData
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
    public class Action<T>
    {
        public T Payload;
        public string Endpoint;
        public string Action;
    }
    public class Response<T>
    {
        public T Payload;
        public string message;
    }
}