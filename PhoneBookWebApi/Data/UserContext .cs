/* Assembly     PhoneBookWebApi
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Microsoft.EntityFrameworkCore;
using Dme.PhoneBook.Model;
using Dme.PhoneBook.WebAPI.Models; // TODO:
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dme.PhoneBook.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users{ get; set; }

        // Pagination and ordering method.
        public IEnumerable<User> GetUsers(Ordering ordering)
        {
            return Users
                .OrderBy(u => u.Dob).ThenBy(u => u.LastName).ThenBy(u => u.FirstName)
                .Skip((ordering.PageNumber - 1) * ordering.PageSize)
                .Take(ordering.PageSize)
                .ToList();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
