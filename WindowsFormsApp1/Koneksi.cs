using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Desktop
{
    class Koneksi
    {
        private static string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Quizify;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}