using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.DataTables;
using System;
using System.Threading.Tasks;

namespace Storage.Implementations
{
    internal class ResolutionRepository : IResolutionRepository
    {
        private readonly ResolutionDataContext _context;
        private readonly ILogger<ResolutionRepository> _logger;

        public ResolutionRepository([FromServices] ResolutionDataContext context, ILogger<ResolutionRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Guid> Save(Model.Resolution r)
        {
            try
            {
                var userData = r.ResolutionToResolutionData();
                _ = await _context.ResolutionData.AddAsync(userData);

                return userData.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while saving resolution for {resolution}.", r, ex);

                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(Guid id)
        {
            var resolutionData = new ResolutionData
            {
                Id = id
            };

            await Task.Run(() =>
            {
                _context.ResolutionData.Attach(resolutionData);
                _context.ResolutionData.Remove(resolutionData);
                _context.SaveChanges();
            });
        }

        public async Task<Model.Resolution> Find(Guid id)
        {
            var findResolution = await _context.ResolutionData.SingleOrDefaultAsync(r => r.Id == id);
            if (findResolution != null)
                return findResolution.ResolutionDataToResolution();
            return null;
        }
    }
}
