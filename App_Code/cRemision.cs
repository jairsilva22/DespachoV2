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
    public class cRemision
    {
        //variables
        private string cadena;

        public cRemision()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public DataTable obtenerRemisiones(int idSucursal)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * from ordenDosificacion where folio is not null and idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                       // cmd.Parameters.Add(new SqlParameter("@programada", programada));
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

        public string obtenerFolioByIDOrden(int idOrden) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioR FROM ordenDosificacion WHERE idOrden = @idOrden AND (cantidad > 0) AND (folioR IS NOT NULL) AND (eliminado IS NULL) " +
                        " ORDER BY id DESC", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            string folios = "";
                            if (dt.Rows.Count < 1) {
                                folios = "No hay folios";
                            }
                            foreach (DataRow dr in dt.Rows) {
                                folios += dr["folioR"].ToString() + "\n";
                            }
                            return folios;
                        }
                    }

                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

    }
}