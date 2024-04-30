using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cDetallesFactura
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public float cantidad { get; set; }
        public string descripcion { get; set; }
        public float precio_unitario { get; set; }
        public float total { get; set; }
        public float impuesto { get; set; }
        public int id_factura { get; set; }
        public int idUsuario { get; set; }
        public int descuento { get; set; }
        public int tretencion { get; set; }
        public int impuestoP { get; set; }
        public string iva { get; set; }
        public string unidad { get; set; }
        public string nparte { get; set; }
        public string claveUnidad { get; set; }
        public string claveProdServ { get; set; }

        public cDetallesFactura()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public void insertarDetFactura()
        {
            try
            {
                comando = "INSERT INTO detfactura(cantidad, descripcion, precio_unitario, total, id_factura, iva, unidad, nparte, claveProdServ, claveUnidad, impuesto, " +
                    " id_usuario, fecha_alta, descuento, tretencion, impuestoP) " +
                    "VALUES(@cantidad, @desc, @precio_unitario, @total, @id_factura, @iva, @unidad, @nparte, @claveProdServ, @claveunidad, @impuesto," +
                    " @idUsuario, GETDATE(), @descuento, @tretencion, @impuestoP)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@desc", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@precio_unitario", precio_unitario));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@id_factura", id_factura));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@nparte", nparte));
                        cmd.Parameters.Add(new SqlParameter("@claveUnidad", claveUnidad));
                        cmd.Parameters.Add(new SqlParameter("@claveProdServ", claveProdServ));
                        cmd.Parameters.Add(new SqlParameter("@impuesto", impuesto));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@descuento", descuento));
                        cmd.Parameters.Add(new SqlParameter("@tretencion", tretencion));
                        cmd.Parameters.Add(new SqlParameter("@impuestoP", impuestoP));
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