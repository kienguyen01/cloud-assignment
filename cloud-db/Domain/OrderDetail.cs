using Newtonsoft.Json;

namespace cloud_db.Domain
{
    public class OrderDetail : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
