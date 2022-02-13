using Dme.PhoneBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMvcApp.Models
{
    // TODO: move to DLL
    public interface IUserRepository
    {
        Task<User> GetUserAsync(long id);
        Task<IEnumerable<User>> GetPageAsync(int page);
        Task DeleteAsync(long id);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
