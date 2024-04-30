using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cFactura
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public long idfactura { get; set; }
        public int idcliente { get; set; }
        public int folio { get; set; }
        public DateTime fecha { get; set; }
        public int idempresa { get; set; }
        public float iva { get; set; }
        public float subtotal { get; set; }
        public float total { get; set; }
        public string serie { get; set; }
        public string estatus { get; set; }
        public int idusuario { get; set; }
        public DateTime fechaAlta { get; set; }
        public string forma_pago { get; set; }
        public string metodo_pago { get; set; }
        public int tipo_comprobante { get; set; }
        public DateTime fechaCfd { get; set; }
        public string abreviatura { get; set; }
        public string tasa { get; set; }
        public string asn { get; set; }
        public string vendedor { get; set; }
        public string ordenCompra { get; set; }
        public string va { get; set; }
        public string retencion { get; set; }
        public string obsCliente { get; set; }
        public int moneda { get; set; }
        public int idSolicitud { get; set; }
        public float cambio { get; set; }
        public float tretencion { get; set; }
        public string metodoPago { get; set; }
        public string NumCtaPago { get; set; }
        public float tcambio { get; set; }
        public string condicionesDePago { get; set; }
        public string nombreClnte { get; set; }
        public string uuid { get; set; }
        public string sello { get; set; }
        public string certificado { get; set; }
        public string cadenaOrig { get; set; }
        public int estadoComprobante { get; set; }
        public string terminos { get; set; }
        public string selloCFD { get; set; }
        public string embarque { get; set; }
        public string nfactura { get; set; }
        //comercio exterior
        public string certificadoOrigen { get; set; }
        public string claveDePedimento { get; set; }
        public string numCertificadoOrigen { get; set; }
        public string numExportadorConfiable { get; set; }
        public string incoterm { get; set; }
        public string subdivision { get; set; }
        public string tipoCambioUsd { get; set; }
        public string tipoOperacion { get; set; }
        public string totalUsd { get; set; }
        public string observaciones { get; set; }
        public string numero_certificado { get; set; }
        public string ocupada { get; set; }
        public long relacionado { get; set; }
        public string numRegIdTrib { get; set; }
        public string motivoTraslado { get; set; }
        //comercio exterior destino
        public string nombreDestino { get; set; }
        public string numRegIdTribDestino { get; set; }
        public string paisDestino { get; set; }
        public string municipioDestino { get; set; }
        public string estadoDestino { get; set; }
        public string codigoPostalDestino { get; set; }
        public string calleDestino { get; set; }
        public string exteriorDestino { get; set; }
        public string interiorDestino { get; set; }
        public string coloniaDestino { get; set; }
        public string localidadDestino { get; set; }
        public string referenciaDestino { get; set; }
        public string estCobranza { get; set; }
        public string estatusCobranza { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string codigoCan { get; set; }

        //archivos pdf y xml 
        public string PDF { get; set; }
        public string XML { get; set; }
        public string status { get; set; }

        //constructor
        public cFactura()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para obtener todos los datos de la factura
        public void buscarFactura()
        {
            try
            {
                comando = "SELECT idCliente, idempresa, iva, subtotal, total, serie, idusuario, forma_pago, metodo_pago, tipo_comprobante, UUID, ";
                comando += "tasa, asn, vendedor, ordenCompra, VA, retencion, obsCliente, moneda, cambio, tretencion, metodoPago, NumCtaPago, ";
                comando += "condicionesDePago, numero_certificado, folio, estatus FROM factura WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idcliente = int.Parse(reader["idcliente"].ToString());
                                    idempresa = int.Parse(reader["idempresa"].ToString());
                                    //iva = int.Parse(reader["iva"].ToString());
                                    subtotal = float.Parse(reader["subtotal"].ToString());
                                    total = float.Parse(reader["total"].ToString());
                                    serie = reader["serie"].ToString();
                                    idusuario = int.Parse(reader["idusuario"].ToString());
                                    forma_pago = reader["forma_pago"].ToString();
                                    //metodo_pago = reader["metodo_pago"].ToString();
                                    tipo_comprobante = int.Parse(reader["tipo_comprobante"].ToString());
                                    tasa = (reader["tasa"].ToString());
                                    asn = reader["asn"].ToString();
                                    //vendedor = int.Parse(reader["vendedor"].ToString());
                                    //ordenCompra = reader["ordenCompra"].ToString();
                                    //va = double.Parse(reader["VA"].ToString());
                                    //retencion = double.Parse(reader["retencion"].ToString());
                                    obsCliente = reader["obsCliente"].ToString();
                                    moneda = int.Parse(reader["moneda"].ToString());
                                    //cambio = float.Parse(reader["cambio"].ToString());
                                    //tretencion = double.Parse(reader["tretencion"].ToString());
                                    metodoPago = reader["metodoPago"].ToString();
                                    NumCtaPago = reader["NumCtaPago"].ToString();
                                    condicionesDePago = reader["condicionesDePago"].ToString();
                                    uuid = reader["UUID"].ToString();
                                    estatus = reader["estatus"].ToString();
                                    numero_certificado = reader["numero_certificado"].ToString();
                                    if(reader["folio"].ToString() != "")
                                    {
                                        folio = int.Parse(reader["folio"].ToString());
                                    }
                                    else
                                    {
                                        folio = 0;
                                    }
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

        //buscar pdf y xml 
        public bool buscarArchivos()
        {
            try
            {
                comando = "SELECT Top(1) * FROM log_idCO WHERE id_factura = @id ORDER BY id_LogCo DESC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    PDF = reader["archivo_pdf"].ToString();
                                    XML = reader["nombre_archivo"].ToString();
                                    status = reader["status"].ToString();
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

        //metodo para insertar en la tabla la factura
        public void insertarFactura()
        {
            try
            {
                comando = "INSERT INTO factura(idcliente, fecha, idempresa, iva, subtotal, total, estatus, idusuario, fechaAlta, forma_pago, ";
                comando += "tipo_comprobante, fechacfd, tasa, ASN, vendedor, ordenCompra, VA, retencion, obsCliente, moneda, cambio, tretencion, ";
                comando += "metodoPago, estCobranza, estatusCobranza, abreviatura, serie, idSolicitud) ";
                comando += "VALUES(@idcliente, GETDATE(), @idempresa, @iva, @subtotal, @total, 'Pendiente', @idusuario, GETDATE(), @forma_pago, ";
                comando += "@tipo_comprobante, GETDATE(), @tasa, @ASN, @vendedor, @ordenCompra, @VA, @retencion, @obsCliente, @moneda, @cambio, @tretencion,";
                comando += "@metodoPago, @estCobranza, @estatusCobranza, @abreviatura, @serie, @idSolicitud); SELECT SCOPE_IDENTITY() AS id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idcliente));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idusuario", idusuario));
                        cmd.Parameters.Add(new SqlParameter("@idempresa", idempresa));
                        cmd.Parameters.Add(new SqlParameter("@forma_pago", forma_pago));
                        cmd.Parameters.Add(new SqlParameter("@tipo_comprobante", tipo_comprobante));
                        cmd.Parameters.Add(new SqlParameter("@tasa", tasa));
                        cmd.Parameters.Add(new SqlParameter("@ASN", asn));
                        cmd.Parameters.Add(new SqlParameter("@vendedor", vendedor));
                        cmd.Parameters.Add(new SqlParameter("@ordenCompra", ordenCompra));
                        cmd.Parameters.Add(new SqlParameter("@VA", va));
                        cmd.Parameters.Add(new SqlParameter("@retencion", retencion));
                        cmd.Parameters.Add(new SqlParameter("@obsCliente", obsCliente));
                        cmd.Parameters.Add(new SqlParameter("@moneda", moneda));
                        cmd.Parameters.Add(new SqlParameter("@cambio", cambio));
                        cmd.Parameters.Add(new SqlParameter("@tretencion", tretencion));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@estCobranza", estCobranza));
                        cmd.Parameters.Add(new SqlParameter("@estatusCobranza", estatusCobranza));
                        cmd.Parameters.Add(new SqlParameter("@abreviatura", abreviatura));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        //cmd.Parameters.Add(new SqlParameter("@NumCtaPago", NumCtaPago));
                        //cmd.Parameters.Add(new SqlParameter("@condicionesDePago", condicionesDePago));
                        //int filas = cmd.ExecuteNonQuery();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idfactura = int.Parse(reader["id"].ToString());
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

        public void insertarFacturaXML()
        {
            try
            {
                comando = "INSERT INTO factura(idcliente, fechasellado, idempresa, iva, subtotal, total, estatus, idusuario, fechaAlta, forma_pago, metodo_pago, ";
                comando += "tipo_comprobante, fechacfd, tasa, ASN, vendedor, ordenCompra, VA, retencion, obsCliente, moneda, cambio, tretencion, estadoComprobante, ";
                comando += "metodoPago, NumCtaPago, condicionesDePago, sello, numero_certificado, certificado, serie, folio, abreviatura, cadena_original, fEmbarque, ";
                comando += "terminos, selloSAT, embarque, UUID, nfactura, timbre, generaPDF, Imprimir, estatusCobranza, estCobranza) ";
                comando += "VALUES(@idcliente, @fecha, @idempresa, @iva, @subtotal, @total, 'Facturada', @idusuario, @fecha, @forma_pago, @metodo_pago, ";
                comando += "@tipo_comprobante, @fecha, @tasa, @ASN, @vendedor, @ordenCompra, @VA, @retencion, @obsCliente, @moneda, @cambio, @tretencion, @edoCom, ";
                comando += "@metodoPago, @NumCtaPago, @condicionesDePago, @sello, @noCert, @cert, @serie, @folio, @abrev, @orig, @fecha, @term, @cfd, @emb, @uuid, @nfac, 'NO', 0, 'SI', 'Pendiente', 'Pendiente'); ";
                comando += "SELECT SCOPE_IDENTITY() as id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idcliente", idcliente));
                        cmd.Parameters.Add(new SqlParameter("@idempresa", idempresa));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idusuario", idusuario));
                        cmd.Parameters.Add(new SqlParameter("@forma_pago", forma_pago));
                        cmd.Parameters.Add(new SqlParameter("@metodo_pago", metodo_pago));
                        cmd.Parameters.Add(new SqlParameter("@tipo_comprobante", tipo_comprobante));
                        cmd.Parameters.Add(new SqlParameter("@tasa", tasa));
                        cmd.Parameters.Add(new SqlParameter("@ASN", asn));
                        cmd.Parameters.Add(new SqlParameter("@vendedor", vendedor));
                        cmd.Parameters.Add(new SqlParameter("@ordenCompra", ordenCompra));
                        cmd.Parameters.Add(new SqlParameter("@VA", va));
                        cmd.Parameters.Add(new SqlParameter("@retencion", retencion));
                        cmd.Parameters.Add(new SqlParameter("@obsCliente", obsCliente));
                        cmd.Parameters.Add(new SqlParameter("@moneda", moneda));
                        cmd.Parameters.Add(new SqlParameter("@cambio", cambio));
                        cmd.Parameters.Add(new SqlParameter("@tretencion", tretencion));
                        cmd.Parameters.Add(new SqlParameter("@edoCom", estadoComprobante));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@NumCtaPago", NumCtaPago));
                        cmd.Parameters.Add(new SqlParameter("@condicionesDePago", condicionesDePago));
                        cmd.Parameters.Add(new SqlParameter("@sello", sello));
                        cmd.Parameters.Add(new SqlParameter("@noCert", numero_certificado));
                        cmd.Parameters.Add(new SqlParameter("@cert", certificado));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@abrev", abreviatura));
                        cmd.Parameters.Add(new SqlParameter("@orig", cadenaOrig));
                        cmd.Parameters.Add(new SqlParameter("@term", terminos));
                        cmd.Parameters.Add(new SqlParameter("@cfd", selloCFD));
                        cmd.Parameters.Add(new SqlParameter("@emb", embarque));
                        cmd.Parameters.Add(new SqlParameter("@uuid", uuid));
                        cmd.Parameters.Add(new SqlParameter("@nfac", nfactura));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fechaAlta));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idfactura = long.Parse(reader["id"].ToString());
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

        //metodo para actualizar los totales de la factura
        public void actualizarTotales(double iva, double subtotal, double total)
        {
            try
            {
                comando = "UPDATE factura SET iva = @iva, subtotal = @sub, total = @total WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el ultimo id generado al insertar la factura
        public void ultimaFactura()
        {
            try
            {
                comando = "SELECT MAX(idfactura) AS id FROM factura ORDER BY id DESC";
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
                                    idfactura = int.Parse(reader["id"].ToString());
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

        //metodo para obtener el nombre del cliente de la factura
        public string nombreCliente(object factura)
        {
            try
            {
                comando = "SELECT nombreCliente FROM clientes JOIN factura ON factura.idcliente = clientes.idcliente WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", factura.ToString()));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreClnte = reader["nombreCliente"].ToString();
                                }
                            }
                            return nombreClnte;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar el estatus de la factura
        public void modificarEstatus()
        {
            try
            {
                comando = "UPDATE factura SET estatus = @estatus WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener la moneda
        public int obtenerMoneda(string dato)
        {
            int id = 0;
            try
            {
                comando = "SELECT idmd FROM moneda WHERE cMoneda = @mon";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mon", dato));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["idmd"].ToString());
                                }
                            }
                        }
                    }
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int obtenerCantidadDetallesFactura(int idfactura)
        {
            int cantidad = 0;
            try
            {
                comando = "SELECT COUNT(id_detFactura) as cantidad FROM detFactura WHERE id_factura = @id_factura";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id_factura", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cantidad = int.Parse(reader["cantidad"].ToString());
                                }
                            }
                        }
                    }
                    return cantidad;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para buscar la factura si ya se insertó
        public bool existeFactura(string dato)
        {
            try
            {
                comando = "SELECT idfactura FROM factura WHERE UUID LIKE '%" + dato + "%' AND estatus <> 'Cancelada'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@uid", dato));
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

        //comercio exterior

        //metodo para buscar los campos de comercio exterior
        public void camposComercioExterior()
        {
            try
            {
                comando = "SELECT idcliente, CertificadoOrigen, ClaveDePedimento, Incoterm, Subdivision, TipoCambioUSD, TotalUSD, TipoOperacion,";
                comando += " NumCertificadoOrigen, NumeroExportadorConfiable, Observaciones, abreviatura, numRegIdTrib, total FROM factura WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idcliente = int.Parse(reader["idcliente"].ToString());
                                    certificadoOrigen = (reader["CertificadoOrigen"].ToString());
                                    claveDePedimento = reader["ClaveDePedimento"].ToString();
                                    incoterm = reader["Incoterm"].ToString();
                                    subdivision = (reader["Subdivision"].ToString());
                                    tipoCambioUsd = (reader["TipoCambioUSD"].ToString());
                                    totalUsd = (reader["TotalUSD"].ToString());
                                    tipoOperacion = (reader["TipoOperacion"].ToString());
                                    numCertificadoOrigen = reader["NumCertificadoOrigen"].ToString();
                                    numExportadorConfiable = reader["NumeroExportadorConfiable"].ToString();
                                    observaciones = reader["Observaciones"].ToString();
                                    abreviatura = reader["abreviatura"].ToString();
                                    numRegIdTrib = reader["numRegIdTrib"].ToString();
                                    total = float.Parse(reader["total"].ToString());
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

        //metodo para buscar los campos del destinatario de traslado
        public void camposDestinatario()
        {
            try
            {
                comando = "SELECT nombreDestino, numRegIdTribDestino, paisDestino, municipioDestino, estadoDestino, codigoPostalDestino, calleDestino, ";
                comando += "exteriorDestino, interiorDestino, coloniaDestino, localidadDestino, referenciaDestino FROM factura WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreDestino = reader["nombreDestino"].ToString();
                                    numRegIdTribDestino = reader["numRegIdTribDestino"].ToString();
                                    municipioDestino = reader["municipioDestino"].ToString();
                                    paisDestino = reader["paisDestino"].ToString();
                                    estadoDestino = reader["estadoDestino"].ToString();
                                    codigoPostalDestino = reader["codigoPostalDestino"].ToString();
                                    calleDestino = reader["calleDestino"].ToString();
                                    exteriorDestino = reader["exteriorDestino"].ToString();
                                    interiorDestino = reader["interiorDestino"].ToString();
                                    coloniaDestino = reader["coloniaDestino"].ToString();
                                    localidadDestino = reader["localidadDestino"].ToString();
                                    referenciaDestino = reader["referenciaDestino"].ToString();
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

        //metodo para actualizar los campos de comercio exterior
        public void actualizarComercioExterior()
        {
            try
            {
                comando = "UPDATE factura SET CertificadoOrigen = @certificado, ClaveDePedimento = @clave, Incoterm = @incoterm, Subdivision = @subdivision, ";
                comando += "TipoCambioUSD = @tipoUSD, TipoOperacion = @operacion, TotalUSD = @totalusd, Observaciones = @observ, NumCertificadoOrigen = @num, ";
                comando += "NumeroExportadorConfiable = @exp, numRegIdTrib = @reg, motivoTraslado = @tras WHERE idfactura = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@certificado", certificadoOrigen));
                        cmd.Parameters.Add(new SqlParameter("@clave", claveDePedimento));
                        cmd.Parameters.Add(new SqlParameter("@incoterm", incoterm));
                        cmd.Parameters.Add(new SqlParameter("@subdivision", subdivision));
                        cmd.Parameters.Add(new SqlParameter("@tipoUSD", tipoCambioUsd));
                        cmd.Parameters.Add(new SqlParameter("@operacion", tipoOperacion));
                        cmd.Parameters.Add(new SqlParameter("@totalusd", totalUsd));
                        cmd.Parameters.Add(new SqlParameter("@observ", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@num", numCertificadoOrigen));
                        cmd.Parameters.Add(new SqlParameter("@exp", numExportadorConfiable));
                        cmd.Parameters.Add(new SqlParameter("@reg", numRegIdTrib));
                        cmd.Parameters.Add(new SqlParameter("@tras", motivoTraslado));
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para mostrar todas las facturas que son anticipos sin utilizar
        public DataTable facturasAnticipo()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT factura.idfactura, factura.idcliente, factura.folio, factura.cambio, factura.uuid, idmoneda, moneda.cMoneda,";
                comando += " clientes.nombreCliente, clientes.rfcCliente, cadena_original, uuid FROM factura INNER JOIN  clientes ON factura.idcliente = clientes.idCliente";
                comando += " JOIN moneda ON idmoneda = moneda WHERE tipo_comprobante = 4 AND timbre = 'NO' AND estatus <> 'Cancelada' AND factura.idcliente = @cliente AND relacionado IS NULL ORDER BY folio";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cliente", idcliente));
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

        //metodo para actualizar el metodo de pago y forma de pago del pago del anticipo
        public void actualizarAnticipo()
        {
            try
            {
                comando = "UPDATE factura SET relacionado = @rel WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@rel", relacionado));
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar el metodo de pago y forma de pago del pago del anticipo
        public void actualizarOcupado()
        {
            try
            {
                comando = "UPDATE factura SET ocupado = 'NO' WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar la parte de destinatario para el traspaso
        public void actualizarDestino()
        {
            try
            {
                comando = "UPDATE factura SET nombreDestino = @nom, numRegIdTribDestino = @reg, paisDestino = @pais, municipioDestino = @mun,";
                comando += " estadoDestino = @edo, codigoPostalDestino = @cp, calleDestino = @calle, exteriorDestino = @ext, interiorDestino = @int, ";
                comando += " coloniaDestino = @col, localidadDestino = @loc, referenciaDestino = @ref WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nom", nombreDestino));
                        cmd.Parameters.Add(new SqlParameter("@reg", numRegIdTribDestino));
                        cmd.Parameters.Add(new SqlParameter("@pais", paisDestino));
                        cmd.Parameters.Add(new SqlParameter("@mun", municipioDestino));
                        cmd.Parameters.Add(new SqlParameter("@edo", estadoDestino));
                        cmd.Parameters.Add(new SqlParameter("@cp", codigoPostalDestino));
                        cmd.Parameters.Add(new SqlParameter("@calle", calleDestino));
                        cmd.Parameters.Add(new SqlParameter("@ext", exteriorDestino));
                        cmd.Parameters.Add(new SqlParameter("@int", interiorDestino));
                        cmd.Parameters.Add(new SqlParameter("@col", coloniaDestino));
                        cmd.Parameters.Add(new SqlParameter("@loc", localidadDestino));
                        cmd.Parameters.Add(new SqlParameter("@ref", referenciaDestino));
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

        //metodo para actualizar el tipo de documento
        public void actualizarTipoDocumento(int tipo, string abrev)
        {
            try
            {
                comando = "UPDATE factura SET tipo_comprobante = @tipo, abreviatura = @abrev WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@abrev", abrev));
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


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //TIMBRE-----------------------------------------------------------
        //metodo para buscar la factura a procesar
        public void buscarFacturaTimbre()
        {
            try
            {
                comando = "SELECT * FROM dbo.factura WHERE idfactura = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idempresa = int.Parse(reader["idempresa"].ToString());
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

        //metodo para actualizar a status procesando la factura
        public void actualizarError()
        {
            try
            {
                comando = "UPDATE dbo.factura SET timbre = 'Error' WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //metodo para actualizar a status procesando la factura
        public void actualizarStatusTimbre()
        {
            try
            {
                comando = "UPDATE dbo.factura SET generaPDF = 1, timbre = 'NO' WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void datosFactura()
        {
            try
            {
                comando = "SELECT A.folio, B.nombreCliente, B.rfcCliente, A.UUID, A.fechasellado, A.total, A.estatus, ISNULL(A.CodigoCan, '') AS CodigoCan, A.idempresa FROM factura AS A JOIN Clientes AS B " +
                    "ON B.idCliente = A.idCliente WHERE A.idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = int.Parse(reader["folio"].ToString());
                                    vendedor = reader["nombreCliente"].ToString();
                                    ordenCompra = reader["rfcCliente"].ToString();
                                    uuid = reader["UUID"].ToString();
                                    fechaAlta = DateTime.Parse(reader["fechasellado"].ToString());
                                    total = float.Parse(reader["total"].ToString());
                                    estatus = reader["estatus"].ToString();
                                    codigoCan = reader["CodigoCan"].ToString();
                                    idempresa = int.Parse(reader["idempresa"].ToString());
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

        //metodo para cancelar la factura por completo
        public void cancelacionFactura()
        {
            try
            {
                comando = "UPDATE factura SET estatus = 'Cancelada', estadoComprobante = 0, generaPDF = 1 WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
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

        //metodo para cancelar una factura
        public void procesoCancelar()
        {
            try
            {
                comando = "UPDATE factura SET codigoCan = @cod, fechaSolicitud = @fecha, estatus = 'PCancelada' WHERE idfactura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cod", codigoCan));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fechaSolicitud));
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

        //metodo para saber el nombre del archivo xml
        public void nombreXml()
        {
            try
            {
                comando = "SELECT nombre_archivo FROM log_idCO WHERE id_factura = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idfactura));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    abreviatura = reader["nombre_archivo"].ToString();
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

        public DataTable añoFactura()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT DISTINCT YEAR(fechasellado) AS fecha FROM Factura WHERE fechasellado <> '' ORDER BY fecha DESC";
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
        public bool existeFolioFacturaSolicitud(int idSolicitud)
        {
            try
            {
                comando = "SELECT factura.folio FROM factura, ordenes WHERE factura.idSolicitud = ordenes.idSolicitud " +
                    "  AND factura.folio IS NOT NULL AND factura.idSolicitud = @idSolicitud";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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
        public string obtenerFolioSerie(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CONCAT(serie,factura.folio) AS serieFolio FROM factura, ordenes" +
                    " WHERE factura.idSolicitud = ordenes.idSolicitud AND factura.idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int obtenerIdClienteFacturaByID(int idfactura)
        {
            int idcliente = 0;
            try
            {
                comando = "select idcliente from factura where idfactura = @idfactura";
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
                                    idcliente = int.Parse(reader["idcliente"].ToString());
                                }
                            }
                        }
                    }
                    return idcliente;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool existeSolicitudFacturada(int idSolicitud)
        {
            try
            {
                comando = "SELECT TOP(1) idCliente, idfactura FROM factura WHERE idSolicitud = @idSolicitud";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idfactura = int.Parse(reader["idfactura"].ToString());
                                    idcliente = int.Parse(reader["idCliente"].ToString());
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
        public int obtenerIdClienteFacturaByIDSolicitud(int idSolicitud)
        {
            int idcliente = 0;
            try
            {
                comando = "select idcliente from factura where idSolicitud = @idSolicitud";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idcliente = int.Parse(reader["idcliente"].ToString());
                                }
                            }
                        }
                    }
                    return idcliente;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}