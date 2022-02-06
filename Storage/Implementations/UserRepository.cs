using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Storage.DataTables;
using System;
using System.Threading.Tasks;

namespace Storage.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly ResolutionDataContext _context;
        private readonly ILogger<ResolutionRepository> _logger;

        public UserRepository([FromServices] ResolutionDataContext context, ILogger<ResolutionRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Guid> Save(User u)
        {
            var userData = await _context.UserData.FindAsync(u.Id);

            try
            {
                if (userData == null)
                    _context.UserData.Add(u.ToData());
                else
                {
                    userData.FirstName = u.FirstName;
                    userData.LastName = u.LastName;
                }

                _context.SaveChanges();
                return u.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while saving user for {user}.", u, ex);

                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(Guid id)
        {
            var userData = new UserData
            {
                Id = id
            };

            await Task.Run(() =>
            {
                _context.UserData.Attach(userData);
                _context.UserData.Remove(userData);
                _context.SaveChanges();
            });
        }

        public async Task<User> Find(Guid id)
        {
            var findUser = await _context.UserData.SingleOrDefaultAsync(r => r.Id == id);
            if (findUser != null)
                return findUser.ToModel();
            return null;
        }
    }
}
