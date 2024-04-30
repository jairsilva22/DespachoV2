using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sellado.Clases
{
    class Empresa
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades de la tabla de empresas
        public string nombreCer { get; set; }
        public int idempresa { get; set; }
        public string contraseña { get; set; }

        //constructor
        public Empresa()
        {
            idempresa = 0;
            nombreCer = "";
            contraseña = "";
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        //metodo para buscar la direccion fisica del archivo
        public void archivoCer()
        {
            try
            {
                comando = "SELECT nombreCer, contrasenaArchivos FROM sucursales";
                comando += " WHERE id = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idempresa));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreCer = reader["nombreCer"].ToString();
                                    contraseña = reader["contrasenaArchivos"].ToString();
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

        public void limpiar()
        {
            idempresa = 0;
            nombreCer = "";
            contraseña = "";
        }
    }
}
