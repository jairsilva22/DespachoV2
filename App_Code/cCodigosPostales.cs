using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public class cCodigosPostales
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int cp { get; set; }
        public string asenta { get; set; }
        public string tipo { get; set; }
        public int idEstado { get; set; }
        public int idCiudad { get; set; }
        public string zona { get; set; }

        //Constructor
        public cCodigosPostales()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO codigosPostales(cp, asenta, idEstado, idCiudad) VALUES(@cp, @asenta, @idEstado, @idCiudad)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@asenta", asenta));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
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
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM codigosPostales WHERE asenta = @asenta AND cp = @cp", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@asenta", asenta));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
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

        public DataTable getColoniasByCP(int cp)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM codigosPostales WHERE(cp = @cp)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            try
                            {
                                return dt;
                            }
                            catch (Exception)
                            {
                                return null;
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
        public DataTable getColoniasByIdC(int idC, int idE)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT asenta FROM codigosPostales WHERE (idCiudad = @idCiudad) AND (idEstado =  @idEstado)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idC));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idE));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            try
                            {
                                return dt;
                            }
                            catch (Exception)
                            {
                                return null;
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
        public string obtenerCPByAsentaAndIdC()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cp FROM codigosPostales WHERE (idEstado = @idEstado) AND (idCiudad = @idCiudad) AND (asenta = @asenta)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@asenta", asenta));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}