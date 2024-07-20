using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assign3Cumulative___Narmin_Gurbanli.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root";  } }
        private static string Database { get { return "school"; } }
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
                    + "; password = " + Password;

            }
        }

        //Getting to the database
        
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }

        
     }
}