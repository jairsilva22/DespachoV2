using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellado.Clases
{
    class Cliente
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades
        public long idCliente { get; set; }
        public string nombreCliente { get; set; }

        public Cliente()
        {
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        //metodo para obtener el nombre del cliente
        public void datosCliente()
        {
            try
            {
                comando = "SELECT nombreCliente FROM ClientesFacturacion WHERE idCliente = @id";
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
                                    nombreCliente = reader["nombreCliente"].ToString();
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
    }
}
