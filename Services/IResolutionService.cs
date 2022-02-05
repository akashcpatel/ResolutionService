using Model;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IResolutionService
    {
        Task<Guid?> UpSert(Resolution r);
        Task<Resolution> Find(Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteAllForUser(Guid id);
    }
}
