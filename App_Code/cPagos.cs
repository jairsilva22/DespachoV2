using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public class cPagos
    {
        //variables
        private string cadena;
        private string comando;
        //propiedades
        public int id { get; set; }
        public int idSucursal { get; set; }
        public int idSolicitud { get; set; }
        public int idUsuarioActivo { get; set; }
        public string metodoPago { get; set; }
        public string formaPago { get; set; }
        public int folio { get; set; }
        public int idBP { get; set; }
        public int idBR { get; set; }
        public float monto { get; set; }
        public float saldo { get; set; }
        public float pago { get; set; }
        public float fechaPago { get; set; }
        public string estatus { get; set; }
        public string pdf { get; set; }
        public string observaciones { get; set; }
        //Constructor
        public cPagos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }
        public void insertarPagosFinanzas(int idSol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pagosFinanzas(folio, monto, saldo, pago, fechaPago, idSolicitud, idUsuario, estatus, metodoPago, formaPago, pdfPago, observaciones, idBancoPago, idBancoRec) " +
                        "VALUES(@folio, @monto, @saldo, @pago, GETDATE(), @idSolicitud, @idUsuario, @estatus, @metodoPago, @formaPago, @pdf, @obs, @idBP, @idBR)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@saldo", saldo));
                        cmd.Parameters.Add(new SqlParameter("@pago", pago));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@formaPago", formaPago));
                        cmd.Parameters.Add(new SqlParameter("@pdf", pdf));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@idBP", idBP));
                        cmd.Parameters.Add(new SqlParameter("@idBR", idBR));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //pagos extraidos
        public int insertarPagosFinanzasAlgoritmo(int idSol, int idPagoContpaq, int idSucursal)
        {
            int idPagoFinanzas = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pagosFinanzas(folio, monto, saldo, pago, fechaPago, idSolicitud, idUsuario, estatus, metodoPago, formaPago, pdfPago, observaciones, idBancoPago, idBancoRec, idPagoContpaq, folioContpaq) " +
                        "VALUES(@folio, @monto, @saldo, @pago, GETDATE(), @idSolicitud, @idUsuario, @estatus, @metodoPago, @formaPago, @pdf, @obs, @idBP, @idBR, @idPC, @folioContpaq); SELECT SCOPE_IDENTITY()", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", obtenerFolioPagoFinanzas(idSucursal)));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@saldo", saldo));
                        cmd.Parameters.Add(new SqlParameter("@pago", pago));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@formaPago", formaPago));
                        cmd.Parameters.Add(new SqlParameter("@pdf", pdf));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@idBP", idBP));
                        cmd.Parameters.Add(new SqlParameter("@idBR", idBR));
                        cmd.Parameters.Add(new SqlParameter("@idPC", idPagoContpaq));
                        cmd.Parameters.Add(new SqlParameter("@folioContpaq", folio));

                        idPagoFinanzas = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }


            actualizarFolioPagosFinanzas(idSucursal);
            return idPagoFinanzas;
        }



        /// <summary>
        /// Función que actualiza el folio para un registro de pago finanzas de la sucursal
        /// </summary>
        /// <param name="idSucursal">Id de la sucursal para actualizar el folio</param>
        public void actualizarFolioPagosFinanzas(int idSucursal)
        {
            

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE folios SET folioActivo = folioActivo + 1 WHERE idEmpresa = @idSucursal AND pagosFinanzas = 1", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Función que obtiene el folio para un registro de pago finanzas de la sucursal
        /// </summary>
        /// <param name="idSucursal">Id de la sucursal para obtener folio</param>
        public string obtenerFolioPagoFinanzas(int idSucursal)
        {


            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioActivo FROM folios WHERE idEmpresa = @idSucursal AND pagosFinanzas = 'true'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }





        //pagos extraidos
        public void insertarPagosFinanzas(int idSol, int idPagoContpaq) {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pagosFinanzas(folio, monto, saldo, pago, fechaPago, idSolicitud, idUsuario, estatus, metodoPago, formaPago, pdfPago, observaciones, idBancoPago, idBancoRec, idPagoContpaq) " +
                        "VALUES(@folio, @monto, @saldo, @pago, GETDATE(), @idSolicitud, @idUsuario, @estatus, @metodoPago, @formaPago, @pdf, @obs, @idBP, @idBR, @idPC)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@saldo", saldo));
                        cmd.Parameters.Add(new SqlParameter("@pago", pago));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@formaPago", formaPago));
                        cmd.Parameters.Add(new SqlParameter("@pdf", pdf));
                        cmd.Parameters.Add(new SqlParameter("@obs", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@idBP", idBP));
                        cmd.Parameters.Add(new SqlParameter("@idBR", idBR));
                        cmd.Parameters.Add(new SqlParameter("@idPC", idPagoContpaq));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        public void insertarPagosVendedor(int idSol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pagosVendedor(folio, monto, saldo, pago, fechaPago, idSolicitud, idUsuario, estatus, metodoPago, formaPago) " +
                        "VALUES(@folio, @monto, @saldo, @pago, GETDATE(), @idSolicitud, @idUsuario, @estatus, @metodoPago, @formaPago)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@saldo", saldo));
                        cmd.Parameters.Add(new SqlParameter("@pago", pago));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", metodoPago));
                        cmd.Parameters.Add(new SqlParameter("@formaPago", formaPago));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool consultaSaldoFinanzas(int idSol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT saldo FROM pagosFinanzas WHERE idSolicitud = @idSol AND estatus = 'Pagado'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSol", idSol));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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
        public string obtenerSaldoFinanzas(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) ISNULL(saldo,0) AS saldo, id FROM pagosFinanzas WHERE idSolicitud = @idSolicitud AND estatus = 'Pagado' order by id desc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0) {
                                return dt.Rows[0][0].ToString();
                            }
                            else {
                                return "0";
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

        public float obtenerSaldoFinanzasByIdPago(int idPago)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(saldo,0) AS saldo, id FROM pagosFinanzas WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return float.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public float obtenerPagadoFinanzasByIdPago(int idPago)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(pago,0) AS pago, id FROM pagosFinanzas WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idPago));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return float.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public float obtenerPagoFinanzasByIdSol(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(pago) FROM pagosFinanzas WHERE idSolicitud = @idSolicitud AND estatus = 'Pagado'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return float.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerPagosSolicitud()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM pagosFinanzas WHERE idSolicitud = @idSolicitud ORDER BY id desc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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
        public DataTable obtenerPagosSolicitudVendedor()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM pagosVendedor WHERE idSolicitud = @idSolicitud ORDER BY id desc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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

        public bool consultaSaldoVendedor(int idSol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT saldo FROM pagosVendedor WHERE idSolicitud = @idSol AND estatus = 'Pagado'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSol", idSol));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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

        public string obtenerSaldoVendedor(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) saldo, id FROM pagosVendedor WHERE idSolicitud = @idSolicitud AND estatus = 'Pagado' order by id desc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool existeFechaPago(int idSol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) fechaPago FROM pagosFinanzas WHERE idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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
        public string obtenerFechaPagoBySolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) fechaPago FROM pagosFinanzas WHERE idSolicitud = @idSolicitud " +
                        " ORDER BY id DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string obtenerFechaPagoByID(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT fechaPago FROM pagosFinanzas WHERE id = @id " +
                        " ORDER BY id DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void modificarEstatusF()
        {
            try
            {
                comando = "UPDATE pagosFinanzas SET estatus = 'Cancelado', idUsuarioMod = @idU, fechaMod = GETDATE() WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@idU", idUsuarioActivo));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void modificarEstatusV()
        {
            try
            {
                comando = "UPDATE pagosVendedor SET estatus = 'Cancelado', idUsuarioMod = @idU, fechaMod = GETDATE() WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@idU", idUsuarioActivo));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void obtenerUltimoID()
        {
            try
            {
                comando = "SELECT TOP(1) id FROM pagosFinanzas WHERE idSolicitud = @idSolicitud " +
                        " ORDER BY id DESC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["id"].ToString());
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

        public string obtenerFolioByID(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folio FROM pagosFinanzas WHERE id = @id " +
                        " ORDER BY id DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string obtenerFolioByIDSolicitud(int idSolicitud) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folio FROM pagosFinanzas WHERE idSolicitud = @id AND estatus = 'Pagado'" +
                        " ORDER BY id DESC", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", idSolicitud));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            string folios = "";
                            if (dt.Rows.Count < 1) {
                                folios = "No hay folios";
                            }
                            foreach (DataRow dr in dt.Rows) {
                                folios += dr["folio"].ToString() + "\n";
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

        //Método para obtener el idSolicitud con el folio de pago
        public int obtenerIDSolicitud(int folio) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1)ISNULL(idSolicitud, 0) AS idSolicitud FROM pagosFinanzas WHERE folio = @folio" +
                        " ORDER BY id DESC", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int idS = 0;
                            if (dt.Rows.Count < 1) {
                                idS = 0;
                            }
                            foreach (DataRow dr in dt.Rows) {
                                idS = int.Parse(dr["idSolicitud"].ToString());
                            }
                            return idS;
                        }
                    }

                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //Metodo para obtener el resumen de pagos de una solicitud - Luis Moctezuma (19-11-2022)
        public DataTable obtenerResumenPagos()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "select ISNULL(sum(pago), 0) as totalPagos, ISNULL(min(saldo), 0) as totalSaldo from pagosFinanzas where (idSolicitud = @idSolicitud) and(estatus = 'Pagado')";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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
        public DataTable obtenerTotalSol()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "select total from detallesSolicitud where (idSolicitud = @idSolicitud)";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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