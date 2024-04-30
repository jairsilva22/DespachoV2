using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sellado.Clases
{
    class Folios
    {
        //variables para uso de la clase
        string cadena;
        string comando;

        //propiedades de la clase clientes que son los campos
        //necesarios para la generacion del XML
        public long idLogs { get; set; }
        public long folioInicio { get; set; }
        public long folioFinal { get; set; }
        public long folioActivo { get; set; }
        public string serie { get; set; }

        //constructor
        public Folios()
        {
            idLogs = 0;
            folioInicio = 0;
            folioFinal = 0;
            folioActivo = 0;
            serie = "";
            cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            comando = "";
        }

        //metodo para buscar los folios
        public void buscarFolios()
        {
            try
            {
                comando = "SELECT idLogs, folioInicio, folioFinal, folioActivo, serie FROM dbo.folios WHERE idLogs = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idLogs));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idLogs = int.Parse(reader["idLogs"].ToString());
                                    folioInicio = long.Parse(reader["folioInicio"].ToString());
                                    folioFinal = long.Parse(reader["folioFinal"].ToString());
                                    folioActivo = long.Parse(reader["folioActivo"].ToString());
                                    serie = reader["serie"].ToString();
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

        //metodo para actualizar el folio activo
        public void actualizarFolio(long folio)
        {
            try
            {
                comando = "UPDATE folios SET folioActivo = @folio WHERE idLogs = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@id", idLogs));

                        int filasAfectadas = cmd.ExecuteNonQuery();
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
            idLogs = 0;
            folioInicio = 0;
            folioFinal = 0;
            folioActivo = 0;
            serie = "";
        }
    }
}
