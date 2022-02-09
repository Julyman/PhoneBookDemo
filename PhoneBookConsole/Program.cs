/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Dme.PhoneBookConsole.Configuration;
using Dme.PhoneBookConsole.Data;
using Dme.PhoneBookConsole.Models;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dme.PhoneBookConsole
{
    class Program
    {
        static async Task Main(string[] _)
        {
            try
            {
                // load application settings
                AppConfig appConfig = new AppConfig();

                // define HTTP request parameters
                RequestParams requestParams = RequestParams.Default(appConfig);

                Console.WriteLine("Press any to begins...");
                Console.ReadLine();

                // get list of users
                var randomUsers = await HttpContext.DoHttpRequestAsync(requestParams);
                Console.WriteLine($"{randomUsers.Results.Count} records was read");

                // map entities
                var users = SimpleMapper.Map(randomUsers.Results);


                // database initializing
                SqlServerDbContext context = new SqlServerDbContext(appConfig.ConnectionString);
                context.Initialize();

                // store to database
                context.BulkInsert(users);
                var c = context.Count();

                Console.WriteLine($"{c} users in database"); 
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
