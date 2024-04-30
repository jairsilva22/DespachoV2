using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho {
    public class cLogError {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public string error { get; set; }
        public string tabla { get; set; }
        public string metodo { get; set; }
        public DateTime fecha { get; set; }

        //Variable de conexion
        private string cadena;

        //Constructor
        public cLogError() { 
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public void insertarError() {
            fecha = DateTime.Now;
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO logErrores (idUsuario, idProducto, error, tabla, metodo, fecha) " +
                        "VALUES(@idUsuario, @idProducto, @error, @tabla, @metodo, @fecha)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@error", error));
                        cmd.Parameters.Add(new SqlParameter("@tabla", tabla));
                        cmd.Parameters.Add(new SqlParameter("@metodo", metodo));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}