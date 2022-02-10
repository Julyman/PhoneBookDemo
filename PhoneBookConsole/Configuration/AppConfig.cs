/* Assembly     PhoneBookConsole (console application)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Microsoft.Extensions.Configuration;
using System;

namespace Dme.PhoneBook.Configuration
{
    /// <summary>
    /// Application configuration class.
    /// </summary>
    class AppConfig
    {
        #region Ctor
        /// <summary>
        /// Constructor. Loads the application settings.
        /// </summary>
        /// <exception cref="ArgumentException">Value is null or out of range.</exception>
        public AppConfig()
        {
            // read application configuration from 'appsettings.json'

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // DB connection string
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString));

            // HTTP request host name
            DataSourceHost = Configuration["DataSourceHost"];
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException(nameof(DataSourceHost));

            // amount of records 
            var recordCount = Configuration["RecordCount"];
            if (string.IsNullOrEmpty(recordCount))
                throw new ArgumentNullException(nameof(recordCount));
            RecordCount = int.Parse(recordCount);

            if (RecordCount < 0)
                throw new ArgumentOutOfRangeException(nameof(RecordCount));

        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the DB connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request host name.
        /// </summary>
        public string DataSourceHost { get; set; }

        /// <summary>
        /// Gets or sets the amount of records.
        /// </summary>
        public int RecordCount { get; set; } 
        #endregion
    }
}
