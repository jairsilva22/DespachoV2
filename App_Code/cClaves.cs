using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cClaves
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;
        public string clave { get; set; }
        public string descripcion { get; set; }

        public cClaves()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public DataTable claves(string param)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT DISTINCT claveProdServ FROM detFactura JOIN factura ON idfactura = id_factura WHERE claveProdServ IS NOT NULL AND claveProdServ != '' AND estatus = 'Facturada' AND timbre= 'NO' AND claveProdServ != 'METALSA SA DE CV' " + param + " ORDER BY claveProdServ ASC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@cliente", clave));
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

        public DataTable descripcionCves(string param)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT DISTINCT descripcion FROM detFactura JOIN factura ON idfactura = id_factura WHERE descripcion IS NOT NULL AND descripcion != '' AND estatus = 'Facturada' AND timbre= 'NO' AND claveProdServ = @cve " + param;
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cve", clave));
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


        public DataTable descripcionCvesU(string param)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT DISTINCT claveUnidad FROM detFactura JOIN factura ON idfactura = id_factura JOIN unidades ON claveUnidad = unidades.unidad WHERE unidades.unidad = detFactura.unidad AND claveUnidad IS NOT NULL AND claveUnidad != '' AND estatus = 'Facturada' AND timbre= 'NO' AND claveProdServ = @cve " + param;
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cve", clave));
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

        public DataTable mostrarAños()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT DISTINCT YEAR(fecha_alta) as año FROM detFactura ORDER BY año DESC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@cliente", clave));
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