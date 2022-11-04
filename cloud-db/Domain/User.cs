using Newtonsoft.Json;

namespace cloud_db.Domain
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
