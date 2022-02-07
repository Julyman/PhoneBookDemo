/* Assembly     PhoneBookWebApi
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Microsoft.EntityFrameworkCore;
using Dme.PhoneBook.Model;
using Dme.PhoneBook.WebAPI.Models; // TODO: combine model namespaces?
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
        public IEnumerable<User> GetUsers(QueryStringParameters qsp)
        {
            return Users
                .OrderBy(u => u.Dob)
                    .ThenBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                .Skip((qsp.PageNumber - 1) * qsp.PageSize)
                .Take(qsp.PageSize)
                .ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
