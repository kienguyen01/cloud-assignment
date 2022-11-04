using cloud_db.Domain;
using cloud_db.Domain.DTO;

namespace cloud_db.DAL.Service
{
    public interface IUserService
    {
        Task<User> AddUser(AddUserDTO addUserDTO);
    }
}
