using Model;
using System;
using System.Threading.Tasks;

namespace Storage
{
    public interface IUserRepository
    {
        Task<Guid> Save(User r);
        Task Delete(Guid id);
        Task<User> Find(Guid id);
    }
}
