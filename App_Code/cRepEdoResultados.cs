using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace despacho
{
    public class cRepEdoResultados
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idSucursal { get; set; }

        //Constructor
        public cRepEdoResultados()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnxCompras"].ConnectionString;
        }

        public DataTable obtenerProductosBySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, codigo FROM productos WHERE idSucursal = @idSuc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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