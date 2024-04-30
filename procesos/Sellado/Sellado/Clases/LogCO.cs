using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sellado.Clases
{
    class LogCO
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades de la clase clientes que son los campos
        //necesarios para la generacion del XML
        public string nombre_archivo { get; set; }
        public string nombre_pdf { get; set; }
        public string carpeta { get; set; }
        public string status { get; set; }
        public long idFolios { get; set; }
        public int idEmpresa { get; set; }
        public string observaciones { get; set; }
        public int id_usuario { get; set; }
        public long idFactura { get; set; }

        //constructor
        public LogCO()
        {
            nombre_archivo = "";
            nombre_pdf = "";
            carpeta = "";
            status = "";
            idFolios = 0;
            idEmpresa = 0;
            observaciones = "";
            id_usuario = 0;
            idFactura = 0;
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            comando = "";
        }

        //metodo para modificar en la tabla
        public void modificarLog()
        {
            try
            {
                comando = "UPDATE log_idCo SET nombre_archivo = @xml, archivo_pdf = @pdf, status = @status, observaciones = @obs";
                comando += " WHERE id_factura = @factura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@xml", nombre_archivo));
                        cmd.Parameters.Add(new SqlParameter("@pdf", nombre_pdf));
                        cmd.Parameters.Add(new SqlParameter("@status", status));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@factura", idFactura));
                        
                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        //metodo para buscar en la tabla los estatus 'Pendiente'
        public DataTable buscarPendientes()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT nombre_archivo, archivo_pdf, carpeta, id_folios, id_factura FROM log_idCO WHERE status = 'Pendiente' ORDER BY id_factura";
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
                throw(ex);
            }
        }

        //metodo para actualizar el logCO
        public void modificarLogCO()
        {
            try
            {
                comando = "UPDATE dbo.log_idCO SET nombre_archivo = @xml, archivo_pdf = @pdf, status = 'Terminado', observaciones = 'Terminado con exito' WHERE idfactura =  @id AND status = 'Pendeinte'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@xml", nombre_archivo));
                        cmd.Parameters.Add(new SqlParameter("@pdf", nombre_pdf));
                        cmd.Parameters.Add(new SqlParameter("@id", idFactura));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar el error en el log
        public void modificarError()
        {
            try
            {
                comando = "UPDATE dbo.log_idCO SET status = 'Error', observaciones = @obs WHERE id_factura =  @id AND status = 'Pendiente'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@id", idFactura));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para limpiar las variables
        public void limpiar()
        {
            nombre_archivo = "";
            nombre_pdf = "";
            carpeta = "";
            status = "";
            idFolios = 0;
            idEmpresa = 0;
            observaciones = "";
            id_usuario = 0;
            idFactura = 0;
        }
    }
}
