namespace Dme.PhoneBook.WebAPI.Models
{
    /// <summary>
    /// Base class of pagination parameters.
    /// </summary>
    public class QueryStringParameters
    {
        #region Fields
        private const int MAX_PAGE_SIZE = 20;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets page number for GET request.
        /// </summary>
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value;
        }

        /// <summary>
        /// Gets or sets items length on page for GET request.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value <= MAX_PAGE_SIZE) ? value : MAX_PAGE_SIZE;
        }
        #endregion
    }
}
