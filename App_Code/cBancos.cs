using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    

    public class cBancos
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public bool recibo { get; set; }

        //Constructor
        public cBancos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public DataTable obtenerBancos()
        {
            try
            {
                string comando = "SELECT idbanco, nombrebanco FROM bancos WHERE eliminado IS NULL ORDER BY nombrebanco";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public DataTable obtenerBancosR()
        {
            try
            {
                string comando = "SELECT idbanco, nombrebanco FROM bancos WHERE recibo = 1 AND eliminado IS NULL ORDER BY nombrebanco";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public DataTable obtenerBcos()
        {
            try
            {
                string comando = "SELECT idbanco, nombrebanco FROM bancos WHERE eliminado IS NULL ORDER BY idbanco DESC";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public void eliminar(int idB, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE bancos SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idbanco = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idB));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void insertarBco()
        {
            try
            {
                string comando = "INSERT INTO bancos(nombrebanco, recibo, idUsuario, fechaAlta)";
                comando += "VALUES(@bco, @rec, @idUsuario, GETDATE())";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@bco", nombre));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@rec", recibo));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void modificarBco()
        {
            try
            {
                string comando = "UPDATE bancos SET nombrebanco = @dpto, idUsuarioMod = @idUsuario, fechaMod = GETDATE(), recibo = @rec WHERE idbanco = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", nombre));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@rec", recibo));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool existeBco()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idbanco FROM bancos WHERE nombrebanco = @dpto " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", nombre));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["idbanco"].ToString());
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
                throw (ex);
            }
        }

        public bool existeBco2()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idbanco FROM bancos WHERE nombrebanco = @dpto AND idbanco != @id " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", nombre));  
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["idbanco"].ToString());
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
                throw (ex);
            }
        }

        public void obtenerBco()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idbanco, nombrebanco, ISNULL(recibo,0) AS recibo FROM bancos WHERE idbanco = @id ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["idbanco"].ToString());
                                    recibo = bool.Parse(reader["recibo"].ToString());
                                    nombre = (reader["nombrebanco"].ToString());
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

    }

    
}