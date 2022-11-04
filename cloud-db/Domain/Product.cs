using Newtonsoft.Json;

namespace cloud_db.Domain
{
    public class Product : IBaseEntity
    {
        public Guid Id { get; set; }
        public string ImageBlob { get; set; }
        public List<string> Review { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
