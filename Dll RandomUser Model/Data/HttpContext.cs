/* Assembly     RandomUser.Model
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Dme.RandomUser.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dme.RandomUser.Data
{
    /// <summary>
    /// HTTP request executor class.
    /// </summary>
    public static class HttpContext
    {
        /// <summary>
        /// Method receives data from an HTTP request.
        /// </summary>
        /// <param name="requestParams">Url and amount of data rows.</param>
        /// <returns>Requested data set.</returns>
        public static async Task<RandomUserResponse> DoHttpRequestAsync(HttpRequestParameters requestParams)
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
