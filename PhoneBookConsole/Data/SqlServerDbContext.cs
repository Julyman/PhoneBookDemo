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
            const string query = "INSERT INTO [User] (FirstName, LastName, Dob, PictureThumbnail) VALUES (@FirstName, @LastName, @Dob, @PictureThumbnail)";
            using (SqlCommand cmd = new SqlCommand(query, cnn, transaction))
            {
                cmd.CommandType = CommandType.Text;
                SqlParameter firstName = cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                SqlParameter lasttName = cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
                SqlParameter dob = cmd.Parameters.Add("@Dob", SqlDbType.DateTime2);

                // null accepted parameter
                SqlParameter pictureThumbnail = cmd.Parameters.Add("@PictureThumbnail", SqlDbType.NVarChar);
                pictureThumbnail.IsNullable = true;

                foreach (var user in users)
                {
                    // TODO: CancellationPending maybe here for asynchronous code version

                    firstName.Value = user.FirstName;
                    lasttName.Value = user.LastName;
                    dob.Value = user.Dob;
                    if (user.PictureThumbnail == null)
                        pictureThumbnail.Value = DBNull.Value;
                    else
                        pictureThumbnail.Value = user.PictureThumbnail;

                    cmd.ExecuteNonQuery();
                }
            }
        } 
        #endregion
    }
}
