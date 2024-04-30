using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho {
    public class cLogCantidadEntregada {
        //Variables
        string cadena;

        //Propiedades
        public int idLog { get; set; }
        public int idDetalleSolicitud { get; set; }
        public int idUsuario { get; set; }
        public double cantidadAnterior { get; set; }
        public double cantidadActualizada { get; set; }
        public string fecha { get; set; }

        //Constructor
        public cLogCantidadEntregada() {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //Método para insertar
        public void insertar() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO logCantidadEntregada(idDetalleSolicitud, idUsuario, cantidadAnterior, cantidadActualizada, fecha) " +
                        "VALUES(@idDetSol, @idUsuario, @cantAnterior, @cantActual, GETDATE())", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idDetSol", idDetalleSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@cantAnterior", cantidadAnterior));
                        cmd.Parameters.Add(new SqlParameter("@cantActual", cantidadActualizada));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Método para obtener el idDetalleSolicitud con el idOrden
        public void obtenerIdDetalleSolicitud(int idOrden) {
            try {
                string comando = "SELECT TOP(1)idDetalleSolicitud FROM ordenDosificacion where idOrden = @id";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", idOrden));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    idDetalleSolicitud = int.Parse(reader["idDetalleSolicitud"].ToString());
                                }
                            }
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