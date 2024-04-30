using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cMoneda
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public string cMon { get; set; }
        public string descripcion { get; set; }
        public double tCambio { get; set; }
        public int idSuc { get; set; }

        public cMoneda()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO moneda(cMoneda, descripcion, tcambio, idEmpresa) " +
                        "VALUES (@cMon, @descripcion, @tCambio, @idSuc)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cMon", cMon));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tCambio", tCambio));
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));

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
        public void eliminar(int idMd, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE moneda SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idmd = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idMd));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void actualizar(int idMd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE moneda SET cmoneda = @cMon, descripcion = @descripcion, tcambio = @tcambio " +
                        " WHERE idmd = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cMon", cMon));
                        cmd.Parameters.Add(new SqlParameter("@tcambio", tCambio));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@id", idMd));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerMonedabySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idmd, cMoneda, descripcion, tcambio, sucursales.nombre as nombreSuc FROM moneda, sucursales " +
                    "WHERE idempresa = sucursales.id AND eliminado IS NULL AND idEmpresa = @idSuc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public float obtenerTCambioByID(int idmd)
        {
            float tCambio = 0;
            try
            {
                comando = "SELECT tcambio FROM moneda WHERE idmd = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idmd));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    tCambio = float.Parse(reader["tcambio"].ToString());
                                }
                            }
                        }
                    }
                    return tCambio;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerMonedaByID(int idMd)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM moneda WHERE idmd = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idMd));
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
        public bool existe(string descripcion, string cMon, int idSuc)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM moneda WHERE descripcion = @descripcion AND cMoneda = @cMon and idEmpresa = @idSuc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@cMon", cMon));
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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