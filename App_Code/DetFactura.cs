using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class DetFactura
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public long id_detfactura { get; set; }
        public float cantidad { get; set; }
        public string descripcion { get; set; }
        public float precio_unitario { get; set; }
        public float total { get; set; }
        public float descuento { get; set; }
        public float impuesto { get; set; }
        public long id_factura { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_alta { get; set; }
        public float iva { get; set; }
        public string unidad { get; set; }
        public string nparte { get; set; }
        public float retencion { get; set; }
        public float tretencion { get; set; }
        public string claveProdServ { get; set; }
        public string claveUnidad { get; set; }
        ///Comercio Exterior///
        public string noIdentificacion { get; set; }
        public string cantidadAduana { get; set; }
        public string unidadAduana { get; set; }
        public string valorUnitarioAduana { get; set; }
        public string valorDolares { get; set; }
        public string fraccionArancelaria { get; set; }

        public DetFactura()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar los detalles de la factura
        public void insertarDetFactura()
        {
            try
            {
                comando = "INSERT INTO detFactura(cantidad, descripcion, precio_unitario, total, descuento, impuesto, id_factura, ";
                comando += "fecha_alta, iva, unidad, nparte, retencion, tretencion, claveProdServ, claveUnidad) ";
                comando += "VALUES(@cantidad, @descripcion, @precio_unitario, @total, @descuento, @impuesto, @id_factura, ";
                comando += "GETDATE(), @iva, @unidad, @nparte, @retencion, @tretencion, @claveProdServ, @claveUnidad)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@precio_unitario", precio_unitario));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@descuento", descuento));
                        cmd.Parameters.Add(new SqlParameter("@impuesto", impuesto));
                        cmd.Parameters.Add(new SqlParameter("@id_factura", id_factura));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@nparte", nparte));
                        cmd.Parameters.Add(new SqlParameter("@retencion", retencion));
                        cmd.Parameters.Add(new SqlParameter("@tretencion", tretencion));
                        cmd.Parameters.Add(new SqlParameter("@claveProdServ", claveProdServ));
                        cmd.Parameters.Add(new SqlParameter("@claveUnidad", claveUnidad));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener todos los detalles de la factura
        public DataTable detallesFactura()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT id_detfactura, cantidad, descripcion, precio_unitario, total, descuento, impuesto, id_factura, id_usuario, ";
                comando += "iva, unidad, nparte, retencion, tretencion, claveProdServ, claveUnidad FROM detFactura ";
                comando += "WHERE id_factura = @id AND cantidad <> 0 ORDER BY id_detfactura ASC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id_factura));
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

        //comercio Exterior

        //metodo para buscar los campos de comercio exterior
        public void camposComercioExterior()
        {
            try
            {
                comando = "SELECT NoIdentificacion, CantidadAduana, ValorUnitarioAduana, ValorDolares, FraccionArancelaria,";
                comando += " UnidadAduana FROM detFactura WHERE id_detfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id_detfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    noIdentificacion = reader["NoIdentificacion"].ToString();
                                    cantidadAduana = (reader["CantidadAduana"].ToString());
                                    valorUnitarioAduana = (reader["ValorUnitarioAduana"].ToString());
                                    valorDolares = (reader["ValorDolares"].ToString());
                                    unidadAduana = reader["UnidadAduana"].ToString();
                                    fraccionArancelaria = reader["FraccionArancelaria"].ToString();
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

        //metodo para modificar los datos de comercio exterior
        public void modificarComExt()
        {
            try
            {
                comando = "UPDATE detFactura SET NoIdentificacion = @noidentificacion, CantidadAduana = @cantidad, UnidadAduana = @unidad,";
                comando += " ValorUnitarioAduana = @valor, ValorDolares = @dls, FraccionArancelaria = @fraccion WHERE id_detFactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@noidentificacion", noIdentificacion));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidadAduana));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidadAduana));
                        cmd.Parameters.Add(new SqlParameter("@valor", valorUnitarioAduana));
                        cmd.Parameters.Add(new SqlParameter("@dls", valorDolares));
                        cmd.Parameters.Add(new SqlParameter("@fraccion", fraccionArancelaria));
                        cmd.Parameters.Add(new SqlParameter("@id", id_detfactura));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para saber la suma total del valor en USD
        public double sumaValorUSD()
        {
            double suma = 0;

            try
            {
                comando = "SELECT SUM(ValorDolares) AS total FROM detFactura WHERE id_factura = @id";
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
                                    if (reader["total"].ToString() != "" && reader["total"].ToString() != null)
                                    {
                                        suma = double.Parse(reader["total"].ToString());
                                    }
                                }
                            }
                            return suma;
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