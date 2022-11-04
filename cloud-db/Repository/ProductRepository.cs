using cloud_db.DAL;
using cloud_db.Domain;

namespace cloud_db.Repository
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AssignmentContext context) : base(context)
        {
        }
    }
}
