/* Assembly     PhoneBookConsole (Entity Framework version)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dme.PhoneBook.Configuration
{
    /// <summary>
    /// Application settings class.
    /// </summary>
    static class AppConfig
    {
        #region Ctor
        static AppConfig()
        {
            const string APP_CONFIG_FILE = "appsettings.json";
            var configiguration = new ConfigurationBuilder()
                .AddJsonFile(APP_CONFIG_FILE, optional: false).Build();

            ConnectionString = configiguration.GetConnectionString("DefaultConnection");
            DataSourceHost = configiguration[nameof(DataSourceHost)];
            RecordCount = int.Parse(configiguration[nameof(RecordCount)]);
        } 
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the DB connection string.
        /// </summary>
        public static string ConnectionString { get; }

        /// <summary>
        /// Gets or sets the HTTP request host name.
        /// </summary>
        public static string DataSourceHost { get; }

        /// <summary>
        /// Gets or sets the amount of records.
        /// </summary>
        public static int RecordCount { get; }
        #endregion
    }
}
