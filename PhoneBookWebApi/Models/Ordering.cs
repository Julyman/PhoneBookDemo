using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dme.PhoneBook.WebAPI.Models
{
    public class Ordering
    {
        const int MAX_PAGE_SIZE = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = (value <= MAX_PAGE_SIZE) ? value : MAX_PAGE_SIZE;
            }
        }
    }
}
