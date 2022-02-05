using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
    public interface IResolutionRepository
    {
        Task<Guid> Save(Resolution r);
        Task Delete(Guid id);
        Task Delete(IEnumerable<Resolution> ids);
        Task<Resolution> Find(Guid id);
        Task<IEnumerable<Resolution>> FindAllForUser(Guid id);
    }
}
