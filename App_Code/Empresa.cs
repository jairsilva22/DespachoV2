using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Empresa
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propriedadaes
        public int idEmpresa { get; set; }
        public string nombre { get; set; }
        public string rfc { get; set; }
        public string telefono { get; set; }
        public string calle { get; set; }
        public int paisCliente { get; set; }
        public string nombrePais { get; set; }
        public int estadoCliente { get; set; }
        public string nombreEstado { get; set; }
        public int ciudadCliente { get; set; }
        public string nombreCiudad { get; set; }
        public string codigoPostal { get; set; }
        public string Ncarpeta { get; set; }
        public string nombreCer { get; set; }
        public string nombreKey { get; set; }
        public string logo { get; set; }
        public string ancho { get; set; }
        public string alto { get; set; }
        public string rfclogo { get; set; }
        public int idusuario { get; set; }
        public DateTime fechaAlta { get; set; }
        public string noExterior { get; set; }
        public string noInterior { get; set; }
        public string colonia { get; set; }
        public string referencia { get; set; }
        public string municipio { get; set; }
        public string contraseña { get; set; }
        public DateTime NoAfter { get; set; }
        public DateTime NoBefore { get; set; }
        public string anchoRfc { get; set; }
        public string altoRfc { get; set; }
        public string lugarExpedicion { get; set; }
        public string regimenFiscal { get; set; }
        public string imagenUUID { get; set; }
        public string anchoUUID { get; set; }
        public string carpetaTXT { get; set; }
        public string carpetaTimbre { get; set; }
        public string c_regimenFiscal { get; set; }
        public string correo { get; set; }
        public string carpetaPagos { get; set; }

        public Empresa()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para obtener las empresas
        public DataTable empresas()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idEmpresa, nombre FROM sucursales ORDER BY razon";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para buscar si el rfc del emisor es correcto
        public bool rfcEmpresa()
        {
            try
            {
                comando = "SELECT id FROM sucursales WHERE rfc = @rfc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfc));
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

        #region Procesos
        //metodo para obtener los datos de la empresa
        public void empresaProcesos()
        {
            try
            {
                comando = "SELECT rfc, nombre, calle, codigoPostal, pais.pais, estado.estado, ciudad.ciudad,";
                comando += " colonia, numero, c_regimenFiscal, Ncarpeta, carpetaPagos, carpetaTimbre, logo FROM dbo.sucursales AS empresas JOIN dbo.pais ON pais.idpais = empresas.idPais JOIN dbo.estados AS estado";
                comando += " ON estado.id = empresas.idEstado JOIN dbo.ciudades AS ciudad ON ciudad.id = empresas.idCiudad WHERE empresas.id = @empresa";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@empresa", idEmpresa));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lugarExpedicion = reader["nombre"].ToString();
                                    rfc = reader["rfc"].ToString();
                                    nombre = reader["nombre"].ToString();
                                    calle = reader["calle"].ToString();
                                    codigoPostal = reader["codigoPostal"].ToString();
                                    nombrePais = reader["pais"].ToString();
                                    nombreEstado = reader["estado"].ToString();
                                    nombreCiudad = reader["ciudad"].ToString();
                                    colonia = reader["colonia"].ToString();
                                    noExterior = reader["numero"].ToString();
                                    c_regimenFiscal = reader["c_regimenFiscal"].ToString();
                                    Ncarpeta = reader["Ncarpeta"].ToString();
                                    carpetaPagos = reader["carpetaPagos"].ToString();
                                    carpetaTimbre = reader["carpetaTimbre"].ToString();
                                    logo = reader["logo"].ToString();
                                }
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
        #endregion

        //SELLADO ------------------------------------------------------------------------------------------------------

        //metodo para buscar la direccion fisica del archivo
        public void archivoCer()
        {
            try
            {
                comando = "SELECT nombreCer, contrasenaArchivos, nombreKey FROM sucursales";
                comando += " WHERE id = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idEmpresa));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreCer = reader["nombreCer"].ToString();
                                    contraseña = reader["contrasenaArchivos"].ToString();
                                    nombreKey = reader["nombreKey"].ToString();
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

        public void limpiar()
        {
            idEmpresa = 0;
            nombreCer = "";
            contraseña = "";
        }
    }
}