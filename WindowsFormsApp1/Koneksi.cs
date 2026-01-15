using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Desktop
{
    public static class Koneksi
    {
        private static string connString = @"Data Source=BINTANG\MSSQLSERVER01;Initial Catalog=Quizify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        public static SqlConnection GetConn()
        {
            
            return new SqlConnection(connString);
        }
    }
}