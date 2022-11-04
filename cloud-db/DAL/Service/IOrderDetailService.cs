using cloud_db.Domain;
using cloud_db.Domain.DTO;

namespace cloud_db.DAL.Service
{
    public interface IOrderDetailService
    {
        Task<OrderDetail> CreateOrderDetail(CreateOrderDetailDTO createOrderDetailDTO);
        Task<OrderDetail> EditOrderDetail(EditOrderDetailDTO editOrderDetailDTO);
        Task RemoveOrderDetail(Guid OrderDetailId);
    }
}
