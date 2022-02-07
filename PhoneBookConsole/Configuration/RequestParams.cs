/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Dme.PhoneBookConsole.Configuration
{
    /// <summary>
    /// HTTP request parameters class.
    /// </summary>
    class RequestParams
    {
        #region Properties
        /// <summary>
        /// Gets or sets the amount of records.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets fields set to request.
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request host name.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the nationalities filter.
        /// </summary>
        public List<string> Nationalities { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Define a parameters for HTTP request.
        /// </summary>
        /// <param name="appConfig">Application setting.</param>
        /// <returns>HTTP request parameters.</returns>
        public static RequestParams Default(AppConfig appConfig)
        {
            var value = new RequestParams
            {
                Count = appConfig.RecordsCount,
                Fields = new List<string> { "name", "dob", "picture" },
                Host = appConfig.DataSourceHost,
                Nationalities = new List<string> { "US" },
            };
            return value;
        }

        /// <summary>
        /// Method combine props to URL like https://randomuser.me/api/?results=5&nat=US&inc=name,dob,picture
        /// </summary>
        /// <returns>URL with arguments.</returns>
        public Uri ToUri()
        {
            Debug.Assert(string.IsNullOrEmpty(Host) == false, "The property [Host] never be an empty or null.", "string.IsNullOrEmpty(Host) == false");
            Debug.Assert(Count >= 0, "The property [Count] is greater or equ 0.", "Count >= 0");

            StringBuilder sb = new StringBuilder();
            sb.Append(Host);

            // TODO: '?' by conditions

            if (Count > 0)
                sb.AppendFormat("?results={0}", Count);

            if (Nationalities != null && Nationalities.Count > 0)
                sb.AppendFormat("&nat={0}", string.Join<string>(",", Nationalities));

            if (Fields != null && Fields.Count > 0)
                sb.AppendFormat("&inc={0}", string.Join<string>(",", Fields));

            return new Uri(sb.ToString());
        }
        #endregion
    }
}