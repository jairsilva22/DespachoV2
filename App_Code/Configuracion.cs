using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Configuracion
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public int idconfig { get; set; }
        public double iva { get; set; }
        public string path { get; set; }
        public int idempresa { get; set; }
        public string Path_arch_utiles { get; set; }
        public string smtp { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string cadenaConfig { get; set; }
        public double valorAgregado { get; set; }
        public string prueba { get; set; }
        public string correoVista { get; set; }
        public string asuntosmtp { get; set; }
        public string mensajesmtp { get; set; }
        public string mensaje1 { get; set; }
        public string invitado { get; set; }

        //constructor
        public Configuracion()
        {
            path = "";
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            comando = "";
        }

        #region Procesos
        //metodo para obtener los datos de configuracion
        public void configProcesos()
        {
            try
            {
                comando = "SELECT * FROM dbo.configmenor WHERE idempresa = @idEmpresa";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpresa", idempresa));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idconfig = int.Parse(reader["idconfig"].ToString());
                                    iva = double.Parse(reader["iva"].ToString());
                                    path = reader["path"].ToString();
                                    idempresa = int.Parse(reader["idempresa"].ToString());
                                    Path_arch_utiles = reader["Path_arch_utiles"].ToString();
                                    smtp = reader["smtp"].ToString();
                                    correo = reader["correo"].ToString();
                                    password = reader["password"].ToString();
                                    cadenaConfig = reader["cadena"].ToString();
                                    valorAgregado = double.Parse(reader["valorAgregado"].ToString());
                                    prueba = reader["prueba"].ToString();
                                    correoVista = reader["correoVista"].ToString();
                                    // asuntosmtp = reader["asuntosmtp"].ToString();
                                    mensajesmtp = reader["mensajesmtp"].ToString();
                                    //  mensaje1 = reader["Mensaje1"].ToString();
                                    ////invitado = reader["invitado"].ToString();
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
        #endregion

        //metodo para buscar las configuraciones
        public void buscarConfig()
        {
            try
            {
                comando = "SELECT path FROM dbo.configmenor WHERE idconfig = 6";
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
                                    path = reader["path"].ToString();
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

        //metodo para limpiar variables
        public void limpiar()
        {
            path = "";
        }
    }
}