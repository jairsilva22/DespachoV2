using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class UnidadesAduana
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }

        public UnidadesAduana()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar()
        {
            try
            {
                comando = "INSERT INTO UnidadAduana(codigo, descripcion) VALUES(@cod, @desc)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cod", codigo));
                        cmd.Parameters.Add(new SqlParameter("@desc", descripcion));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para modificar
        public void modificar()
        {
            try
            {
                comando = "UPDATE unidadAduana SET codigo = @cod, descripcion = @desc WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cod", codigo));
                        cmd.Parameters.Add(new SqlParameter("@desc", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para buscar los datos
        public void buscar()
        {
            try
            {
                comando = "SELECT * FROM unidadAduana WHERE id = @id";
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
                                    codigo = reader["codigo"].ToString();
                                    descripcion = reader["descripcion"].ToString();
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

        //metodo para mostrar los datos de la tabla
        public DataTable unidadesAduana()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM unidadAduana ORDER BY codigo";
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

        //metodo para el combo en comercio exterior
        public DataTable unidadesComExt()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT codigo, CONCAT(codigo, '->', descripcion) AS descripcion FROM unidadAduana ORDER BY id DESC";
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
    }
}