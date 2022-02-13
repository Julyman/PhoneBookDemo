/* Assembly     PhoneBook.Model
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace Dme.PhoneBook.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [StringLength(255)]
        public string PictureThumbnail { get; set; }
    }
}
