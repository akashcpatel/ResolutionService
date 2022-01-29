using Model;
using System;
using System.Threading.Tasks;

namespace Storage
{
    public interface IResolutionRepository
    {
        Task<Guid> Add(Resolution r);
        Task<Guid> Update(Resolution r);
        Task Delete(Guid id);
        Task<Resolution> Find(Guid id);
    }
}
