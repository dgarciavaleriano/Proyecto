using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.DAO
{
    public static class MapeadoDB
    {
        public static List<Models.FanModelo> RecogerFansPorUsuario(int id)
        {
            var dt = DAO.CrudDB.RecogerFansPorUsuario(id);
            var listaFans = new List<Models.FanModelo>();
            foreach (DataRow row in dt.Rows)
            {
                listaFans.Add(new Models.FanModelo() {
                    Id = (int)row["Id"],
                    IdIdolatrado = (int)row["Idolatrado"],
                    IdFan = (int)row["Fan"]
                });
            }
            return listaFans;
        }

        public static List<string> RecogerNombresFansPorUsuario(int id)
        {
            var dt = DAO.CrudDB.RecogerIdentificadorFansPorUsuario(id);
            var listaFans = new List<int>();
            var fans = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                listaFans.Add((int)row["Fan"]);
            }
            foreach(var idFan in listaFans)
            {
                var fan = DAO.CrudDB.RecogerNombresFansPorUsuario(idFan);
                foreach(DataRow row in fan.Rows)
                {
                    fans.Add(row["Nombre"].ToString());
                }
                
            }
            return fans;
        }

        public static int Ranking(int id, int estilo)
        {
            var dt = CrudDB.RankingPorEstilo(estilo);
            int count = 0;
            foreach (DataRow row in dt.Rows){
                count++;
                if ((int)row["Idolatrado"] == id) return count;
            }
            count++;
            return count;
        }

        public static string RecogerNombreRemitente(int id)
        {
            var dt = CrudDB.RecogerNombreRemitente(id);

            foreach(DataRow row in dt.Rows)
            {
                return row["Nombre"].ToString();
            }

            return string.Empty;
        }

        public static List<Models.ComentarioModelo> RecogerComentariosPorUsuario(int id)
        {
            var dt = CrudDB.RecogerComentariosPorUsuario(id);
            var comentarios = new List<Models.ComentarioModelo>();

            foreach(DataRow row in dt.Rows)
            {
                comentarios.Add(new Models.ComentarioModelo()
                {
                    Id = (int)row["Id"],
                    Comentario = row["Comentario"].ToString(),
                    Remitente = (int)row["Remitente"],
                    NombreRemitente = RecogerNombreRemitente((int)row["Remitente"]),
                    Destinatario = (int)row["Destinatario"],
                    Hora = DateTime.Parse(row["Hora"].ToString())
                });
            }
            return comentarios;
        }

        public static List<Models.ModeloGenerico> RecogerUsuarios()
        {
            var dt = CrudDB.RecogerUsuarios();
            var lista = new List<Models.ModeloGenerico>();

            foreach(DataRow row in dt.Rows)
            {
                lista.Add(RecogerDatosUsuario((int)row["Id"]));
            }

            return lista;
        }

        public static Models.ModeloGenerico RecogerDatosUsuario(int id)
        {
            var dt = DAO.CrudDB.RecogerDatosUsuario(id);

            foreach (DataRow row in dt.Rows)
            {
                return new Models.ModeloGenerico()
                {
                    Id = (int)row["Id"],
                    Generales = RecogerDatosGenerales(id, row),
                    Fans = RecogerDatosFans(id),
                    Tipo = (Models.Enum.TipoUsuario)row["Tipo"],
                    Comentarios = RecogerComentariosPorUsuario(id),
                    Canciones = RecogerCancionesUsuario(id),
                    Fotos = RecogerFotosUsuario(id),
                    
                };
            }
            return new Models.ModeloGenerico();
        }

        public static Models.DatosGenerales RecogerDatosGenerales(int id, DataRow row)
        {
            return new Models.DatosGenerales()
            {
                Nombre = !string.IsNullOrEmpty(row["Nombre"].ToString()) ? row["Nombre"].ToString() : string.Empty,
                Ciudad = !string.IsNullOrEmpty(row["Ciudad"].ToString()) ? row["Ciudad"].ToString() : string.Empty,
                Pais = !string.IsNullOrEmpty(row["Pais"].ToString()) ? row["Pais"].ToString() : string.Empty,
                Descripcion = !string.IsNullOrEmpty(row["Descripcion"].ToString()) ? row["Descripcion"].ToString() : string.Empty,
                Estilo = !string.IsNullOrEmpty(row["Estilo"].ToString()) ? (Models.Enum.Estilo)row["Estilo"] : Models.Enum.Estilo.SinEstilo,
                Sonido = !string.IsNullOrEmpty(row["Influencias"].ToString()) ? row["Influencias"].ToString() : string.Empty,
                Email = !string.IsNullOrEmpty(row["Email"].ToString()) ? row["Email"].ToString() : string.Empty,
                Website = !string.IsNullOrEmpty(row["Website"].ToString()) ? row["Website"].ToString() : string.Empty,
                Ranking = Ranking(id, (int)row["Estilo"]),
                Perfil = RecogerFotoPerfil(id),
                Direccion = !string.IsNullOrEmpty(row["Direccion"].ToString()) ? row["Direccion"].ToString() : string.Empty,
                Direccion2 = !string.IsNullOrEmpty(row["Direccion2"].ToString()) ? row["Direccion2"].ToString() : string.Empty
            };
        }

        public static Models.DatosFans RecogerDatosFans(int id)
        {
            var fans = RecogerFansPorUsuario(id);

            return new Models.DatosFans()
            {
                Fans = fans,
                NombresFans = RecogerNombresFansPorUsuario(id),
                NumeroFans = fans.Count
            };
        }

        public static Models.Login.IdentidicadoModelo Identificado(string email, string password)
        {
            var dt = new DataTable();
            dt = DAO.CrudDB.RecogerDatosUsuariosPorEmail(email);
            foreach (DataRow row in dt.Rows)
            {
                if (row["Email"].ToString().Equals(email) && row["Password"].ToString().Equals(password))
                    return new Models.Login.IdentidicadoModelo()
                    {
                        Resultado = true,
                        Id = (int)row["Id"],
                        Nombre = !string.IsNullOrEmpty(row["Nombre"].ToString()) ? row["Nombre"].ToString() : string.Empty
                    };
            }
            return new Models.Login.IdentidicadoModelo();
        }

        public static List<Models.Enum.Estilo> RecogerEstilos()
        {
            return Enum.GetValues(typeof(Models.Enum.Estilo)).Cast<Models.Enum.Estilo>().ToList();
        }

        public static Models.BuscadorModelo ConversionGenericoBuscador(Models.ModeloGenerico modelo)
        {
            return new Models.BuscadorModelo()
            {
                Id = modelo.Id,
                Generales = modelo.Generales
            };
        }

        public static List<Models.CancionModelo> RecogerCancionesUsuario(int propietario)
        {
            var dt = DAO.CrudDB.RecogerCancionesUsuarios(propietario);
            var lista = new List<Models.CancionModelo>();

            foreach(DataRow row in dt.Rows)
            {
                lista.Add(new Models.CancionModelo()
                {
                    Id = (int)row["Id"],
                    Ruta = row["Ruta"].ToString(),
                    Propietario = (int)row["Propietario"],
                    Nombre = row["Nombre"].ToString()
                });
            }

            return lista;
        }

        public static List<Models.FotoModelo> RecogerFotosUsuario(int propietario)
        {
            var dt = DAO.CrudDB.RecogerFotosUsuarios(propietario);
            var lista = new List<Models.FotoModelo>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Models.FotoModelo()
                {
                    Id = (int)row["Id"],
                    Ruta = row["Ruta"].ToString(),
                    Propietario = (int)row["Propietario"],
                    Nombre = row["Nombre"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    Perfil = (row["Perfil"].ToString().Equals("1") ? true : false)
                });
            }

            return lista;
        }

        public static string RecogerFotoPerfil(int propietario)
        {
            var dt = DAO.CrudDB.RecogerFotosUsuarios(propietario);
            foreach (DataRow row in dt.Rows)
            {
                if (row["Perfil"].ToString().Equals("1")) return row["Ruta"].ToString();
            }

            return string.Empty;
        }
    }
}
