/* Assembly     PhoneBookConsole (console application)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Dme.PhoneBook.Configuration;
using Dme.PhoneBook.Data;
using Dme.RandomUser.Data;
using Dme.RandomUser.Model;

namespace Dme.PhoneBook
{
    class Program
    {
        static async Task Main(string[] _)
        {
            try
            {
                // load application settings
                AppConfig config = new AppConfig();

                // define HTTP request parameters
                HttpRequestParameters requestParams = HttpRequestParameters.Default(config.RecordCount, config.DataSourceHost);

                Console.WriteLine("Press any to begins...");
                Console.ReadLine();

                // get list of users
                var randomUsers = await HttpContext.DoHttpRequestAsync(requestParams);
                Console.WriteLine($"{randomUsers.Results.Count} records was read");

                // map entities
                var users = SimpleMapper.Map(randomUsers.Results);

                // database initializing
                SqlServerDbContext context = new SqlServerDbContext(config.ConnectionString);
                context.Initialize();

                // clear table and insert
                context.BulkInsert(users);
                var c = context.Count();

                Console.WriteLine($"{c} records in database"); 
                Console.WriteLine("Done");
                Console.ReadLine();
            }
            catch (ArgumentException argumentException)
            {
                ExitOnError("FAIL settings", argumentException);
            }
            catch (HttpRequestException httpExeption)
            {
                ExitOnError("FAIL HTTP request", httpExeption);
            }
            catch (SqlException sqlException)
            {
                ExitOnError("FAIL DB insertion", sqlException);
            }
        }

        static void ExitOnError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
