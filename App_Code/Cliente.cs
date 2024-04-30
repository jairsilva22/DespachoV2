using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Cliente
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public long idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string rfcCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string calleCliente { get; set; }
        public int paisCliente { get; set; }
        public string nombrePais { get; set; }
        public string clavePais { get; set; }
        public int estadoCliente { get; set; }
        public string nombreEstado { get; set; }
        public int ciudadCliente { get; set; }
        public string nombreCiudad { get; set; }
        public string codigoPostalCliente { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaAlta { get; set; }
        public int idEmpresa { get; set; }
        public string noExterior { get; set; }
        public string noInterior { get; set; }
        public string colonia { get; set; }
        public string referencia { get; set; }
        public string municipio { get; set; }
        public string entrega { get; set; }
        public string nombreEmpresa { get; set; }
        public string correo { get; set; }
        public string terminoPago { get; set; }
        public string obsCliente { get; set; }
        public string clave { get; set; }
        public string destino { get; set; }
        public int tazaIva { get; set; }
        public bool retencion { get; set; }
        public string formaPago { get; set; }
        public bool valorAgregado { get; set; }
        public string metodoPago { get; set; }
        public string numeroCuenta { get; set; }
        public int diasCredito { get; set; }
        public bool enviarCorreo { get; set; }
        public string isr { get; set; }
        public string usoCFDI { get; set; }
        public string numRegIdTrib { get; set; }
        public string estatusCobranza { get; set; }
        public string truncar { get; set; }
        public double descuento { get; set; }

        public Cliente()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para saber el id del cliente de acuerdo al rfc del cliente
        public void obtenerID()
        {
            try
            {
                comando = "SELECT idCliente, obsCliente, numeroCuenta, entrega FROM ClientesFacturacion WHERE rfcCliente = @rfc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idCliente = int.Parse(reader["idCliente"].ToString());
                                    obsCliente = reader["obsCliente"].ToString();
                                    numeroCuenta = reader["numeroCuenta"].ToString();
                                    entrega = reader["entrega"].ToString();
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

        //metodo para saber si el rfc pertenece a volkswagen(solo KSR)
        public bool existeRFC()
        {
            try
            {
                comando = "SELECT IdCliente FROM ClientesFacturacion WHERE rfcCliente = @rfc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
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

        //metodo para buscar los campos para el xml
        public void clienteProcesos()
        {
            try
            {
                comando = "SELECT rfcCliente, isnull(retencion, 0) as retencion, nombreCliente, numRegIdTrib, isnull(tazaIva, 0) as tazaIva, calleCliente, noExterior, colonia,";
                comando += "  estado.estado, ciudad.ciudad, codigoPostalCliente, usoCFDI, pais.descripcion AS clavePais, isr,";
                comando += " truncar FROM dbo.clientesFacturacion AS clientes JOIN dbo.estados AS estado ON ";
                comando += " estado.id = clientes.estadoCliente JOIN dbo.ciudades AS ciudad ON ciudad.id = clientes.ciudadCliente WHERE";
                comando += " idCliente = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    rfcCliente = reader["rfcCliente"].ToString();
                                    // retencion = int.Parse(reader["retencion"].ToString());
                                    nombreCliente = reader["nombreCliente"].ToString();
                                    numRegIdTrib = reader["numRegIdTrib"].ToString();
                                    //tazaIva = int.Parse(reader["tazaIva"].ToString());
                                    calleCliente = reader["calleCliente"].ToString();
                                    noExterior = reader["noExterior"].ToString();
                                    colonia = reader["colonia"].ToString();
                                    nombrePais = "México";
                                    nombreEstado = reader["estado"].ToString();
                                    nombreCiudad = reader["ciudad"].ToString();
                                    codigoPostalCliente = reader["codigoPostalCliente"].ToString();
                                    usoCFDI = reader["usoCFDI"].ToString();
                                    clavePais = reader["clavePais"].ToString();
                                    //isr = reader["isr"].ToString();
                                    //truncar = reader["truncar"].ToString();
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

        //metodo para buscar los datos del cliente
        public void buscarCliente()
        {
            try
            {
                comando = "SELECT nombreCliente,usoCFDI,numRegIdTrib,truncar,obsCliente,clave, destino,ISNULL(tazaIva, 0) AS tazaIva,retencion,valorAgregado,metodoPago,numeroCuenta, ISNULL(diasCredito, 0) as diasCredito, ISNULL(enviarCorreo, 0) AS enviarCorreo,rfcCliente,telefonoCliente,calleCliente, estadoCliente, ciudadCliente, codigoPostalCliente, idEmpresa, noExterior, noInterior, colonia, referencia, municipio, entrega, nombreEmpresa, correo, terminoPago FROM clientesFacturacion WHERE idCliente = @clnt";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clnt", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreCliente = reader["nombreCliente"].ToString();
                                    rfcCliente = reader["rfcCliente"].ToString();
                                    telefonoCliente = reader["telefonoCliente"].ToString();
                                    calleCliente = reader["calleCliente"].ToString();
                                    //paisCliente = int.Parse(reader["paisCliente"].ToString());
                                    estadoCliente = int.Parse(reader["estadoCliente"].ToString());
                                    ciudadCliente = int.Parse(reader["ciudadCliente"].ToString());
                                    codigoPostalCliente = reader["codigoPostalCliente"].ToString();
                                    idEmpresa = int.Parse(reader["idEmpresa"].ToString());
                                    noExterior = reader["noExterior"].ToString();
                                    noInterior = reader["noInterior"].ToString();
                                    colonia = reader["colonia"].ToString();
                                    referencia = reader["referencia"].ToString();
                                    municipio = reader["municipio"].ToString();
                                    entrega = reader["entrega"].ToString();
                                    nombreEmpresa = reader["nombreEmpresa"].ToString();
                                    correo = reader["correo"].ToString();
                                    terminoPago = reader["terminoPago"].ToString();
                                    obsCliente = reader["obsCliente"].ToString();
                                    clave = reader["clave"].ToString();
                                    destino = reader["destino"].ToString();
                                    tazaIva = int.Parse(reader["tazaIva"].ToString());
                                    retencion = bool.Parse(reader["retencion"].ToString());
                                    valorAgregado = bool.Parse(reader["valorAgregado"].ToString());
                                    metodoPago = reader["metodoPago"].ToString();
                                    numeroCuenta = reader["numeroCuenta"].ToString();
                                    diasCredito = int.Parse(reader["diasCredito"].ToString());
                                    enviarCorreo = bool.Parse(reader["enviarCorreo"].ToString());
                                    usoCFDI = reader["usoCFDI"].ToString();
                                    numRegIdTrib = reader["numRegIdTrib"].ToString();
                                    truncar = reader["truncar"].ToString();
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

        //metodo para insertar en la tabla
        public void insertar()
        {
            try
            {
                comando = "INSERT INTO dbo.clientesFacturacion (nombreCliente, paisCliente, rfcCliente, estadoCliente, telefonoCliente, ciudadCliente, correo, colonia," +
                    " calleCliente, codigopostalCliente, noExterior, terminoPago, noInterior, entrega, obsCliente, idusuario, fechaAlta, idempresa, nombreEmpresa, " +
                    "clave, destino, retencion, tazaIva, metodoPago, numeroCuenta, valorAgregado, referencia, truncar, isr, usoCFDI) VALUES(@nombre, @pais, @rfc, @edo, @tel, @ciudad, " +
                    "@mail, @col, @calle, @cp, @ext, @termino, @inter, @entrega, @obs, @user, GETDATE(), @idemp, @empresa, @clave, @destino, @retencion, @iva, @metodo, " +
                    "@cuenta, @va, @referencia, @truncar, @isr, @uso)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
                        cmd.Parameters.Add(new SqlParameter("@pais", paisCliente));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
                        cmd.Parameters.Add(new SqlParameter("@edo", estadoCliente));
                        cmd.Parameters.Add(new SqlParameter("@tel", telefonoCliente));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudadCliente));
                        cmd.Parameters.Add(new SqlParameter("@mail", correo));
                        cmd.Parameters.Add(new SqlParameter("@col", colonia));
                        cmd.Parameters.Add(new SqlParameter("@calle", calleCliente));
                        cmd.Parameters.Add(new SqlParameter("@cp", codigoPostalCliente));
                        cmd.Parameters.Add(new SqlParameter("@ext", noExterior));
                        cmd.Parameters.Add(new SqlParameter("@termino", terminoPago));
                        cmd.Parameters.Add(new SqlParameter("@inter", noInterior));
                        cmd.Parameters.Add(new SqlParameter("@entrega", entrega));
                        cmd.Parameters.Add(new SqlParameter("@obs", obsCliente));
                        cmd.Parameters.Add(new SqlParameter("@user", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idemp", idEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@empresa", nombreEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@clave", clave));
                        cmd.Parameters.Add(new SqlParameter("@destino", destino));
                        cmd.Parameters.Add(new SqlParameter("@retencion", retencion));
                        cmd.Parameters.Add(new SqlParameter("@iva", tazaIva));
                        cmd.Parameters.Add(new SqlParameter("@metodo", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@cuenta", numeroCuenta));
                        cmd.Parameters.Add(new SqlParameter("@va", valorAgregado));
                        cmd.Parameters.Add(new SqlParameter("@referencia", referencia));
                        cmd.Parameters.Add(new SqlParameter("@desc", descuento));
                        cmd.Parameters.Add(new SqlParameter("@truncar", truncar));
                        cmd.Parameters.Add(new SqlParameter("@isr", isr));
                        cmd.Parameters.Add(new SqlParameter("@uso", usoCFDI));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para modificar en la tabla
        public void modificar()
        {
            try
            {
                comando = "UPDATE dbo.clientesFacturacion SET nombreCliente = @nombre, rfcCliente = @rfc, telefonoCliente = @tel, correo = @mail, calleCliente = @calle, " +
                    "paisCliente = @pais, estadoCliente = @edo, ciudadCliente = @ciudad, codigopostalCliente = @cp, noExterior = @ext, noInterior = @inter, " +
                    "colonia = @col, terminoPago = @termino, entrega = @entrega, obsCliente = @obs, nombreEmpresa = @empresa, clave = @clave, destino = @destino, " +
                    "retencion = @retencion, tazaIva = @iva, metodoPago = @metodo, numeroCuenta = @cuenta, valorAgregado = @va, referencia = @referencia, " +
                    "truncar = @truncar, isr = @isr, usoCFDI = @uso WHERE idCliente = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
                        cmd.Parameters.Add(new SqlParameter("@pais", paisCliente));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
                        cmd.Parameters.Add(new SqlParameter("@edo", estadoCliente));
                        cmd.Parameters.Add(new SqlParameter("@tel", telefonoCliente));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudadCliente));
                        cmd.Parameters.Add(new SqlParameter("@mail", correo));
                        cmd.Parameters.Add(new SqlParameter("@col", colonia));
                        cmd.Parameters.Add(new SqlParameter("@calle", calleCliente));
                        cmd.Parameters.Add(new SqlParameter("@cp", codigoPostalCliente));
                        cmd.Parameters.Add(new SqlParameter("@ext", noExterior));
                        cmd.Parameters.Add(new SqlParameter("@termino", terminoPago));
                        cmd.Parameters.Add(new SqlParameter("@inter", noInterior));
                        cmd.Parameters.Add(new SqlParameter("@entrega", entrega));
                        cmd.Parameters.Add(new SqlParameter("@obs", obsCliente));
                        cmd.Parameters.Add(new SqlParameter("@user", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idemp", idEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@empresa", nombreEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@clave", clave));
                        cmd.Parameters.Add(new SqlParameter("@destino", destino));
                        cmd.Parameters.Add(new SqlParameter("@retencion", retencion));
                        cmd.Parameters.Add(new SqlParameter("@iva", tazaIva));
                        cmd.Parameters.Add(new SqlParameter("@metodo", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@cuenta", numeroCuenta));
                        cmd.Parameters.Add(new SqlParameter("@va", valorAgregado));
                        cmd.Parameters.Add(new SqlParameter("@referencia", referencia));
                        cmd.Parameters.Add(new SqlParameter("@truncar", truncar));
                        cmd.Parameters.Add(new SqlParameter("@isr", isr));
                        cmd.Parameters.Add(new SqlParameter("@uso", usoCFDI));
                        cmd.Parameters.Add(new SqlParameter("@id", idCliente));

                        int fila = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para saber si existe en la tabla
        public bool existeCliente()
        {
            try
            {
                comando = "SELECT idCliente FROM ClientesFacturacion WHERE nombreCliente = @nombre AND rfcCliente = @rfc AND calleCliente = @calle";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
                        cmd.Parameters.Add(new SqlParameter("@calle", calleCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idCliente = int.Parse(reader["idCliente"].ToString());
                                }

                                return true;
                            }
                            else
                            {
                                idCliente = 0;
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

        public DataTable Clientes()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM clientesFacturacion order by nombreEmpresa";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@cliente", idcliente));
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
        public string obtenerTipoComisionByCliente(int idCliente)
        {
            string directo = "";
            try
            {
                comando = "SELECT directo FROM clientes WHERE id = @idCliente";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    directo = reader["directo"].ToString();
                                }
                            }
                        }
                    }
                    return directo;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}