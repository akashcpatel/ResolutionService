using Model;
using System;
using System.Threading.Tasks;

namespace Storage
{
    public interface IResolutionRepository
    {
        Task<Guid> Save(Resolution r);
        Task Delete(Guid id);
        Task<Resolution> Find(Guid id);
    }
}
