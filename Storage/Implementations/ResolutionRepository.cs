using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Storage.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Guid> Save(Resolution r)
        {
            try
            {
                var userData = r.ToData();
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

        public async Task Delete(IEnumerable<Resolution> resolutions)
        {
            await Task.Run(() =>
            {
                var removeResolutions = resolutions.Select(resolution => resolution.ToData());

                _context.ResolutionData.AttachRange(removeResolutions);
                _context.ResolutionData.RemoveRange(removeResolutions);
            });
        }

        public async Task<Resolution> Find(Guid id)
        {
            var findResolution = await _context.ResolutionData.SingleOrDefaultAsync(r => r.Id == id);
            if (findResolution != null)
                return findResolution.ToModel();
            return null;
        }

        public async Task<IEnumerable<Resolution>> FindAllForUser(Guid userId)
        {
            var resolutionsData = await Task.FromResult(_context.ResolutionData.Where(r => r.UserId == userId));
            return resolutionsData.Select(r => r.ToModel());
        }
    }
}
