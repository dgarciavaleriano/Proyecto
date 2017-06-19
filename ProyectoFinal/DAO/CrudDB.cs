using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.DAO
{
    public static class CrudDB
    {
        public static bool InsertarDB(Models.Login.SuscripcionPeticionModelo peticion)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into usuario (Email,Password,Nombre,Ciudad,Pais,Direccion,Estilo,Tipo) value('" + peticion.Email + "','" + peticion.Password +
                "','" + peticion.Nombre + "','" + peticion.Ciudad + "','" + peticion.Pais + "', '" + peticion.Direccion + "', '" + (int)peticion.Estilo + "', '" + (int)peticion.Tipo + "')";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool ModificarDB(Models.DatosGenerales peticion, int id)
        {
            if(peticion.Estilo == 0)
            {
                peticion.Estilo = Models.Enum.Estilo.SinEstilo;
            }
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Update usuario set Nombre = '" + peticion.Nombre + "', Estilo = '" + (int)peticion.Estilo + "', Ciudad = '" + peticion.Ciudad + "', Pais = '" +
                peticion.Pais + "', Descripcion = '" + peticion.Descripcion + "', Influencias = '" + peticion.Sonido + "', Website = '" + peticion.Website + "', Direccion = '" + 
                peticion.Direccion + "', Direccion2 = '" + peticion.Direccion2 + "' where Id = " + id;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool BorrarDB(int id)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from usuario where id = '" + id + "'";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool InsertarFan(int idIdolatrado, int IdFan)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into fan (Fan, Idolatrado) value("+ IdFan +", "+ idIdolatrado +")";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool BorrarFan(int idIdolatrado, int IdFan)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "delete from fan where Fan = "+ IdFan + " and Idolatrado = " + idIdolatrado;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool BorrarComentario(int id)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "delete from comentario where Id = " + id;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static DataTable RecogerFansPorUsuario(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM fan where Idolatrado = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static DataTable RecogerIdentificadorFansPorUsuario(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT Fan FROM fan where Idolatrado = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static DataTable RecogerNombresFansPorUsuario(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT Nombre FROM usuario where Id = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static DataTable RecogerComentariosPorUsuario(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM comentario where Destinatario = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static bool ComentarDB(string comentario, int remitente, int destinatario, DateTime hora)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into comentario (Comentario,Remitente,Destinatario,Hora) value('" + comentario + "','" + remitente +
                "','" + destinatario + "','" + hora.ToString("yyyy-MM-dd HH:mm") + "')";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool ModificarComentarioDB(string comentario, int id, DateTime hora)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Update comentario set Comentario = '" + comentario + "', Hora = '" + hora.ToString("yyyy-MM-dd HH:mm") + "' where Id = " + id;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static DataTable RankingPorEstilo(int estilo)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "select Idolatrado, count(*) as Fans, Tipo from fan f, usuario u where f.Idolatrado = u.Id and u.Estilo = "+ estilo + " group by Idolatrado order by Fans desc";
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static DataTable RecogerDatosUsuario(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM usuario where Id = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;
        }

        public static DataTable RecogerNombreRemitente(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT Nombre FROM usuario where Id = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;
        }

        public static DataTable RecogerUsuarios()
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT Id FROM usuario";
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;
        }

        public static DataTable RecogerDatosUsuariosPorTipo(Models.Enum.TipoUsuario tipo)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM usuario where Tipo =" + (int)tipo;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;
        }

        public static DataTable RecogerDatosUsuariosPorEmail(string email)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM usuario where Email ='" + email +"'";
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static DataTable RecogerCancionesUsuarios(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM cancion where Propietario = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static bool InsertarCancionDB(string ruta, int propietario, string nombre)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into cancion (Ruta,Propietario,Nombre) value('" + ruta+ "'," + propietario + ",'" + nombre +"')";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool BorrarCancion(int id)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "delete from cancion where Id = " + id;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static DataTable RecogerFotosUsuarios(int id)
        {
            var dt = new DataTable();
            var conn = DAO.ConexionDB.Conexion();
            var consulta = "SELECT * FROM foto where Propietario = " + id;
            var cmd = new MySqlCommand(consulta, conn);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public static bool InsertarFotosDB(string ruta, int propietario, string nombre, string descripcion, bool? perfil)
        {
            if (perfil == true)
            {
                ActualizarFotoPerfil();
            }
            var perf = perfil == true ? 1 : 0;
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into foto (Ruta,Propietario,Nombre,Descripcion,Perfil) value('" + ruta + "'," + propietario + ",'" + nombre + "', '" + descripcion + "'," + perf + ")";
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        private static void ActualizarFotoPerfil()
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "update foto set Perfil = 0 where Perfil = 1";
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static bool ModificarFotoPerfil(int id)
        {
            //ActualizarFotoPerfil();
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "update foto set Perfil = 1 where Id = " + id;
            conn.Open();
            return  cmd.ExecuteNonQuery() > 0;
        }

        public static bool BorrarFoto(int id)
        {
            var conn = DAO.ConexionDB.Conexion();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "delete from foto where Id = " + id;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
