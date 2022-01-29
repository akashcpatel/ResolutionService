using Model;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    public interface IResolutionChangedPublisher
    {
        Task Add(Resolution r);
        Task Update(Resolution r);
        Task Delete(Guid id);
    }
}
