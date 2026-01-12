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
        private static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Quizify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        public static SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}