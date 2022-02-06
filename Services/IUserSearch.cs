using Model;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserSearch
    {
        Task<User> Find(Guid id);
    }
}