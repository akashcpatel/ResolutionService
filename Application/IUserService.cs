using Model;
using Publisher.Message.Data;
using System;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserService
    {
        Task Sync(UserChangedData userChangedData);
        Task<User> Find(Guid id);
    }
}
