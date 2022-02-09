/* Assembly     PhoneBook.Model (Dll PhoneBook Model)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System;
using System.Collections.Generic;

namespace Dme.PhoneBook.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string PictureThumbnail { get; set; }

        #region Public methods
        
        // debug method
        public static IEnumerable<User> Generate(int count)
        {
            List<User> values = new List<User>(count);
            DateTime dt = DateTime.Parse("2001-01-01");
            for (int i = 0; i < count; i++)
            {
                values.Add(new User
                {
                    FirstName = "Andy",
                    LastName = $"Anderson {i + 1}",
                    Dob = dt.AddDays(i),
                });
            }
            return values;
        } 
        #endregion
    }
}
