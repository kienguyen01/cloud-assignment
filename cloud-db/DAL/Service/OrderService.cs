using cloud_db.Domain;
using cloud_db.Domain.DTO;
using cloud_db.Repository;
using Microsoft.Azure.Documents;

namespace cloud_db.DAL.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<Order> CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var User = await _userRepository.GetSingle(createOrderDTO.UserId);
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                UserId = User.Id,
                User = User,
                OrderDetails = new List<OrderDetail>(),
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                ConfirmOrderDate = new DateOnly(),
                ShippingDate = new DateOnly()
            };

            User.Orders.Add(order);
            _orderRepository.Add(order);
            _userRepository.Commit();
            _orderRepository.Commit();

            return order;
        }

        public async Task<Order> ConfirmOrder(Guid OrderId)
        {
            var order = await _orderRepository.GetSingle(OrderId);

            order.ConfirmOrderDate = DateOnly.FromDateTime(DateTime.Now);

            _orderRepository.Commit();
            foreach (var b in order.OrderDetails)
            {
                Console.WriteLine(b.OrderId);
                Console.WriteLine(b.Product.ProductName);
            }
            return order;
        }

        public async Task<Order> ShipOrder(Guid OrderId)
        {
            var order = await _orderRepository.GetSingle(OrderId);

            order.ShippingDate = DateOnly.FromDateTime(DateTime.Now);

            _orderRepository.Commit();
            foreach (var b in order.OrderDetails)
            {
                Console.WriteLine(b.OrderId);
                Console.WriteLine(b.Product.ProductName);
            }
            return order;
        }

        public async Task<Order> GetOrder(Guid OrderId)
        {
            var order = await _orderRepository.GetSingle(OrderId);

            foreach (var b in order.OrderDetails)
            {
                Console.WriteLine(b.OrderId);
                Console.WriteLine(b.Product.ProductName);
            }
            return order;
        }
    }
}
