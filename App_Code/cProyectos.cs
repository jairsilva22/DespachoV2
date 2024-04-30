using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace despacho
{
    public class cProyectos
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string interior { get; set; }
        public string colonia { get; set; }
        public int cp { get; set; }
        public int idEstado { get; set; }
        public int idCiudad { get; set; }
        public int idCliente { get; set; }

        //Constructor
        public cProyectos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO proyectos(nombre, calle, numero, interior, colonia, cp, idEstado, idCiudad, idCliente, idUsuario, fechaAlta) " +
                        "VALUES(@nombre, @calle, @numero, @interior, @colonia, @cp, @idEstado, @idCiudad, @idCliente, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        int filas = cmd.ExecuteNonQuery();
                        throw new Exception("ok");

                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE proyectos SET nombre=@nombre,calle = @calle, numero = @numero, interior = @interior, colonia = @colonia, cp = @cp, " +
                        "idEstado = @idEstado, idCiudad = @idCiudad, idCliente = @idCliente, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        throw new Exception("ok");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        //metodo para eliminar
        public void eliminar(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE proyectos SET eliminado = 1, idUsuarioElimino = @idUsuarioElimino, fechaEliminado = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el combobox de proyectos
        public DataTable obtenerProyectos(int idClt)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM proyectos WHERE (idCliente=" + idClt + ") AND (eliminado IS NULL) ORDER BY id", conn))
                    //using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM proyectos WHERE ((idCliente=0) OR (idCliente=" + idClt + ")) AND (eliminado IS NULL) ORDER BY id", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerProyectosBySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT proyectos.id, proyectos.nombre FROM proyectos, clientes WHERE proyectos.idCliente = clientes.id AND idsucursal = @idSuc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para obtener el id de perfil en el alta de Perfiles
        public DataTable obtenerProyectosByID(int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM proyectos WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idP));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para obtener el id de perfil en el alta de Perfiles
        public DataTable obtenerProyectosByIdCliente(int idC)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM proyectos WHERE idCliente = @idCliente AND (eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idC));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el Perfil por ID
        public int obtenerProyectoByCPCalleNumeroLIKE(int pcp, string pcalle, int pnum, int idCliente)
        {
            try
            {
                string comando = "SELECT id FROM proyectos WHERE (cp=@cp) AND (calle LIKE '%" + pcalle + "%') AND (numero=@numero) AND (idCliente=@idCliente) AND (eliminado IS NULL)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cp", pcp));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@numero", pnum));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }


        //metodo para obtener el Perfil por ID
        public int obtenerProyectoByCPCalleNumeroEQUAL(int pcp, string pcalle, int pnum, int idCliente)
        {
            try
            {
                string comando = "SELECT id FROM proyectos WHERE (cp=@cp) AND (calle = @calle) AND (numero=@numero) AND (idCliente=@idCliente) AND (eliminado IS NULL)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cp", pcp));
                        cmd.Parameters.Add(new SqlParameter("@calle", pcalle));
                        cmd.Parameters.Add(new SqlParameter("@numero", pnum));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }

        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM proyectos WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool existeEnCliente()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM proyectos WHERE  nombre = @nombre AND idCliente = @idCliente", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string obtenerNombreProyectoByID(int idProyecto)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM proyectos WHERE id = @idProyecto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProyecto", idProyecto));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable obtenerProyectosByIdClienteModal(int idC)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM proyectos WHERE idCliente = @idCliente AND (eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idC));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty)
                            {

                                return null;
                            }
                            else
                            {
                                return dt;

                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}