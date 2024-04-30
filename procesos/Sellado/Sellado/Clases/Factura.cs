using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sellado.Clases
{
    class Factura
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades de la clase clientes que son los campos
        //necesarios para la generacion del XML
        public long idFactura { get; set; }
        public long idCliente { get; set; }
        public int idEmpresa { get; set; }
        public string CadenaOrig { get; set; }
        public string carpeta { get; set; }
        public string noCertificado { get; set; }
        public string abreviatura { get; set; }
        public double iva { get; set; }
        public double tipoCambio { get; set; }
        public string condicionesDePago { get; set; }
        public string formaDePago { get; set; }
        public DateTime fechaCfd { get; set; }        
        public double total { get; set; }
        public string folio { get; set; }
        public string serie { get; set; }
        public string metodoDePago { get; set; }
        public string estatus { get; set; }
        public double retencion { get; set; }
        public double tasa { get; set; }

        public string carpetaTimbre { get; set; }
        //construnctor
        public Factura()
        {
            idFactura = 0;
            idEmpresa = 0;
            idCliente = 0;
            folio = "";
            serie = "";
            estatus = "";
            noCertificado = "";
            abreviatura = "";
            carpeta = "";
            CadenaOrig = "";
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            comando = "";
            carpetaTimbre = "";
        }

        //metodo para buscar las facturas en estatus de ProcesoCO
        public void buscarFacturas()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT folio, idfactura, serie, estatus, idcliente, Ncarpeta, abreviatura, factura.idempresa, carpetaTimbre FROM dbo.factura JOIN sucursales as empresas ON empresas.id = factura.idEmpresa WHERE idfactura = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idFactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = reader["folio"].ToString();
                                    idFactura = long.Parse(reader["idfactura"].ToString());
                                    idEmpresa = int.Parse(reader["idempresa"].ToString());
                                    serie = "";
                                    estatus = reader["estatus"].ToString();
                                    abreviatura = reader["abreviatura"].ToString();
                                    carpeta = reader["Ncarpeta"].ToString() + @"\";
                                    idCliente = long.Parse(reader["idcliente"].ToString());
                                    carpetaTimbre = reader["carpetaTimbre"].ToString();
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

        //metodo para actualizar a Procesando la factura
        public void actualizarFactura()
        {
            try
            {
                comando = "UPDATE dbo.factura SET fechacfd = GETDATE(), fechasellado = GETDATE(), estatus = @estatus, folio = @folio, serie = @serie, numero_certificado = @noCer, timbre = 'SI', estadoComprobante = 1, cadena_original = @co WHERE idfactura =  @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@noCer", noCertificado));
                        cmd.Parameters.Add(new SqlParameter("@co", CadenaOrig));
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

        //metodo para limpiar las variables del objeto
        public void limpiar()
        {
            idFactura = 0;
            idEmpresa = 0;
            folio = "";
            serie = "";
            estatus = "";
            noCertificado = "";
            abreviatura = "";
            carpeta = "";
            CadenaOrig = "";
        }

    }
}
