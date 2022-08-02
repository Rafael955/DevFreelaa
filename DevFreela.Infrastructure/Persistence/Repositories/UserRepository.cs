using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var userToDelete = await GetByIdAsync(id);

            _dbContext.Users.Remove(userToDelete);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync(string query)
        {
            List<User> listaUsers;

            if (query != null)
                listaUsers = await _dbContext.Users.Where(x => x.Fullname.Contains(query)).ToListAsync();
            else
                listaUsers = await _dbContext.Users.ToListAsync();

            return listaUsers;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
