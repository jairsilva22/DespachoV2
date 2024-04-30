using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho {
    public class cContpaq {
        //variables
        //
        public string cadena;
        private string cadena2;
        //propiedades
        public int ultimoId { get; set; }
        public int IdInicial { get; set; }
        public string ultimaActualizacion { get; set; }
        public int idSolicitud { get; set; }
        public int idContpaq { get; set; }
        public string folio { get; set; }
        public string pago { get; set; }
        public DateTime fecha { get; set; }
        public string observaciones { get; set; }
        public int folioPago { get; set; }
        public string folioAux { get; set; }
        public string nombreUsr { get; set; }
        public decimal Saldo { get; set; }
        public string Monto { get; set; }
        public string Status { get; set; }
        public cContpaq() {
            cadena = ConfigurationManager.ConnectionStrings["contpaqcnx"].ConnectionString;
            cadena2 = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para buscar configuracion a Base de datos a CONTPAQ 
        public string BDContpaq() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 1", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Concretos Saltillo Ventas General
        public string BDConcrSalVG() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 2", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Block Saltillo Facturable
        public string BDBlockSalFac() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 3", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Block Saltillo Ventas General
        public string BDBlockSalVG() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 4", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Block Irapuato Facturable
        public string BDBlockIra() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 7", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Block Irapuato Ventas General
        public string BDBlockIraVG() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 8", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Concretos Irapuato Facturable
        public string BDIraConcretos() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 9", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        //Consulta a Concretos Irapuato Ventas General
        public string BDIraConcretosVG() {
            string conexion = "";
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 10", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

        }

        public DataTable obtenerConexiones() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.configContpaq WHERE (activo = 1)", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Obtener conexiones a Contabilidad
        public DataTable obtenerConexionesContabilidad() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.configContpaq WHERE (contabilidad = 1)", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para consultar los datos de CONTPAQ
        public DataTable obtenerDatos() {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    //using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO FROM dbo.admDocumentos WHERE(CIDDOCUMENTO > @idInicial AND CIDDOCUMENTODE = 3 ORDER BY CIDDOCUMENTO ASC )", conn))
                    using (SqlCommand cmd = new SqlCommand("SELECT top (25) CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO FROM dbo.admDocumentos WHERE(CIDDOCUMENTO > @idInicial AND CIDDOCUMENTODE = 3 ) ORDER BY CIDDOCUMENTO ASC", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idInicial", IdInicial));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener la solicitud a la que pertenecen las remisiones traidas de contpaq
        public string obtenerSolicitudPorRemision() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioR, OD.id as idOrdenDosificacion, O.id as idOrden, o.idSolicitud FROM ordenDosificacion AS OD JOIN ordenes AS O ON O.id = Od.idOrden WHERE (folioR = @folioAux)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@folioAux", folioAux));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            // return dt.Rows[0]["idSolicitud"].ToString();


                            if (dt.Rows.Count > 0) {

                                return dt.Rows[0]["idSolicitud"].ToString();

                            }
                            else {
                                return "0";
                            }



                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para insertar ultima actualizacion
        public void insertarActualizacion() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.configContpaq SET(ultimo ID = @ultimoId, ultimaConsulta = GETDATE()) where idConfigContpaq = 1)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ultimoId", ultimoId));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener ultima actualizacion
        public DataTable obtenerActualizacion() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ultimaConsulta FROM configContpaq ORDER BY idConfigContpaq DESC", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para insertar los pagos en la tabla de pagosFinanzas
        public void insertarPagos() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.pagosFinanzas(idSolicitud, folioRemision, pago, fechaPago, observaciones, idPagoContpaq, folio, monto, saldo,estatus) VALUES (@idSolicitud, @folio, @pago, @fecha, @observaciones, @idContpaq, @folioPago, @Monto, @Saldo,@statu)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@pago", pago));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@observaciones", observaciones));
                        cmd.Parameters.Add(new SqlParameter("@idContpaq", idContpaq));
                        cmd.Parameters.Add(new SqlParameter("@folioPago", folioPago));
                        cmd.Parameters.Add(new SqlParameter("@monto", Monto));
                        cmd.Parameters.Add(new SqlParameter("@saldo", Saldo));
                        cmd.Parameters.Add(new SqlParameter("@statu", Status));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //metodo para insertar el ultimo Id consultado
        public void insertarUltimoId() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.configContpaq SET ultimoID = @ultimoId, ultimaConsulta = GETDATE() where idConfigContpaq = 1", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ultimoid", ultimoId));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //Obtener Los Movimientos de la Remsion
        public DataTable ObtenerMovimientos(string IDDocumento) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTO, CIDPRODUCTO, CUNIDADESCAPTURADAS, CPRECIOCAPTURADO, CTOTAL FROM admMovimientos WHERE CIDDOCUMENTO = '" + IDDocumento + "'", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para consultar los datos de CONTPAQ por folio
        public DataTable obtenerDatosPorFolioSaltilloConcretos() {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO, CPENDIENTE FROM dbo.admDocumentos WHERE (CFOLIO = @folioAux AND CIDDOCUMENTODE = 3)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@folioAux", folioAux));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        public DataTable ObtenerDatosCompaqi(string comando) {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["contpaqcnx"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosPorFolioSaltilloBlock(string comando) {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockSaltilloRemisiones"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosPorFolioIrapuatoConcreto(string comando) {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BancoConcretosIrapuato"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosPorFolioIrapuatoBlock(string comando) {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockIrapuatoRemisiones"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        //metodo para saber si el pago que estamos trayendo de contpaq ya existe en nuestra BD
        public bool existePago() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM pagosFinanzas WHERE idPagoContpaq = @idContpaq", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idContpaq", idContpaq));
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
                throw (ex);
            }
        }
        //metodo para generar folio
        public string obtenerUltimoFolio() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 folio FROM pagosFinanzas ORDER BY folio DESC", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0]["folio"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //metodo para obtener el ultimo id guardado
        public string obtenerUltimoId() {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ultimoID FROM configContpaq ORDER BY idConfigContpaq DESC", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0]["ultimoID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para insertar Log de extracciones contpaq
        public void insertarLogContpaq() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.logContpaq(idInicial, idFinal, FechaConsulta, usuario ) VALUES (@idInicial, @ultimoId, GETDATE(), @usr)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idInicial", IdInicial));
                        cmd.Parameters.Add(new SqlParameter("@ultimoId", ultimoId));
                        cmd.Parameters.Add(new SqlParameter("@usr", nombreUsr));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //metodo para obtener las razones sociales del contpaq
        public DataTable obtenerClientesContpaq() {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDCLIENTEPROVEEDOR, CCODIGOCLIENTE, CRAZONSOCIAL, CIDAGENTEVENTA FROM dbo.admClientes ORDER BY CRAZONSOCIAL ASC", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idInicial", IdInicial));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para buscar por razón social
        public DataTable obtenerClientesContpaqRazonSocial(string razon) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDCLIENTEPROVEEDOR, CRAZONSOCIAL FROM dbo.admClientes WHERE CRAZONSOCIAL LIKE '%" + razon + "%' ORDER BY CRAZONSOCIAL ASC", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idInicial", IdInicial));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los datos del cliente
        public DataTable obtenerDatosCliente(int CIDCLIENTEPROVEEDOR) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admClientes WHERE cidclienteproveedor = @idCliente", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los datos del cliente
        public DataTable obtenerDatosCliente(string cadena, int CIDCLIENTEPROVEEDOR) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admClientes WHERE cidclienteproveedor = @idCliente", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los datos del cliente
        public DataTable obtenerDatosCliente(string cadena) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admClientes", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        public DataTable obtenerClienteVtaGrl() {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admClientes WHERE CIDCLIENTEPROVEEDOR > 0", conn)) {
                        // cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas por fecha
        public DataTable obtenerVentasPorCliente(string fechaInicio, string fechaFin) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select admDocumentos.CIDDOCUMENTO, admClientes.CCODIGOCLIENTE,admClientes.CRAZONSOCIAL,admMovimientos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admMovimientos.CUnidades,admMovimientos.CNETO,admMovimientos.CDESCUENTO1,admMovimientos.CNETO-admMovimientos.CDESCUENTO1 as 'Neto-Desc',admMovimientos.CIMPUESTO1,admMovimientos.CTOTAL " +
                        "from admDocumentos inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR=admClientes.CIDCLIENTEPROVEEDOR inner Join admMovimientos on admDocumentos.CIDDOCUMENTO= admMovimientos.CIDDOCUMENTO inner join admProductos on admProductos.CIDPRODUCTO= admMovimientos.CIDPRODUCTO " +
                        "where (admDocumentos.cfecha >= @fechaInicio AND admDocumentos.cfecha <= @fechaFin) AND (admDocumentos.CIDCONCEPTODOCUMENTO = 3) " +
                        "order by admClientes.CCODIGOCLIENTE", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }



        //metodo para obtener los cargos del cliente
        public DataTable obtenerCargosCliente(int CIDCLIENTEPROVEEDOR, string fechaInicio, string fechaFin) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE cidclienteproveedor = @idCliente AND (cfecha >= @fechaInicio AND cfecha <= @fechaFin) AND (CIDDOCUMENTODE = 4) AND CCANCELADO = 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }


        //metodo para obtener los cargos del cliente según la sucursal
        public DataTable obtenerCargosCliente(string cadena, int CIDCLIENTEPROVEEDOR, string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE cidclienteproveedor = @idCliente AND (cfecha >= @fechaInicio AND cfecha <= @fechaFin) AND (CIDDOCUMENTODE = 4) AND CCANCELADO = 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }


        //metodo para obtener los cargos del cliente según la sucursal
        public DataTable obtenerCargosClienteTodos(string cadena, string codigoCliente, string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos As A JOIN dbo.admClientes As C ON A.CIDCLIENTEPROVEEDOR = C.CIDCLIENTEPROVEEDOR WHERE (C.CCODIGOCLIENTE = @ccodigocliente) AND (cfecha >= @fechaInicio AND cfecha <= @fechaFin) AND (CIDDOCUMENTODE = 4) AND CCANCELADO = 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ccodigocliente", codigoCliente));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        public string obtenerCCodigoCliente(string cadena, int idCliente) {
            //string cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            //Buscar en otras sucursales
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * from dbo.admClientes WHERE CIDCLIENTEPROVEEDOR = @idCliente", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0]["CCODIGOCLIENTE"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        public DataTable obtenerCargosClienteVtsGrl(int CIDCLIENTEPROVEEDOR, string fechaInicio) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE cidclienteproveedor = @idCliente AND (CFECHAVENCIMIENTO >= '2010/01/01' AND CFECHAVENCIMIENTO <= @fechaInicio) AND (CIDCONCEPTODOCUMENTO = 3014 OR CIDCONCEPTODOCUMENTO = 3008 OR CIDCONCEPTODOCUMENTO = 14) AND CCANCELADO = 0 AND CPENDIENTE > 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        //metodo para obtener abonos de un cargo en especifico
        public DataTable obtenerAbonoPorIdCargo(int ciddocumento) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admAsocCargosAbonos WHERE ciddocumentocargo = @ciddocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ciddocumento", ciddocumento));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener abonos de un cargo en especifico por sucursal
        public DataTable obtenerAbonoPorIdCargo(string cadena, int ciddocumento) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admAsocCargosAbonos WHERE ciddocumentocargo = @ciddocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ciddocumento", ciddocumento));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        //metodo para obtener detalles de Abono por el id
        public DataTable obtenerAbonoPorId(int idAbono) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE ciddocumento = @ciddocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ciddocumento", idAbono));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener detalles de Abono por el id por sucursal
        public DataTable obtenerAbonoPorId(string cadena, int idAbono) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE ciddocumento = @ciddocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@ciddocumento", idAbono));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {return null;
                //throw (ex);
            }
        }

        //Metodo para obtener el concepto de un documento de contpaq
        public String obtenerConceptoPorId(int idDocumento) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CNOMBRECONCEPTO FROM dbo.admConceptos WHERE cidconceptodocumento = @cidconceptodocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidconceptodocumento", idDocumento));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0]["CNOMBRECONCEPTO"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Metodo para obtener el concepto de un documento de contpaq por sucursal
        public String obtenerConceptoPorId(string cadena, int idDocumento) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CNOMBRECONCEPTO FROM dbo.admConceptos WHERE cidconceptodocumento = @cidconceptodocumento", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidconceptodocumento", idDocumento));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0]["CNOMBRECONCEPTO"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        public string obtenerVendedor(int idVendedor) {
            try {
                string cadena = BDContpaq();
                string nombre = "";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CNOMBREAGENTE FROM admAgentes WHERE CIDAGENTE = @id", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", idVendedor));
                        using (SqlDataReader sda = cmd.ExecuteReader()) {
                            if (sda.HasRows) {
                                while (sda.Read()) {
                                    nombre = sda["CNOMBREAGENTE"].ToString();
                                }
                            }

                        }
                        return nombre;
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        public DataTable obtenerCargosVencidosCliente(int CIDCLIENTEPROVEEDOR, string fechaInicio, string fechaFin) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.admDocumentos WHERE cidclienteproveedor = @idCliente AND (cfecha >= @fechaInicio AND cfecha <= @fechaFin) AND (CIDCONCEPTODOCUMENTO = 3014 OR CIDCONCEPTODOCUMENTO = 3008) AND (CPENDIENTE > 0) AND (CCANCELADO = 0) AND (CFECHAVENCIMIENTO < @fechaFin) order by CFECHAVENCIMIENTO asc", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas Generales Cobranza
        // vencido
        public DataTable obtenerVentasGenerales(string fechaInicio) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select admDocumentos.CIDCLIENTEPROVEEDOR, admDocumentos.CRAZONSOCIAL, admAgentes.CIDAGENTE, admAgentes.CNOMBREAGENTE, admDocumentos.CPENDIENTE, admDocumentos.CFECHAVENCIMIENTO " +
                        "From admDocumentos inner join admAgentes on admDocumentos.CIDAGENTE=admAgentes.CIDAGENTE " +
                        "Where (CIDDOCUMENTODE = 4) and ccancelado = 0 and CPENDIENTE >= 1 and cfecha BETWEEN '2010-03-19' AND @fechaInicio " +
                        "Order by admAgentes.CIDAGENTE, admDocumentos.CIDCLIENTEPROVEEDOR", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                            
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas Generales Cobranza por sucursal
        public DataTable obtenerVentasGenerales(string cadena, string fechaInicio) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select admDocumentos.CIDCLIENTEPROVEEDOR, admDocumentos.CRAZONSOCIAL, admAgentes.CIDAGENTE, admAgentes.CNOMBREAGENTE, admDocumentos.CPENDIENTE, admDocumentos.CFECHAVENCIMIENTO " +
                        "From admDocumentos inner join admAgentes on admDocumentos.CIDAGENTE=admAgentes.CIDAGENTE " +
                        "Where (CIDDOCUMENTODE = 4) and ccancelado = 0 and CPENDIENTE >= 1 and cfecha BETWEEN '2010-03-19' AND @fechaInicio " +
                        "Order by admAgentes.CIDAGENTE, admDocumentos.CIDCLIENTEPROVEEDOR", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas por vendedor (25/08/2022)
        public DataTable obtenerVentasPorVendedor(string fechaInicio, string fechaFin) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select admDocumentos.CIDDOCUMENTO, admDocumentos.CIDAGENTE, admAgentes.CIDAGENTE, admAgentes.CNOMBREAGENTE,admAgentes.CCODIGOAGENTE,admClientes.CRAZONSOCIAL,admMovimientos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admMovimientos.CUnidades,admMovimientos.CNETO,admMovimientos.CDESCUENTO1,admMovimientos.CNETO-admMovimientos.CDESCUENTO1 as 'Neto-Desc',admMovimientos.CIMPUESTO1,admMovimientos.CTOTAL " +
                        "from admDocumentos inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR=admClientes.CIDCLIENTEPROVEEDOR inner Join admMovimientos on admDocumentos.CIDDOCUMENTO= admMovimientos.CIDDOCUMENTO inner join admProductos on admProductos.CIDPRODUCTO= admMovimientos.CIDPRODUCTO inner join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE " +
                        "where (admDocumentos.cfecha >= @fechaInicio AND admDocumentos.cfecha <= @fechaFin) AND (admDocumentos.CIDCONCEPTODOCUMENTO = 3) " +
                        "order by admAgentes.CCODIGOAGENTE", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {

                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {

                                return null;
                            }
                            else {
                                return dt;

                            }


                        }

                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        public DataTable obtenerFlujoBancos(string cadena, string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using(SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand("SELECT ISNULL(sum(I.Total), 0) as Ingresos, ISNULL((SELECT sum(Total) as Egresos FROM dbo.Egresos WHERE Fecha BETWEEN @fechaInicio AND @fechaFin), 0) as Egresos," +
                        "ISNULL((SELECT sum(Total) FROM dbo.Ingresos WHERE Fecha < @fechaInicio), 0) As 'IngresosInicial'," +
                        "ISNULL((SELECT sum(Total) FROM dbo.Egresos WHERE Fecha < @fechaInicio), 0) As 'EgresosInicial'" +
                        "FROM dbo.Ingresos as I WHERE (I.Fecha BETWEEN @fechaInicio AND @fechaFin)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                dt.Columns.Add("Ingresos");
                                dt.Columns.Add("Egresos");
                                dt.Columns.Add("IngresosInicial");
                                dt.Columns.Add("EgresosInicial");

                                dt.Rows.Add(new Object[] { "0", "0", "0", "0"});
                                return dt;
                            }
                            else {
                                return dt;

                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }

        }

        //metodo para obtener los Reporte de Ventas por vendedor (25/08/2022)
        public DataTable obtenerVentasPorVendedor(string cadena, string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select admDocumentos.CIDDOCUMENTO, admDocumentos.CIDAGENTE, admAgentes.CIDAGENTE, admAgentes.CNOMBREAGENTE,admAgentes.CCODIGOAGENTE,admClientes.CRAZONSOCIAL,admMovimientos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admMovimientos.CUnidades,admMovimientos.CNETO,admMovimientos.CDESCUENTO1,admMovimientos.CNETO-admMovimientos.CDESCUENTO1 as 'Neto-Desc',admMovimientos.CIMPUESTO1,admMovimientos.CTOTAL " +
                        "from admDocumentos inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR=admClientes.CIDCLIENTEPROVEEDOR inner Join admMovimientos on admDocumentos.CIDDOCUMENTO= admMovimientos.CIDDOCUMENTO inner join admProductos on admProductos.CIDPRODUCTO= admMovimientos.CIDPRODUCTO inner join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE " +
                        "where (admDocumentos.cfecha >= @fechaInicio AND admDocumentos.cfecha <= @fechaFin) AND (admDocumentos.CIDDOCUMENTODE = 4) AND admDocumentos.CCANCELADO = 0 " +
                        "order by admAgentes.CCODIGOAGENTE", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {

                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {

                                return null;
                            }
                            else {
                                return dt;

                            }


                        }

                    }
                }

            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        //metodo para obtener los Reporte de Cuentas por pagar a proveedores (26/08/2022)
        public DataTable obtenerCuentasPorPagarAProveedores(string fechaInicio, string fechaFin) {
            try {
                string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT AC.CIDCLIENTEPROVEEDOR, AC.CDENCOMERCIAL, AC.CRAZONSOCIAL, AD.CFECHA,AD.CNETO,AD.CFOLIO, AD.CIMPUESTO1,AD.CTOTAL,AD.CIDMONEDA, AD.CFECHAULTIMOINTERES, AM.CCLAVESAT, AC.CIDCUENTA,CB.CNOMBRECUENTA " +
                        "FROM admClientes AS AC INNER " +
                        "JOIN admDocumentos AS AD ON AC.CIDCLIENTEPROVEEDOR = AD.CIDCLIENTEPROVEEDOR " +
                        "JOIN admMonedas AS AM ON AM.CIDMONEDA = AD.CIDMONEDA " +
                        "JOIN admCuentasBancarias AS CB ON CB.CIDCUENTA=AC.CIDCUENTA " +
                        "WHERE (AD.CFECHA >= @fechaInicio AND AD.CFECHA <= @fechaFin) AND  CTIPOCLIENTE='3'", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", CIDCLIENTEPROVEEDOR));
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {

                                return null;
                            }
                            else {
                                return dt;

                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        public DataTable obtenerVentasPorProductoDespacho(string fechaInicio, string fechaFin) {
            try {

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    //using (SqlCommand cmd = new SqlCommand("SELECT p.codigo,p.descripcion, o.fecha, s.idSucursal AS planta, tp.tipo, ds.cantidadEntregada, " +
                    //    "ds.precioU, ds.subtotal, ds.iva, ds.total, p.idUDM, od.cantidad " +
                    //    "FROM detallesSolicitud AS ds INNER JOIN ordenes AS o ON ds.idSolicitud = o.idSolicitud INNER JOIN productos AS p INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ON ds.idProducto = p.id INNER JOIN " +
                    //    "solicitudes AS s ON o.idSolicitud = s.id  INNER JOIN ordenDosificacion AS od ON od.idOrden = od.id WHERE(ds.cantidadEntregada > 0)AND(od.fecha >= @fechaInicio AND o.fecha <= @fechaFin) AND(s.eliminada IS NULL) AND(o.eliminado IS NULL) AND(ds.eliminado IS NULL) AND od.cantidad > 0 " +
                    //    "AND od.idEstadoDosificacion > 6 AND od.idEstadoDosificacion < 12 AND od.eliminado IS NULL AND s.idSucursal in (1,2,3,4) order by codigo desc ", conn)) {
                    using (SqlCommand cmd = new SqlCommand("" +
                        "SELECT p.codigo, p.descripcion, o.fecha, s.idSucursal AS planta, tp.tipo, ds.cantidadEntregada, ds.precioU, ds.subtotal, ds.iva, ds.total, p.idUDM, od.cantidad, o.reqFac " +
                        "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id	 INNER JOIN detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id " +
                        "WHERE (ds.cantidadEntregada > 0) AND (od.fecha >= @fechaInicio AND od.fecha <= @fechaFin) AND (s.eliminada IS NULL) AND (o.eliminado IS NULL) AND (ds.eliminado IS NULL) AND (od.eliminado IS NULL) AND (od.cantidad) > 0 AND (od.idEstadoDosificacion BETWEEN 7 AND 11) AND (s.idSucursal IN (1, 2, 3, 1006)) " +
                        "ORDER BY p.codigo DESC", conn)) { 
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Método para obtener las ventas por vendedor del despacho. Agregado por Luis Sandoval el 02/01/2022
        public DataTable obtenerVentasPorVendedorDespacho(string fechaInicio, string fechaFin) {
            try {

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("" +
                        "SELECT s.idSucursal AS planta, tp.tipo, ds.precioU, p.idUDM, od.cantidad, o.reqFac, u.nombre AS vendedor, c.nombre AS cliente " +
                        "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id	 INNER JOIN detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN usuarios AS u ON o.idVendedor = u.id INNER JOIN clientes AS c ON c.id = s.idCliente " +
                        "WHERE (ds.cantidadEntregada > 0) AND (od.fecha >= @fechaInicio AND od.fecha <= @fechaFin) AND (s.eliminada IS NULL) AND (o.eliminado IS NULL) AND (ds.eliminado IS NULL) AND (od.eliminado IS NULL) AND (od.cantidad) > 0 AND (od.idEstadoDosificacion BETWEEN 7 AND 11) AND (s.idSucursal IN (1, 2, 3, 1006)) " +
                        "ORDER BY u.id DESC", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas por Producto
        public DataTable obtenerVentasPorProducto(string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admProductos.CTIPOPRODUCTO," +
                        "admMovimientos.CUnidades,admMovimientos.CNETO,admMovimientos.CDESCUENTO1, admMovimientos.CNETO - admMovimientos.CDESCUENTO1 as 'Neto-Desc'," +
                        "admMovimientos.CIMPUESTO1, admMovimientos.CTOTAL FROM admDocumentos inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR = admClientes.CIDCLIENTEPROVEEDOR " +
                        "inner Join admMovimientos on admDocumentos.CIDDOCUMENTO = admMovimientos.CIDDOCUMENTO inner join " +
                        "admProductos on admProductos.CIDPRODUCTO = admMovimientos.CIDPRODUCTO" +
                        " WHERE (admDocumentos.cfecha >= @fechaInicio AND admDocumentos.cfecha <= @fechaFin) AND (admDocumentos.CIDCONCEPTODOCUMENTO = 3)" +
                        " AND (admProductos.CCODIGOPRODUCTO != '(Ninguno)') ORDER BY admProductos.CCODIGOPRODUCTO", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //metodo para obtener los Reporte de Ventas por Producto
        public DataTable obtenerVentasPorProducto(string cadena, string fechaInicio, string fechaFin) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT admProductos.CCODIGOPRODUCTO, admProductos.CNOMBREPRODUCTO, admProductos.CTIPOPRODUCTO," +
                        "sum(admMovimientos.CUnidades) as CUnidades, sum(admMovimientos.CNETO) as CNETO, SUM(admMovimientos.CDESCUENTO1) as CDESCUENTO1, SUM(admMovimientos.CNETO - admMovimientos.CDESCUENTO1) as 'Neto-Desc'," +
                        "SUM(admMovimientos.CIMPUESTO1) AS CIMPUESTO1, SUM(admMovimientos.CTOTAL) AS CTOTAL FROM admDocumentos inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR = admClientes.CIDCLIENTEPROVEEDOR " +
                        "inner Join admMovimientos on admDocumentos.CIDDOCUMENTO = admMovimientos.CIDDOCUMENTO inner join " +
                        "admProductos on admProductos.CIDPRODUCTO = admMovimientos.CIDPRODUCTO" +
                        " WHERE (admDocumentos.cfecha >= @fechaInicio AND admDocumentos.cfecha <= @fechaFin) AND (admDocumentos.CIDDOCUMENTODE = 4) AND (admDocumentos.CCANCELADO = 0)" +
                        " AND (admProductos.CCODIGOPRODUCTO != '(Ninguno)') GROUP BY CCODIGOPRODUCTO, CNOMBREPRODUCTO, CTIPOPRODUCTO ORDER BY admProductos.CCODIGOPRODUCTO", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        //metodo para obtener los Reporte de Cuentas Por pagar a Proveedores
        public DataTable obtenerCuentasProveedores(string cadena, string fechaInicio, string fechaFin, string validacion) {
            try {
                //string cadena = BDContpaq();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT D.CFECHA, D.CFOLIO, C.CRAZONSOCIAL AS PROVEEDOR, P.CNOMBREPRODUCTO, D.CNETO AS SUBTOTAL, D.CIMPUESTO1 AS IVA, D.CIMPUESTO2 AS ISR, D.CTOTAL, MO.CCLAVESAT, CTIPOCLIENTE, D.CIDCLIENTEPROVEEDOR, D.CFECHAVENCIMIENTO, M.CUNIDADESCAPTURADAS, D.CPENDIENTE " +
                        "FROM admDocumentos AS D INNER JOIN admMovimientos AS M ON D.CIDDOCUMENTO = M.CIDDOCUMENTO INNER JOIN admProductos AS P ON M.CIDPRODUCTO = P.CIDPRODUCTO " +
                        "INNER JOIN admMonedas AS MO ON D.CIDMONEDA = MO.CIDMONEDA INNER JOIN admclientes AS C ON D.CIDCLIENTEPROVEEDOR = C.CIDCLIENTEPROVEEDOR " +
                        "WHERE D.CIDDOCUMENTODE = 19 AND C.CTIPOCLIENTE = 3 AND(D.CFECHA >= @fechaInicio AND D.CFECHA <= @fechaFin) " + validacion + " order by D.CIDCLIENTEPROVEEDOR DESC ", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            int empty = 0;

                            if (dt.Rows.Count == empty) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
                //throw (ex);
            }
        }

        //Método para encontrar la factura que pertenece a la remisión en admDocumentos
        public string idRemision(string folio) {
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTO " +
                        "FROM admDocumentos " +
                        "WHERE CFOLIO = @cfolio AND CIDDOCUMENTODE = 3 AND CCANCELADO = 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cfolio", folio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt.Rows[0]["CIDDOCUMENTO"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        //Método para encontrar el id de la factura de cargo
        public string idCargo(string idremision) {
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTO " +
                        "FROM admDocumentos " +
                        "WHERE CIDDOCUMENTOORIGEN = @cOrigen AND CIDDOCUMENTODE = 4 and CCANCELADO = 0", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cOrigen", idremision));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt.Rows[0]["CIDDOCUMENTO"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        //Método para encontrar el documento de abono en la tabla AsocCargosAbonos
        public string[] idAbono(string idcCargo) {
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDDOCUMENTOABONO " +
                        "FROM admAsocCargosAbonos " +
                        "WHERE CIDDOCUMENTOCARGO = @cCargo", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cCargo", idcCargo));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                string[] folios = new string[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++) {
                                    folios[i] = dt.Rows[i]["CIDDOCUMENTOABONO"].ToString();
                                }
                                return folios;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        //Método para encontrar los abonos en la tabla Documentos
        public DataTable pagoContpaq(string idcCargo) {
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CFOLIO, ASOC.CIMPORTEABONO, CFECHA, CMETODOPAG, CIDDOCUMENTO " +
                        "FROM admDocumentos AS DOC LEFT JOIN admAsocCargosAbonos AS ASOC ON DOC.CIDDOCUMENTO = ASOC.CIDDOCUMENTOABONO " +
                        "WHERE ASOC.CIDDOCUMENTOCARGO = @cCargo OR DOC.CIDDOCUMENTO = @cCargo AND DOC.CIDDOCUMENTODE = 9 " +
                        "ORDER BY CIDDOCUMENTO ASC ", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cCargo", idcCargo));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0) {
                                return null;
                            }
                            else {
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        //Obtener cadena según id de sucursal
        public string cadenaContpaq(int idSucursal) {
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT conexionBD FROM configContpaq WHERE idSucursalDespacho = @idSucursal AND activo = 1 AND isFacturable = 1", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            string conexion = dt.Rows[0]["conexionBD"].ToString();
                            return conexion;
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