using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Log_IdCo
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;
        public string nombre_archivo { get; set; }
        public string archivo_pdf { get; set; }
        public long id_factura { get; set; }
        public string carpeta { get; set; }
        public string status { get; set; }
        public int id_folios { get; set; }
        public DateTime fecha { get; set; }
        public int id_empresa { get; set; }
        public string observaciones { get; set; }
        public int id_usuario { get; set; }
        public DateTime fechaAlta { get; set; }


        public Log_IdCo()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public void facturasLog()
        {
            try
            {
                comando = "SELECT * FROM log_idCO WHERE id_factura = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id_factura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    archivo_pdf = reader["archivo_pdf"].ToString();
                                    nombre_archivo = reader["nombre_archivo"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void insert()
        {
            try
            {
                comando = "INSERT INTO log_idCO(nombre_archivo, archivo_pdf, carpeta, status, id_folios, fecha, id_empresa, observaciones, id_usuario, id_factura, fecha_alta) ";
                comando += "VALUES(@xml, @pdf, @carpeta, 'Terminado', 0, GETDATE(), @emp, 'Carga de Archivo', 3, @id, GETDATE())";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@xml", nombre_archivo));
                        cmd.Parameters.Add(new SqlParameter("@pdf", archivo_pdf));
                        cmd.Parameters.Add(new SqlParameter("@carpeta", carpeta));
                        cmd.Parameters.Add(new SqlParameter("@emp", id_empresa));
                        cmd.Parameters.Add(new SqlParameter("@id", id_factura));

                        int filas = cmd.ExecuteNonQuery();
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