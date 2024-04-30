using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho.App_Code {
    public class LogPagosContpaq {
        //Variables
        string cadena = string.Empty;
        string comando = string.Empty;
        //Propiedades
        public int id { get; set; }
        public int idPagoContpaq { get; set; }
        public int idCargoContpaq { get; set; }
        public DateTime fechaAlta { get; set; }
        public int idUsuario { get; set; }
        public double monto { get; set; }
        public string metodoPago { get; set; }
        public int idPagoFinanzas { get; set; }

        public LogPagosContpaq() {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //Método para saber si existe un pago ya aplicado
        public bool exists() {
            try {
                comando = "SELECT l.id, p.estatus FROM logPagosContpaq AS L LEFT JOIN pagosFinanzas AS P ON l.idPagosFinanzas = p.id  WHERE (l.idPagoContpaq = @id AND l.idCargoContpaq = @idCargo) AND estatus = 'Pagado'";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", idPagoContpaq));
                        cmd.Parameters.Add(new SqlParameter("@idCargo", idCargoContpaq));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return true;
            }
        }

        //Método para insertar pago en el log
        public void insertar() {
            try {
                comando = "INSERT INTO dbo.logPagosContpaq " +
                    "VALUES (@idPago, @idCargo, GETDATE(), @idUsuario, @monto, @metodoPago, @idPagoFinanzas)";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idPago", idPagoContpaq));
                        cmd.Parameters.Add(new SqlParameter("@idCargo", idCargoContpaq));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@idPagoFinanzas", idPagoFinanzas));
                        
                        int fila = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

    }
}