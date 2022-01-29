using Microsoft.AspNetCore.Mvc;
using Storage.DataTables;
using System;
using System.Threading.Tasks;

namespace Storage.Implementations
{
    internal class ResolutionRepository : IResolutionRepository
    {
        private readonly StorageConfig _storageConfig;
        private readonly ResolutionDataContext _context;

        public ResolutionRepository(StorageConfig config, [FromServices] ResolutionDataContext context)
        {
            _storageConfig = config;
            _context = context;
        }

        public async Task<Guid> Add(Model.Resolution r)
        {
            var userData = r.ResolutionToResolutionData();
            _ = await _context.ResolutionData.AddAsync(userData);

            return userData.Id;
        }

        public async Task Delete(Guid id)
        {
            var findResult = await _context.ResolutionData.FindAsync(new object[] {id});
            if(findResult != null)
                _context.ResolutionData.Remove(findResult);
        }

        public async Task<Model.Resolution> Find(Guid id)
        {
            var findUser = await _context.ResolutionData.FindAsync(new object[] { id });
            if(findUser != null)
                return findUser.ResolutionDataToResolution();
            return null;
        }

        public Task<Guid> Update(Model.Resolution r)
        {
            var resolutionData = r.ResolutionToResolutionData();
            var updateResult = _context.ResolutionData.Update(resolutionData);

            if(updateResult != null && updateResult.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                return Task.FromResult(resolutionData.Id);

            return Task.FromResult(Guid.Empty);
        }
    }
}
