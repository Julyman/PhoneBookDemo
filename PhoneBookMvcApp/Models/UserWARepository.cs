using Dme.PhoneBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PhoneBookMvcApp.Models
{
    public class UserWARepository : IUserRepository
    {
        // TODO: change to Factory
        static UserWARepository()
        {
            const string BASE_URL = "http://localhost:5000"; // TODO: // HARDCODE: 
            var clientHandler = new HttpClientHandler();

            // TODO: HACK: ssl problem
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors)
                =>
            { return true; };

            _httpClient = new HttpClient(clientHandler);

            // passing service base URL
            _httpClient.BaseAddress = new Uri(BASE_URL);

            // data format for HTTP request 
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        private static readonly HttpClient _httpClient;

        #region Fields
        private const string REQUEST_URL = "api/users"; // TODO: // HARDCODE: 
        #endregion

        #region IUserRepository implementation
        public async Task<IEnumerable<User>> GetPageAsync(int page)
        {
            IEnumerable<User> items = null;

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"{REQUEST_URL}?PageSize=20&PageNumber={page}"); // HARDCODE:
            if (httpResponse.IsSuccessStatusCode)
            {
                // deserializing the response into the User list
                var content = httpResponse.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<User>>(content).ToList();
            }
            return items;
        }

        public async Task<User> GetUserAsync(long id)
        {
            User user = null;
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"{REQUEST_URL}/{id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = httpResponse.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(content);
            }
            return user;
        }

        public async Task DeleteAsync(long id)
        {
            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"{REQUEST_URL}/{id}");
            if (httpResponse.IsSuccessStatusCode)
                Debug.WriteLine("exists and deleted");
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                Debug.WriteLine("not found on deleting");
        }

        public async Task<User> InsertAsync(User user)
        {
            User insert = null;
            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync<User>(REQUEST_URL, user);
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = httpResponse.Content.ReadAsStringAsync().Result;
                insert = JsonConvert.DeserializeObject<User>(content);
            }
            return insert;
        }

        public async Task<User> UpdateAsync(User user)
        {
            User update = null;
            HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync<User>($"{REQUEST_URL}/{user.Id}", user);
            switch (httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.NoContent: // actual response
                    update = user;
                    break;

                case System.Net.HttpStatusCode.NotFound: // actual response
                    break;

                default:
                    throw new InvalidOperationException($"HttpClient response {httpResponse.StatusCode}");
            }
            return update;
        }
        #endregion
    }
}
