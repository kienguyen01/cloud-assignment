using cloud_db.DAL;
using cloud_db.Domain;

namespace cloud_db.Repository
{
    public class OrderDetailRepository : EntityBaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AssignmentContext context) : base(context)
        {
        }
    }
}
