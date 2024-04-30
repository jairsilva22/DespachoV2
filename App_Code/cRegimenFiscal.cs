using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cRegimenFiscal
    {
        //variables
        private string cadena;

        //propiedades
        
        //Constructor
        public cRegimenFiscal()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public DataTable llenarRegimen()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM regimenFiscal", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
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