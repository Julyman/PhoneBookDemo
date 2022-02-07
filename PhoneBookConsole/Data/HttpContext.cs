/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Newtonsoft.Json;
using Dme.PhoneBookConsole.Configuration;
using Dme.PhoneBookConsole.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dme.PhoneBookConsole.Data
{
    /// <summary>
    /// HTTP request executor class.
    /// </summary>
    internal static class HttpContext
    {
        /// <summary>
        /// Method receives data from an HTTP request.
        /// </summary>
        /// <param name="requestParams">Url and amount of data rows.</param>
        /// <returns>Requested data set.</returns>
        public static async Task<RandomUserResponse> DoHttpRequestAsync(RequestParams requestParams)
        {
            RandomUserResponse value = null;
            var uri = requestParams.ToUri();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(requestParams.Host);

                // do request
                var httpResponse = await client.GetAsync(uri);
                httpResponse.EnsureSuccessStatusCode();

                // parse response
                var content = await httpResponse.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<RandomUserResponse>(content);
            }
            return value;
        }
    }
}
