/* Assembly     PhoneBookWebApi
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Dme.PhoneBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Dme.PhoneBook.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users{ get; set; }

        public IEnumerable<User> GetUsers(UserQueryStringParameters qsp)
        {
            // sorting first
            var users = Sort(qsp.OrderBy);

            // and pagination
            return users
                .Skip((qsp.PageNumber - 1) * qsp.PageSize)
                .Take(qsp.PageSize)
                .ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }

        #region Private methods
        private IQueryable<User> Sort(string orderByQueryString)
        {
            IQueryable<User> value = null;

            if (!Users.Any())
            {
                // no items
                value = Users.AsQueryable();
            }
            else if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                // no query params
                value = Users.OrderBy(x => x.Id).AsQueryable();
            }
            else
            {
                var orderParams = orderByQueryString.Trim().Split(',');
                var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var orderQueryBuilder = new StringBuilder();
                foreach (var param in orderParams)
                {
                    if (string.IsNullOrWhiteSpace(param))
                        continue;

                    var propertyFromQueryName = param.Split(" ")[0];
                    var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                    if (objectProperty == null)
                        continue;

                    var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                    orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
                }

                var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
                if (string.IsNullOrWhiteSpace(orderQuery))
                {
                    value = Users.OrderBy(x => x.Id).AsQueryable();
                }
                else
                {
                    value = Users.OrderBy(orderQuery); // System.Linq.Dynamic.Core
                }
            }

            return value;
        }
        #endregion
    }
}
