using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
/// <summary>
/// How we have a connection to the db. Ports that xamp is going through
/// </summary>
namespace Test.Models
{//Using CHristine Bittle's Blog as refernce Nov 11th 2020
    public class SchoolDbContext
    {
        private static string User { get { return "Asia"; } }
        private static string Password { get { return "#Bacon4Life"; } }
        private static string Database { get { return "test"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
