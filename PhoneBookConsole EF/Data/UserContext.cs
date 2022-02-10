/* Assembly     PhoneBookConsole (Entity Framework version)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Dme.PhoneBook.Models;
using Dme.PhoneBook.Configuration;

namespace Dme.PhoneBook.Data
{
    /// <summary>
    /// Data layer EF class.
    /// </summary>
    public class UserContext : DbContext
    {
        #region Ctor
        // TODO: DI not used in this demo
        // public UserContext(DbContextOptions<UserContext> options): base(options) { } 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets User dataset.
        /// </summary>
        public DbSet<User> Users { get; set; }
        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Delete all records from [User] table.
        /// </summary>
        /// <remarks>This method is not to TRUNCATE table.</remarks>
        public void BulkDelete()
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (this.Users.Any())
            {
                this.ChangeTracker.AutoDetectChangesEnabled = false;
                this.RemoveRange(this.Users);

                this.ChangeTracker.DetectChanges();
                this.SaveChanges();
                this.ChangeTracker.AutoDetectChangesEnabled = true; // do not forget to re-enable
            }
            sw.Stop();
            Debug.WriteLine($"BulkDelete elapsed {sw.Elapsed}");
        }

        /// <summary>
        /// Bulk insert records into [User] table.
        /// </summary>
        /// <param name="users">Items for insertions.</param>
        public void BulkInsert(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users));

            Stopwatch sw = Stopwatch.StartNew();
            this.ChangeTracker.AutoDetectChangesEnabled = false;

            foreach (var user in users)
                this.Add<User>(user);

            this.ChangeTracker.DetectChanges();
            this.SaveChanges();
            this.ChangeTracker.AutoDetectChangesEnabled = true; // do not forget to re-enable

            sw.Stop();
            Debug.WriteLine($"BulkInsert {users.Count()} elapsed {sw.Elapsed}");
        } 
        #endregion
    }
}
