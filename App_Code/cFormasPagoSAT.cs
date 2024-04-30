using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace despacho
{
    public class cFormasPagoSAT
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //Propiedades
        public int codigo { get; set; }
        public int idFPS { get; set; }
        public string descripcion { get; set; }

        public cFormasPagoSAT()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }
        public void insertar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO formaPagoSAT(codigo, descripcion) " +
                        "VALUES (@codigo, @descripcion)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));

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
        public void eliminar(int idFPS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE formaPagoSAT SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idForma = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idFPS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void actualizar(int idFPS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE formaPagoSAT SET codigo = @codigo, descripcion = @descripcion WHERE idForma = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@id", idFPS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerFormasPagoSAT()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idForma, codigo, CONCAT(codigo, ' - ', descripcion) AS descripcion FROM formaPagoSAT WHERE eliminado IS NULL";
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
        public DataTable obtenerFormasPagoByID(int idFPS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM formaPagoSAT WHERE idForma = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idFPS));
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
        public void actualizarEliminacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("update formaPagoSat set eliminado = null WHERE descripcion = @descripcion", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                       
                        cmd.ExecuteReader();

                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }   
        public bool existe(int codigo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM formaPagoSat WHERE codigo = @codigo", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
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

        public int existeCod(string codigo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM formaPagoSat WHERE codigo = @codigo", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idFPS = int.Parse(reader["idForma"].ToString());
                                }
                               //return true;
                            }
                            return idFPS;
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