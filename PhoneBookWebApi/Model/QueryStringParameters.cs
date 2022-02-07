namespace Dme.PhoneBook.Model
{
    /// <summary>
    /// Base class of pagination parameters.
    /// </summary>
    public abstract class QueryStringParameters
    {
        #region Fields
        private const int MAX_PAGE_SIZE = 20;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private int _pageSize = 10;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets page number for GET request.
        /// </summary>
        public int PageNumber { get; set; } = DEFAULT_PAGE_NUMBER;

        /// <summary>
        /// Gets or sets items length on page for GET request.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value <= MAX_PAGE_SIZE) ? value : MAX_PAGE_SIZE;
        }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public string OrderBy { get; set; }
        #endregion
    }
}
