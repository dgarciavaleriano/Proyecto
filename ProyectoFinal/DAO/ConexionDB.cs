using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.DAO
{
    public static class ConexionDB
    {
        public static MySqlConnection Conexion()
        {
            var conexion = new MySqlConnectionStringBuilder();
            conexion.Server = "localhost";
            conexion.Database = "sys";
            conexion.UserID = "root";
            conexion.Password = "123456";
            conexion.IntegratedSecurity = false;

            return new MySqlConnection(conexion.ToString());
        }
    }
}
