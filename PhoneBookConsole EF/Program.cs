/* Assembly     PhoneBookConsole (Entity Framework version)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Dme.PhoneBook.Configuration;
using Dme.PhoneBook.Data;
using Dme.PhoneBook.Models;
using Dme.RandomUser.Data;
using Dme.RandomUser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dme.PhoneBook
{
    class Program
    {
        static async Task Main(string[] _)
        {
            Console.WriteLine("Configuration");
            Console.WriteLine($"\t ConnectionString {AppConfig.ConnectionString}");
            Console.WriteLine($"\t DataSourceHost   {AppConfig.DataSourceHost}");
            Console.WriteLine($"\t RecordCount      {AppConfig.RecordCount}");
            Console.WriteLine("Press any to begins...");
            Console.ReadLine();

            IEnumerable<User> users = null;
            try
            {
                // HTTP request to retrieve the list
                users = await LoadAsync();

                Debug.Assert(users != null, $"LoadAsync() never returns null.", "users != null");
                Console.WriteLine($"{users.Count()} records read");
            }
            catch (ArgumentException argumentException)
            {
                ExitOnError("FAIL settings", argumentException);
            }
            catch (HttpRequestException httpExeption)
            {
                ExitOnError("FAIL HTTP request", httpExeption);
            }

            // EF context
            using (UserContext context = new UserContext())
            {
                // database initializing
                context.Database.EnsureCreated();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // clear table
                        context.BulkDelete();

                        // insert
                        context.BulkInsert(users);

                        transaction.Commit();
                        var c = context.Users.Count();
                        Console.WriteLine($"{c} records in database");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExitOnError("FAIL DB insertion", ex);
                    }
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        #region Private methods
        private static async Task<IEnumerable<User>> LoadAsync()
        {
            // define HTTP request parameters
            HttpRequestParameters requestParams = HttpRequestParameters.Default(AppConfig.RecordCount, AppConfig.DataSourceHost);

            // get list of users
            var randomUsers = await HttpContext.DoHttpRequestAsync(requestParams);

            // map entities
            return SimpleMapper.Map(randomUsers.Results);
        }

        private static void ExitOnError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Environment.Exit(1);
        }
        #endregion
    }
}
