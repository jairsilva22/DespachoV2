using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sellado.Clases
{
    class Configuraciones
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades de la clase clientes que son los campos
        //necesarios para la generacion del XML
        public string path { get; set; }
        public int idEmpresa { get; set; }

        //constructor
        public Configuraciones()
        {
            path = "";
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            comando = "";
        }

        //metodo para buscar las configuraciones
        public void buscarConfig()
        {
            try
            {
                comando = "SELECT path FROM dbo.configmenor WHERE idempresa = @id";
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
                                    path = reader["path"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }

        //metodo para limpiar variables
        public void limpiar()
        {
            path = "";
        }
    }
}
