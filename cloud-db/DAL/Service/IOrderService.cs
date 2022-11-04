using cloud_db.Domain;
using cloud_db.Domain.DTO;

namespace cloud_db.DAL.Service
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(CreateOrderDTO createOrderDTO);
        Task<Order> ShipOrder(Guid OrderId);
        Task<Order> ConfirmOrder(Guid OrderId);
        Task<Order> GetOrder(Guid OrderId);

    }
}
