using cloud_db.DAL;
using cloud_db.Domain;

namespace cloud_db.Repository
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(AssignmentContext context) : base(context)
        {
        }
    }
}
