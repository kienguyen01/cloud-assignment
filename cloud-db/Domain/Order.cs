using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace cloud_db.Domain
{
    public class Order : IBaseEntity
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        [Required]
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? ConfirmOrderDate { get; set; }
        public DateOnly? ShippingDate { get; set; }
    }
}
