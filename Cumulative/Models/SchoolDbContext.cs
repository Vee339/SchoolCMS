using MySql.Data.MySqlClient;

namespace Cumulative.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "http5125_cumulative_1"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get {return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; Convert Zero Datetime = True";

            }
        }
        
        /// <summary>
        /// Returns a connection to the school database
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
       
        public MySqlConnection AccessDatabase()
            {
            return new MySqlConnection(ConnectionString);
        }
    }
}
