using Model;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IResolutionService
    {
        Task<Guid?> UpSert(Resolution r);
        Task<bool> Delete(Guid id);
        Task<Resolution> Find(Guid id);
    }
}
