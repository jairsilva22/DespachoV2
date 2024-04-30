using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace despacho
{
    public class cUsoCFDI
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //Propiedades

        public cUsoCFDI()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }
        public DataTable obtenerUsoCFDI()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT claveSat, CONCAT(claveSat, ' - ', descripcion) AS descripcion FROM usoCfdi";
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
    }
}