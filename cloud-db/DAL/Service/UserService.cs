using cloud_db.Domain;
using cloud_db.Domain.DTO;
using cloud_db.Repository;
using System.Linq;

namespace cloud_db.DAL.Service
{
    public class UserService : IUserService
    {
        AssignmentContext _context = new AssignmentContext();

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> AddUser(AddUserDTO addUserDTO)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Address = addUserDTO.Address,
                Name = addUserDTO.Name,
                Orders = new List<Order>(),
            };

            _userRepository.Add(user);

            _userRepository.Commit();

            return user;

        }
    }
}
