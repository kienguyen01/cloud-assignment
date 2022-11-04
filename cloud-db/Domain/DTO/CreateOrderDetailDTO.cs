namespace cloud_db.Domain.DTO
{
    public class CreateOrderDetailDTO
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }

    }
}