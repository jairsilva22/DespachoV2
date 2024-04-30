using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class DetallesPagos
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        public int idfactura { get; set; }
        public int folio { get; set; }
        public int idpago { get; set; }
        public float cantidadRecibida { get; set; }
        public string metodoPago { get; set; }
        public string nombreCliente { get; set; }
        public string rfcCliente { get; set; }
        public string formaPago { get; set; }
        public int noParciaidad { get; set; }
        public string rfcEmisorCtaOrd { get; set; }
        public string ctaOrdenante { get; set; }
        public string NomBancoOrdExt { get; set; }
        public string rfcEmisorCteBen { get; set; }
        public string ctaBeneficiario { get; set; }
        public string fechaAlta { get; set; }
        public string moneda { get; set; }
        public string monedaPago { get; set; }
        public int idCliente { get; set; }

        public int noParcialidadDetI { get; set; }
        public int folioDetI { get; set; }
        public string serieDetI { get; set; }
        public float saldoAntDetI { get; set; }
        public float importePagadoDetI { get; set; }
        public float impSaldoInsolutoDetI { get; set; }

        public string truncar { get; set; }
        public string tasa { get; set; }
        public string isr { get; set; }
        public string digitos { get; set; }
        public string serie { get; set; }
        public string cadena_original { get; set; }
        public double cambio { get; set; }
        public double cambioP { get; set; }
        public double tipoCambioP { get; set; }
        public double tipoCambioDR { get; set; }
        //
        public string noOperacion { get; set; }
        public string correo { get; set; }
        public double impSaldoInsoluto { get; set; }
        public double impPagado { get; set; }
        public double ImpSaldoAnt { get; set; }
        public int noParcialidad { get; set; }
        public string idDocumento { get; set; }
        public string carpetaPago { get; set; }
        public string UUID { get; set; }
        public string cadenaConex { get; set; }
        public long folioActivo { get; set; }
        public double cambioP1 { get; set; }
        public int tComprobante { get; set; }
        public double ImpSaldoAnterior { get; set; }
        public int parcialidad { get; set; }

        public DetallesPagos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public void buscarFactura()
        {
            try
            {
                comando = "SELECT cantidad_recibida, pagosFactura.metodoPago, nombreCliente, rfcCliente, pagosFactura.formaPago, noParcialidad, RfcEmisorCtaOrd, ";
                comando += "CtaOrdenante, NomBancoOrdExt, RfcEmisorCtaBen, CtaBeneficiario, noOperacion, pagosFactura.fechaAlta, moneda, tipoCambioDR FROM dbo.pagosFactura INNER JOIN clientes ON ";
                comando += "pagosFactura.idCliente = Clientes.idCliente WHERE idcc = @idpago";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idpago", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cantidadRecibida = float.Parse(reader["cantidad_recibida"].ToString());
                                    metodoPago = (reader["metodoPago"].ToString());
                                    nombreCliente = (reader["nombreCliente"].ToString());
                                    rfcCliente = (reader["rfcCliente"].ToString());
                                    formaPago = (reader["formaPago"].ToString());
                                    noParciaidad = int.Parse(reader["noParcialidad"].ToString());
                                    rfcEmisorCtaOrd = (reader["RfcEmisorCtaOrd"].ToString());
                                    ctaOrdenante = (reader["CtaOrdenante"].ToString());
                                    NomBancoOrdExt = reader["NomBancoOrdExt"].ToString();
                                    rfcEmisorCteBen = (reader["RfcEmisorCtaBen"].ToString());
                                    ctaBeneficiario = (reader["ctaBeneficiario"].ToString());
                                    fechaAlta = reader["fechaAlta"].ToString();
                                    moneda = reader["moneda"].ToString();
                                    noOperacion = reader["noOperacion"].ToString();
                                    cambioP = double.Parse(reader["tipoCambioDR"].ToString());
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

        public bool buscarFacturaExcel()
        {
            try
            {
                comando = "SELECT * FROM dbo.pagosFactura WHERE folio = @folio";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
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

        public DataTable buscarDetFactura()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT d.*, m.descripcion FROM dbo.detallesPagoFacturas d INNER JOIN documento m ON d.tComprobante = m.iddocumento WHERE idcc = @idpago";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idpago", idpago));
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

        public bool BuscarDetallesIng()
        {
            try
            {
                comando = "SELECT d.* FROM detallesPagoFacturas d INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE d.idfactura = @idfactura AND p.estatus = 'Pagado'";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        //cmd.Parameters.Add(new SqlParameter("@id", IdProceso));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                //while (reader.Read())
                                //{
                                //  productId = int.Parse(reader["productId"].ToString());

                                //}
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

        public void buscarDetFacturaIng()
        {
            try
            {
                comando = "SELECT * FROM dbo.detallesPagoFacturas WHERE idfactura = @idfactura AND impSaldoInsoluto <> 0";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    noParcialidadDetI = int.Parse(reader["noParcialidad"].ToString());
                                    folioDetI = int.Parse(reader["folio"].ToString());
                                    serieDetI = (reader["serie"].ToString());
                                    saldoAntDetI = float.Parse(reader["ImpSaldoAnt"].ToString());
                                    importePagadoDetI = float.Parse(reader["impPagado"].ToString());
                                    impSaldoInsolutoDetI = float.Parse(reader["impSaldoInsoluto"].ToString());
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

        //buscar el cliente de la factura enviada para buscar las facturas pendientes del cliente 
        public void buscarCliente()
        {
            try
            {
                comando = "SELECT factura.*, truncar, isr FROM dbo.factura INNER JOIN clientes ON factura.idCliente = clientes.idCliente WHERE idfactura = @idfactura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idCliente = int.Parse(reader["idCliente"].ToString());
                                    truncar = reader["truncar"].ToString();
                                    tasa = reader["tasa"].ToString();
                                    isr = reader["isr"].ToString();
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

        //buscar las facturas del cliente
        public DataTable buscarFacturasCteDet()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT d.idfactura, f.cadena_original FROM detallesPagoFacturas d INNER JOIN factura f ON d.idfactura = f.idfactura INNER JOIN pagosFactura p ON ";
                comando += "d.idcc = p.idcc WHERE f.idcliente = 13";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));
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

        public DataTable buscarFacturasCteDet1()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT d.idfactura, d.folio, sum(d.impPagado) AS impPagado, f.cadena_original, d.serie FROM detallesPagoFacturas d INNER JOIN factura f ON ";
                comando += "d.idfactura = f.idfactura INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE f.idcliente = @idcliente GROUP BY ";
                comando += "d.idfactura, d.folio, f.cadena_original, d.serie ORDER BY folio desc ";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));
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

        //SELECT A.folio, A.serie, A.idfactura FROM factura A LEFT JOIN detallesPagoFacturas B ON a.idfactura = B.idfactura WHERE B.idfactura IS NULL AND idcliente = 13 ORDER BY folio DESC
        //buscar facturas pdtes
        public DataTable buscarFacturasPtes()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT A.folio as folio, A.serie as serie, A.idfactura as idfactura, A.total as total, A.cadena_original as cadena FROM factura A LEFT JOIN detallesPagoFacturas B ON a.idfactura = B.idfactura WHERE B.idfactura IS NULL AND ";
                comando += "idcliente = @idcliente AND timbre = 'NO' AND estatus <> 'Cancelada' AND cadena_original != '' AND A.tipo_comprobante <> 2 ORDER BY folio DESC";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));
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

        public void buscarFacturaPago()
        {
            try
            {
                comando = "SELECT factura.idcliente, folio, serie, nombreCliente, rfcCliente, moneda, factura.forma_Pago, factura.metodoPago, cadena_original, moneda.descripcion, UUID, ";
                comando += "formaPago, moneda.cambio, tipo_comprobante FROM dbo.factura INNER JOIN clientes ON factura.idCliente = Clientes.idCliente INNER JOIN moneda ON moneda.idmoneda = factura.moneda ";
                comando += " WHERE idfactura = @idfactura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@idpago", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = int.Parse(reader["folio"].ToString());
                                    serie = reader["serie"].ToString();
                                    nombreCliente = (reader["nombreCliente"].ToString());
                                    rfcCliente = (reader["rfcCliente"].ToString());
                                    formaPago = (reader["formaPago"].ToString());
                                    moneda = reader["descripcion"].ToString();
                                    formaPago = reader["metodoPago"].ToString();
                                    metodoPago = reader["formaPago"].ToString();
                                    cadena_original = reader["cadena_original"].ToString();
                                    cambio = double.Parse(reader["cambio"].ToString());
                                    UUID = reader["UUID"].ToString();
                                    tComprobante = int.Parse(reader["tipo_comprobante"].ToString());
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

        //buscar la factura ingresada en el filtro de busqueda primero en la tabla de pagos 
        public DataTable buscarFacturaSelec()
        {
            try
            {
                DataTable dt = new DataTable();


                comando = "SELECT d.idfactura, d.folio, sum(d.impPagado) AS impPagado, f.cadena_original, d.serie FROM detallesPagoFacturas d INNER JOIN factura f ON d.idfactura = f.idfactura ";
                comando += "INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE d.folio =@folio AND f.idcliente = @idcliente AND p.estatus <> 'Cancelado' GROUP BY d.idfactura, d.folio, ";
                comando += "f.cadena_original, d.serie ";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));

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

        public bool BuscarDetallesFiltro()
        {
            try
            {
                comando = "SELECT * FROM detallesPagoFacturas WHERE folio = @folio";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        //cmd.Parameters.Add(new SqlParameter("@id", IdProceso));

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

        public DataTable buscarFacturasFiltro()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT folio, serie, idfactura, total, cadena_original as cadena FROM factura WHERE folio = @folio AND idcliente = @idcliente ";
                comando += "AND timbre = 'NO' AND estatus <> 'Cancelada' AND cadena_original <> '' AND tipo_comprobante <> 2";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));
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

        public void AgregarPagoFacIng()
        {
            try
            {

                comando = " SELECT d.idfactura, d.serie, d.folio, sum(d.impPagado) AS impPagado, max(d.noParcialidad) as noParcialidad, c.nombreCliente, c.rfccliente, d.moneda, ";
                comando += "d.formaPago, d.cambio, d.idDocumento, f.cadena_original, d.tComprobante FROM detallesPagoFacturas d INNER JOIN factura f ON d.idfactura = f.idfactura INNER JOIN clientes ";
                comando += "c ON f.idcliente = c.idCliente INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE d.idfactura = @idfactura AND p.estatus <> 'Cancelado' GROUP by d.idfactura, ";
                comando += "d.serie, d.folio, c.nombreCliente, c.rfccliente, d.moneda, d.formaPago, d.cambio, d.idDocumento, f.cadena_original";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    noParcialidadDetI = int.Parse(reader["noParcialidad"].ToString());
                                    folioDetI = int.Parse(reader["folio"].ToString());
                                    serieDetI = (reader["serie"].ToString());
                                    importePagadoDetI = float.Parse(reader["impPagado"].ToString());
                                    moneda = reader["moneda"].ToString();
                                    formaPago = reader["formaPago"].ToString();
                                    nombreCliente = reader["nombrecliente"].ToString();
                                    rfcCliente = reader["rfcCliente"].ToString();
                                    cambio = double.Parse(reader["cambio"].ToString());
                                    idDocumento = reader["idDocumento"].ToString();
                                    cadena_original = reader["cadena_original"].ToString();
                                    tComprobante = int.Parse(reader["tComprobante"].ToString());
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

        //metodo para insertar en la tabla la factura
        public void insertarPago()
        {
            try
            {
                comando = "INSERT INTO dbo.detallesPagoFacturas (idcc, idfactura, folio, serie, impSaldoInsoluto, impPagado, ImpSaldoAnt, noParcialidad, metodoPago, moneda, idDocumento, ";
                comando += "cambio, formaPago, tipoCambioDR, tComprobante) VALUES (@idcc, @idfactura, @folio, @serie, @impSaldoInsoluto, @impPagado, @ImpSaldoAnt, @noParcialidad, @metodoPago, @moneda,";
                comando += " @idDocumento, @cambio, @formaPago, @tipoCambioDR, @tComprobante)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@impSaldoInsoluto", impSaldoInsoluto));
                        cmd.Parameters.Add(new SqlParameter("@impPagado", impPagado));
                        cmd.Parameters.Add(new SqlParameter("@ImpSaldoAnt", ImpSaldoAnt));
                        cmd.Parameters.Add(new SqlParameter("@noParcialidad", noParcialidad));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@moneda", moneda));
                        cmd.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
                        cmd.Parameters.Add(new SqlParameter("@cambio", cambio));
                        cmd.Parameters.Add(new SqlParameter("@formaPago", formaPago));
                        cmd.Parameters.Add(new SqlParameter("@tipoCambioDR", tipoCambioDR));
                        cmd.Parameters.Add(new SqlParameter("@tComprobante", tComprobante));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para insertar en la tabla la factura
        public void eliminarPago()
        {
            try
            {
                comando = "DELETE FROM detallesPagoFacturas WHERE idfactura = @idfactura AND idcc = @idcc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para buscar el pago a generar
        public DataTable buscarPago()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "SELECT d.* FROM detallesPagoFacturas d WHERE idcc = @id AND tComprobante = @tComprobante ORDER BY tComprobante asc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@id", idpago));
                        cmd.Parameters.Add(new SqlParameter("@tComprobante", tComprobante));
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

        public void actualizarPago()
        {
            try
            {
                comando = "UPDATE pagosFactura SET cantidad_recibida = @importePagado, impSaldoAnt = @impAnterior, impSaldoInsoluto = @impsaldoInsoluto, impPagado = @importePagado ";
                comando += "WHERE idcc = @idcc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@importePagado", impPagado));
                        cmd.Parameters.Add(new SqlParameter("@impAnterior", ImpSaldoAnt));
                        cmd.Parameters.Add(new SqlParameter("@impsaldoInsoluto", impSaldoInsoluto));


                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string agregarDecimales(string numero = "")
        {
            if (numero.IndexOf(".") == -1)
            {
                numero = numero + ".00";
            }
            else
            {
                numero = numero;
            }
            return numero;
        }

        public string dosDecimales(string numero = "")
        {
            string decimales;
            string[] partes = numero.Split('.');
            if (numero.IndexOf(".") > 0)
            {
                digitos = numero.Substring(numero.IndexOf(".") + 1);

                //		'if para validar si tiene un solo digito
                if (digitos.Length == 1)
                {
                    numero = numero + "0";

                }
                else
                {
                    // numero = numero.Substring(0, (numero.IndexOf(".") - 1));
                    decimales = digitos.Substring(0, 2);
                    numero = partes[0] + "." + decimales;
                }
            }
            else
            {
                numero = numero + ".00";
            }


            return numero;
        }

        public void buscarCteEmp()
        {
            try
            {
                comando = "SELECT f.idcliente FROM factura f WHERE idfactura=@idfactura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idCliente = int.Parse(reader["idcliente"].ToString());
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

        public void buscarCteCorreo()
        {
            try
            {
                comando = "SELECT * FROM clientes WHERE idcliente=@idcliente";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    correo = (reader["correo"].ToString());
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

        public void buscarCadenaConx()
        {
            try
            {
                comando = "SELECT * FROM configmenor WHERE  idconfig = 6";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cadenaConex = (reader["cadena"].ToString());
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

        public void actualizarFolio()
        {
            try
            {
                comando = "UPDATE pagosFactura SET folio = @folioActivo WHERE idcc = @idcc AND idfactura = @idfactura";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));

                        cmd.Parameters.Add(new SqlParameter("@folioActivo", folioActivo));


                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool ValidarIngreso()
        {
            try
            {
                comando = "SELECT isNULL (sum(d.impPagado), 0) AS impPagado FROM detallesPagoFacturas d INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE ";
                comando += "d.idfactura = @idfactura AND p.timbre = 'NO'";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    impPagado = double.Parse(reader["impPagado"].ToString());
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
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool ValidarDetallesPago()
        {
            try
            {
                comando = "SELECT * FROM detallesPagoFacturas d WHERE d.idfactura = @idfactura AND idcc = @idPago";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@idPago", idpago));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = int.Parse(reader["folio"].ToString());
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
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void compararMonedaPago()
        {
            try
            {
                comando = "SELECT * FROM pagosFactura WHERE  idcc = @idcc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    monedaPago = (reader["moneda"].ToString());
                                    cambioP = double.Parse(reader["tipoCambioDR"].ToString());
                                    formaPago = reader["formaPago"].ToString();
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

        public void compararMonedaPagoDet()
        {
            try
            {
                comando = "SELECT * FROM detallesPagoFacturas WHERE  idcc = @idcc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // monedaPago = (reader["moneda"].ToString());
                                    cambioP1 = double.Parse(reader["tipoCambioDR"].ToString());
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

        public DataTable tComprob()
        {
            try
            {
                DataTable dt = new DataTable();

                comando = "select distinct tComprobante FROM detallesPagoFacturas WHERE idcc = @idcc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
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

        public void compDetalles()
        {
            try
            {
                comando = "select  sum(impSaldoInsoluto) as impInsol, sum(impPagado) as impPag, sum(ImpSaldoAnt) as saldoAnt FROM detallesPagoFacturas WHERE idcc = @idcc AND ";
                comando += "tComprobante = @tcom ";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@tcom", tComprobante));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // monedaPago = (reader["moneda"].ToString());
                                    // formaPago = reader["formaPago"].ToString();
                                    impPagado = double.Parse(reader["impPag"].ToString());
                                    ImpSaldoAnt = double.Parse(reader["saldoAnt"].ToString());
                                    impSaldoInsoluto = double.Parse(reader["impInsol"].ToString());

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

        public bool valPagoDet()
        {
            try
            {
                comando = "Select * from detallesPagoFacturas WHERE idfactura= @idfactura AND idcc = @idcc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                //while (reader.Read())
                                //{
                                //  impPagado = double.Parse(reader["impPagado"].ToString());
                                //}
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

        public void actualizarDetalles()
        {
            try
            {
                comando = "UPDATE detallesPagoFacturas SET impPagado = @importePagado, ImpSaldoAnt = @impAnterior, impSaldoInsoluto = @impsaldoInsoluto ";
                comando += "WHERE idcc = @idcc and folio = @folio";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@importePagado", impPagado));
                        cmd.Parameters.Add(new SqlParameter("@impAnterior", ImpSaldoAnt));
                        cmd.Parameters.Add(new SqlParameter("@impsaldoInsoluto", impSaldoInsoluto));


                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void seleccionarDetallesPago()
        {
            try
            {
                comando = "SELECT * FROM detallesPagoFacturas WHERE idfactura = @idfactura AND idcc = @cc";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@cc", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    noParcialidadDetI = int.Parse(reader["noParcialidad"].ToString());
                                    folioDetI = int.Parse(reader["folio"].ToString());
                                    serieDetI = (reader["serie"].ToString());
                                    importePagadoDetI = float.Parse(reader["impPagado"].ToString());
                                    moneda = reader["moneda"].ToString();
                                    formaPago = reader["formaPago"].ToString();
                                    //ombreCliente = reader["nombrecliente"].ToString();
                                    // rfcCliente = reader["rfcCliente"].ToString();
                                    cambio = double.Parse(reader["tipoCambioDR"].ToString());
                                    idDocumento = reader["idDocumento"].ToString();
                                    //  cadena_original = reader["cadena_original"].ToString();
                                    tComprobante = int.Parse(reader["tComprobante"].ToString());
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

        public void buscarPagoD()
        {
            try
            {
                comando = "Select * from pagosFactura WHERE idcc = @idpago";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        // cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        cmd.Parameters.Add(new SqlParameter("@idpago", idpago));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    moneda = reader["moneda"].ToString();
                                    tipoCambioDR = double.Parse(reader["tipoCambioDR"].ToString());
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

        public void compTipoCambioDet()
        {
            try
            {
                comando = "SELECT * FROM detallesPagoFacturas WHERE idfactura = @idfactura AND idcc = @idcc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcc", idpago));
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // monedaPago = (reader["moneda"].ToString());
                                    cambioP1 = double.Parse(reader["tipoCambioDR"].ToString());
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

        public void buscarCarpeta()
        {
            try
            {
                comando = "SELECT e.carpetaPagos FROM empresas e INNER JOIN factura f ON e.idEmpresa = f.idempresa WHERE f.idfactura = @idfactura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // monedaPago = (reader["moneda"].ToString());
                                    carpetaPago = (reader["carpetaPagos"].ToString());
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

        public bool seleccionarParcialidad()
        {
            try
            {
                comando = "SELECT MAX(d.noParcialidad) as noParcialidad, d.idfactura FROM detallesPagoFacturas d INNER JOIN pagosFactura p ON  p.idcc = d.idcc WHERE d.idfactura = @idfactura ";
                comando += "AND p.timbre = 'NO' AND p.estatus <> 'Cancelado' GROUP BY d.idfactura ";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@idfactura", idfactura));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    parcialidad = int.Parse(reader["noParcialidad"].ToString());
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
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}