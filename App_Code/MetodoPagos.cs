using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class MetodoPagos
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        public string descripcion { get; set; }
        public string forma_pago { get; set; }
        public int idPago { get; set; }
        public int idUsuario { get; set; }

        public MetodoPagos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para obtener los datos de los documentos
        public DataTable metodos()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM formPago WHERE eliminado != 1 or eliminado is null ORDER BY descripcion";
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

        //metodo para insertar
        public void insertar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO formPago(descripcion, forma_pago, idUsuario, fechaAlta) VALUES(@desc, @forma, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@desc", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@forma", forma_pago));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

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
        
        //metodo para obtener id del cliente por su clave
        public bool existeMetodo()
        {
            try
            {
                string comando = "SELECT * FROM formPago WHERE forma_pago = @forma";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@forma", forma_pago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return true;
                                }
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
                return false;
                throw (ex);
            }
        }
        public void actualizarEliminacion()
        {
            try
            {
                string comando = "update formPago set eliminado= null WHERE forma_pago = @forma";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@forma", forma_pago));
                        cmd.ExecuteReader();
                        
                          
                        
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw (ex);
            }
        }

        //metodo para obtener id del cliente por su clave
        public void obtenerMetodo()
        {
            try
            {
                string comando = "SELECT * FROM formPago WHERE idpago = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    descripcion = (reader["descripcion"].ToString());
                                    forma_pago = (reader["forma_pago"].ToString());
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

        //metodo para obtener id del cliente por su clave
        public bool existeMetodo2()
        {
            try
            {
                string comando = "SELECT * FROM formPago WHERE idpago != @id AND forma_pago != @forma";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));
                        cmd.Parameters.Add(new SqlParameter("@forma", forma_pago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return true;
                                }
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
                return false;
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void modificar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE formPago SET descripcion = @desc, forma_pago = @forma,  idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE idPago = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@desc", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@forma", forma_pago));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));

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
                    using (SqlCommand cmd = new SqlCommand("UPDATE formPago SET eliminado=1, idUsuarioEliminado=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idpago = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));

                        int filasAfectadas = cmd.ExecuteNonQuery();
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