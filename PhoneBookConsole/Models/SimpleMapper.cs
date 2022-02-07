/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System.Collections.Generic;
using Dme.PhoneBook.Model;

namespace Dme.PhoneBookConsole.Models
{
    /// <summary>
    /// Entities mapper class.
    /// </summary>
    public class SimpleMapper
    {
        /// <summary>
        /// Manual map source type to destination.
        /// </summary>
        /// <param name="randomUsers">Source type items.</param>
        /// <returns>Target type items.</returns>
        public static IEnumerable<User> Map(IEnumerable<RandomUser> randomUsers)
        {
            List<User> values = new List<User>();
            foreach (var item in randomUsers)
                values.Add(new User 
                { 
                    FirstName        = item.Name.First, 
                    LastName         = item.Name.Last,
                    Dob              = item.Dob.Date,
                    PictureThumbnail = item.Picture.Thumbnail
                });
            return values;
        }
    }
}
