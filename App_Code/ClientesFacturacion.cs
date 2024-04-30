using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class ClientesFacturacion
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //Propiedades
        public string nombreEmpresa { get; set; }
        public string rfcCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string calleCliente { get; set; }
        public string nombreCliente { get; set; }
        public string metodoPago { get; set; }
        public string formaPago { get; set; }
        public int idMetodoPago { get; set; }
        public int idFormaPago { get; set; }
        public int idFactura { get; set; }
        public int paisCliente { get; set; }
        public int estadoCliente { get; set; }
        public int ciudadCliente { get; set; }
        public int idUsuario { get; set; }
        public int idEmpresa { get; set; }
        public string noExterior { get; set; }
        public string noInterior { get; set; }
        public string codigopostalCliente { get; set; }
        public string usoCFDI { get; set; }
        public string colonia { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }

        public ClientesFacturacion()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public void insertar()
        {
            try
            {
                comando = "INSERT INTO clientesFacturacion(nombreCliente, rfcCliente, estadoCliente, telefonoCliente, ciudadCliente, correo, colonia, calleCliente, codigopostalCliente, " +
                    "noExterior, noInterior, idusuario, fechaAlta, idempresa, nombreEmpresa, clave, valorAgregado, metodoPago, " +
                    " retencion, paisCliente, usoCFDI) ";
                comando += "VALUES(@nombre, @rfc, @estado, @telefono, @ciudad, @correo, @colonia, @calle, @cp, @noE, @noI, @idUsuario, GETDATE(), @idE, @nombreE, @clave, 0, @metodoP, 0, @pais, @usoCFDI) ";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfcCliente));
                        cmd.Parameters.Add(new SqlParameter("@estado", estadoCliente));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefonoCliente));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudadCliente));
                        cmd.Parameters.Add(new SqlParameter("@correo", correo));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@calle", calleCliente));
                        cmd.Parameters.Add(new SqlParameter("@cp", codigopostalCliente));
                        cmd.Parameters.Add(new SqlParameter("@noE", noExterior));
                        cmd.Parameters.Add(new SqlParameter("@noI", noInterior));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idE", idEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@nombreE", nombreEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@clave", clave));
                        cmd.Parameters.Add(new SqlParameter("@metodoP", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@pais", paisCliente));
                        cmd.Parameters.Add(new SqlParameter("@usoCFDI", usoCFDI));
                        
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool existeClienteEnFacturacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientesFacturacion WHERE nombreCliente = @nombre AND rfcCliente = @rfc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
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
        public DataTable consultarClienteFacturacion(string nombre, string rfc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientesFacturacion WHERE nombreCliente = @nombre AND rfcCliente = @rfc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfc));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
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
        public DataTable consultarClienteFacturacionByID(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientesFacturacion WHERE idCliente = @id", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
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