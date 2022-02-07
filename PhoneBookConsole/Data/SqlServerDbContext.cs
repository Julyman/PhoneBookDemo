/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

/* TODO:
 * asynchronous interaction
 * cancellation of the operation
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dme.PhoneBook.Model;

namespace Dme.PhoneBookConsole.Data
{
    /// <summary>
    /// Data storage layer class.
    /// </summary>
    public class SqlServerDbContext
    {
        #region Fields
        private readonly string _connectionString;
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">DB connection string.</param>
        /// <exception cref="ArgumentNullException">The string in <paramref name="connectionString"/> is empty or null.</exception>
        public SqlServerDbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Insert records to [User] table.
        /// </summary>
        /// <param name="users">Items for insertions.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="users"/> is null.</exception>
        /// <exception cref="SqlException">Insertion operation failed.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void BulkInsert(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users));

            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                cnn.Open();

                using (SqlTransaction transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        BulkDelete(cnn, transaction);
                        BulkInsert(cnn, transaction, users);
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Acquires the number of records in [User] table.
        /// </summary>
        /// <returns>Number of records.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public int Count()
        {
            int value = 0;
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                cnn.Open();

                const string query = "SELECT @rowscount = COUNT(*) FROM [User]";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlParameter rowscount = cmd.Parameters.Add("@rowscount", SqlDbType.Int);
                    rowscount.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    value = (int)rowscount.Value;
                }
            }
            return value;
        }
        #endregion

        #region Private methods
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public static void BulkDelete(SqlConnection cnn, SqlTransaction transaction)
        {
            string query = "DELETE FROM [User]";
            using (SqlCommand cmd = new SqlCommand(query, cnn, transaction))
            {
                cmd.CommandType = CommandType.Text;
                int count = cmd.ExecuteNonQuery();
                Debug.WriteLine($"deleted rows {count}");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        private static void BulkInsert(SqlConnection cnn, SqlTransaction transaction, IEnumerable<User> users)
        {
            #region Map items to data table
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("FirstName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("LastName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Dob", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("PictureThumbnail", typeof(string)));

            foreach (var user in users)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["FirstName"] = user.FirstName;
                dataRow["LastName"] = user.LastName;
                dataRow["Dob"] = user.Dob;
                dataRow["PictureThumbnail"] = user.PictureThumbnail;

                dataTable.Rows.Add(dataRow);
            }
            #endregion

            #region Bulk insert
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(cnn, SqlBulkCopyOptions.Default, transaction))
            {
                sqlBulkCopy.DestinationTableName = "dbo.[User]";
                sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                sqlBulkCopy.ColumnMappings.Add("LastName", "LastName");
                sqlBulkCopy.ColumnMappings.Add("Dob", "Dob");
                sqlBulkCopy.ColumnMappings.Add("PictureThumbnail", "PictureThumbnail");

                // do mass insertion
                sqlBulkCopy.WriteToServer(dataTable);
            } 
            #endregion
        }
        #endregion
    }
}
