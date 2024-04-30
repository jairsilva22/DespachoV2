using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class FacturaCancelada
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public long id { get; set; }
        public long idfactura { get; set; }
        public long folio { get; set; }
        public double total { get; set; }
        public string uuid { get; set; }
        public string codigoCan { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime fechaCancelado { get; set; }
        public string observaciones { get; set; }

        public FacturaCancelada()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar en la tabla
        public void insertar()
        {
            try
            {
                comando = "INSERT INTO facturaCancelada(idfactura, folio, total, uuid, CodigoCan, fechaSolicitud, observaciones) " +
                    "VALUES(@id, @folio, @total, @uuid, @cod, @fecha, @obs)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@uuid", uuid));
                        cmd.Parameters.Add(new SqlParameter("@cod", codigoCan));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fechaSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para modificar el estatus de cancelacion
        public void modificarCodigo()
        {
            try
            {
                comando = "UPDATE FacturaCancelada SET codigoCan = @cod, fechaCancelado = GETDATE(), observaciones = @obs WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cod", codigoCan));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el reporte de cancelados
        public DataTable reporteFacturasCanceladas(string param)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT A.fechaSolicitud, A.fechaCancelado, A.folio, A.total, A.CodigoCan, D.descripcion, C.nombreCliente, C.rfcCliente, " +
                        "A.observaciones FROM facturaCancelada AS A INNER JOIN factura AS B ON B.idfactura = A.idfactura INNER JOIN clientes AS C ON C.idCliente = " +
                        "B.idcliente INNER JOIN documento AS D ON D.iddocumento = B.tipo_comprobante WHERE(A.id > 0) " + param +
                        " ORDER BY A.fechaCancelado DESC";
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

        public bool buscarCancelacion()
        {
            try
            {
                comando = "SELECT * FROM facturaCancelada WHERE folio = @folio AND idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["id"].ToString());
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
            catch (Exception)
            {

                throw;
            }
        }

        public void modificarCancelacion()
        {
            try
            {
                comando = "UPDATE FacturaCancelada SET total = @total, uuid = @uuid, CodigoCan = @cod, fechaSolicitud = @fecha , observaciones = @obs WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@uuid", uuid));
                        cmd.Parameters.Add(new SqlParameter("@cod", codigoCan));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fechaSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));

                        int fila = cmd.ExecuteNonQuery();
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