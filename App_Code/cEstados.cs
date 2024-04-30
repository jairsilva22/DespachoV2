using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cEstados
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string estado { get; set; }

        //Constructor
        public cEstados()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO estados(estado, idUsuario, fechaAlta) VALUES(@estado, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", estado));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));

                        int filas = cmd.ExecuteNonQuery();
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE estados SET estado = @estado, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@estado", estado));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
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

        //metodo para eliminar
        public void eliminar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE estados WHERE id = @id", conn))
                    {
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
        public DataTable obtenerEstados()
        {
            try
            {
                string comando = "SELECT id, estado FROM estados";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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

        //metodo para el combobox de proyectos
        public DataTable obtenerProyectos(int idClt)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM proyectos WHERE (idCliente=0) OR (idCliente=" + idClt + ") ORDER BY id", conn))
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

        //metodo para obtener el id de perfil en el alta de Perfiles
        public int obtenerPerfil()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @descripcion", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@descripcion", cp));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0][0].ToString());
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
        public void obtenerPerfilByID()
        {
            try
            {
                string comando = "SELECT * FROM perfiles WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                //    descripcion = reader["descripcion"].ToString();
                                //    activo = bool.Parse(reader["activo"].ToString());
                                }
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
        public bool existePerfil(int existe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @nombre AND id <> @id", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@id", existe));
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

        public bool existePerfil()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @nombre", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", descripcion));
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
    }
}