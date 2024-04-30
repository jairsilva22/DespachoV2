using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cFormasPago
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string mensaje { get; set; }

        //Constructor
        public cFormasPago()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO formasPago(nombre, descripcion, tipo, mensaje, idusuario, fechaAlta) VALUES(@nombre, @calle, @descripcion, @tipo, @mensaje, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@mensaje", mensaje));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE formasPago SET nombre = @nombre, descripcion = @descripcion, tipo = @tipo, mensaje = @mensaje, idusuarioMod = @idusuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@mensaje", mensaje));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
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
                    using (SqlCommand cmd = new SqlCommand("DELETE formasPago WHERE id = @id", conn))
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

        //metodo para el combobox de proyectos
        public DataTable obtenerFormasPago()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM formasPago", conn))
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
        //metodo para Solicitudes
        public DataTable obtenerDatosByClaveCliente(string claveClt)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientes WHERE clave = @clave", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clave", claveClt));
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
        public void obtenerPerfilByID()
        {
            try
            {
                string comando = "SELECT * FROM formasPago WHERE id = @id";
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM formasPago WHERE descripcion = @nombre AND id <> @id", conn))
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM formasPago WHERE descripcion = @nombre", conn))
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
        public string obtenerFormaPagoBySolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM formasPago, solicitudes " +
                        " WHERE formasPago.id = solicitudes.idFormaPago AND solicitudes.id = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
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
        public string obtenerNombreFormasPagoByID(int idFormas)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT descripcion FROM formasPago WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idFormas));
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
    }
}