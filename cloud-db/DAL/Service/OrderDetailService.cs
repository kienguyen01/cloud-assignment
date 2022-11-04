using cloud_db.Domain;
using cloud_db.Domain.DTO;
using cloud_db.Repository;

namespace cloud_db.DAL.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetail> CreateOrderDetail(CreateOrderDetailDTO createOrderDetailDTO)
        {
            var Order = await _orderRepository.GetSingle(createOrderDetailDTO.OrderId);
            var Product = await _productRepository.GetSingle(createOrderDetailDTO.ProductId);

            OrderDetail orderDetail = new OrderDetail()
            {
                Id = Guid.NewGuid(),
                OrderId = Order.Id,
                Order = Order,
                ProductId = Product.Id,
                Product = Product,
                Quantity = createOrderDetailDTO.Quantity,
            };

            Order.OrderDetails.Add(orderDetail);

            _orderDetailRepository.Add(orderDetail);
            _orderDetailRepository.Commit();
            _orderRepository.Commit();
            return orderDetail;
        }

        public async Task<OrderDetail> EditOrderDetail(EditOrderDetailDTO editOrderDetailDTO)
        {
            var orderDetail = await _orderDetailRepository.GetSingle(editOrderDetailDTO.Id);

            orderDetail.Quantity = editOrderDetailDTO.Quantity;

            _orderDetailRepository.Commit();

            Console.WriteLine(orderDetail.Product.ProductName);

            return orderDetail;
        }

        public async Task RemoveOrderDetail(Guid OrderDetailId)
        {
            var orderDetail = await _orderDetailRepository.GetSingle(OrderDetailId);

            _orderDetailRepository.Delete(orderDetail);

            _orderDetailRepository.Commit();
        }

    }
}
