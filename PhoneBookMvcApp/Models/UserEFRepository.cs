using Dme.PhoneBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMvcApp.Models
{
    public class UserEFRepository : IUserRepository
    {
        #region Fields
        private readonly UserDbContext _context;
        private const int PAGE_SIZE = 20; // HARDCODE:
        #endregion

        #region Ctor
        public UserEFRepository(UserDbContext context)
        {
            _context = context;
        }
        #endregion

        #region IUserRepository implementation
        public async Task<IEnumerable<User>> GetPageAsync(int page)
        {
            return await _context.Users
                .OrderBy(u => u.Id)
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();
        }
        public async Task<User> GetUserAsync(long id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }          

        public async Task DeleteAsync(long id)
        {
            var user = await GetUserAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User> InsertAsync(User user)
        {
            var entry = _context.Add(user);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
        public async Task<User> UpdateAsync(User user)
        {
            User update = null;
            try
            {
                var entry = _context.Update(user);
                await _context.SaveChangesAsync();
                update = entry.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UserExists(user.Id))
                    throw;
            }
            return update;
        }
        #endregion

        #region Private methods
        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        #endregion
    }
}
