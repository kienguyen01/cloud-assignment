using cloud_db.DAL;
using cloud_db.Domain;

namespace cloud_db.Repository
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AssignmentContext context) : base(context)
        {
        }
    }
}
