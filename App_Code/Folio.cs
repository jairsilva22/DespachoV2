using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Folio
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        public string serie { get; set; }
        public string naprobacion { get; set; }
        public string anoaprobacion { get; set; }
        public int idLog { get; set; }
        public int idUsuario { get; set; }
        public int folioInicio { get; set; }
        public int folioFinal { get; set; }
        public int folioActivo { get; set; }
        public int idEmpresa { get; set; }
        public bool factura { get; set; }
        public bool notaCargo { get; set; }
        public bool notaCredito { get; set; }
        public bool cPago { get; set; }
        public bool activo { get; set; }
        public bool pagosF { get; set; }
        public bool pagosV { get; set; }
        public bool solicitudes { get; set; }
        public bool ordenes { get; set; }
        public bool remisiones { get; set; }

        public Folio()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para obtener los datos de los documentos
        public DataTable mostrarFolios()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM folios WHERE idEmpresa = @id ORDER BY idLogs DESC";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idEmpresa));
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

        //metodo para insertar
        public void insertar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO folios(folioInicio, folioFinal, folioActivo, serie, naprobacion, anoaprobacion, factura, nCredito, nCargo, cPago, idEmpresa, " +
                        "activo, pagosFinanzas, pagosVendedor, solicitudes, remisiones, ordenes, idUsuario, fechaAlta) " +
                        "VALUES(@fi, @ff, @fa, @serie, @napro, @anoapro, @factura, @nCredito, @nCargo, @cPago, @idEmpresa, 1, @pf, @pv, @solicitudes, @remisiones, @ordenes, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fi", folioInicio));
                        cmd.Parameters.Add(new SqlParameter("@ff", folioFinal));
                        cmd.Parameters.Add(new SqlParameter("@fa", folioActivo));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@napro", naprobacion));
                        cmd.Parameters.Add(new SqlParameter("@anoapro", anoaprobacion));
                        cmd.Parameters.Add(new SqlParameter("@factura", factura));
                        cmd.Parameters.Add(new SqlParameter("@nCargo", notaCargo));
                        cmd.Parameters.Add(new SqlParameter("@nCredito", notaCredito));
                        cmd.Parameters.Add(new SqlParameter("@cPago", cPago));
                        cmd.Parameters.Add(new SqlParameter("@idEmpresa", idEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@pf", pagosF));
                        cmd.Parameters.Add(new SqlParameter("@pv", pagosV));
                        cmd.Parameters.Add(new SqlParameter("@solicitudes", solicitudes));
                        cmd.Parameters.Add(new SqlParameter("@remisiones", remisiones));
                        cmd.Parameters.Add(new SqlParameter("@ordenes", ordenes));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        
        //metodo para obtener id del cliente por su clave
        public void obtenerFolio()
        {
            try
            {
                string comando = "SELECT * FROM folios WHERE idLogs = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idLog));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folioInicio = int.Parse(reader["folioInicio"].ToString());
                                    folioFinal = int.Parse(reader["folioFinal"].ToString());
                                    folioActivo = int.Parse(reader["folioActivo"].ToString());
                                    factura = bool.Parse(reader["factura"].ToString());
                                    notaCargo = bool.Parse(reader["nCargo"].ToString());
                                    notaCredito = bool.Parse(reader["nCredito"].ToString());
                                    cPago = bool.Parse(reader["cPago"].ToString());
                                    pagosF = bool.Parse(reader["pagosFinanzas"].ToString());
                                    pagosV = bool.Parse(reader["pagosVendedor"].ToString());
                                    solicitudes = bool.Parse(reader["solicitudes"].ToString());
                                    remisiones = bool.Parse(reader["remisiones"].ToString());
                                    ordenes = bool.Parse(reader["ordenes"].ToString());
                                    serie = (reader["serie"].ToString());
                                    anoaprobacion = (reader["anoaprobacion"].ToString());
                                    naprobacion = (reader["naprobacion"].ToString());
                                    idEmpresa = int.Parse(reader["idEmpresa"].ToString());
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

        public string obtenerSerie(int idS)
        {
            try
            {
                comando = "SELECT serie FROM folios WHERE idEmpresa = @idS AND factura = 'True'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idS", idS));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    serie = reader["serie"].ToString();
                                }
                            }
                            return serie;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void modificar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE folios SET folioInicio = @fi, folioFinal = @ff,  folioActivo = @fa, serie = @serie, naprobacion = @nap, anoaprobacion = @anoap, " +
                        "factura = @fact, nCredito = @ncred, nCargo = @ncargo, cPago = @cpago, pagosFinanzas = @pF, pagosVendedor = @pv, solicitudes = @sol, remisiones = @rem, ordenes = @ord " +
                        "WHERE idLogs = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fi", folioInicio));
                        cmd.Parameters.Add(new SqlParameter("@ff", folioFinal));
                        cmd.Parameters.Add(new SqlParameter("@fa", folioActivo));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@nap", naprobacion));
                        cmd.Parameters.Add(new SqlParameter("@anoap", anoaprobacion));
                        cmd.Parameters.Add(new SqlParameter("@fact", factura));
                        cmd.Parameters.Add(new SqlParameter("@ncred", notaCredito));
                        cmd.Parameters.Add(new SqlParameter("@ncargo", notaCargo));
                        cmd.Parameters.Add(new SqlParameter("@cpago", cPago));
                        cmd.Parameters.Add(new SqlParameter("@pf", pagosF));
                        cmd.Parameters.Add(new SqlParameter("@pv", pagosV));
                        cmd.Parameters.Add(new SqlParameter("@sol", solicitudes));
                        cmd.Parameters.Add(new SqlParameter("@rem", remisiones));
                        cmd.Parameters.Add(new SqlParameter("@ord", ordenes));
                        cmd.Parameters.Add(new SqlParameter("@id", idLog));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string obtenerFolioPagoFinanzas(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioActivo FROM folios WHERE idEmpresa = @idSucursal AND pagosFinanzas = 'true'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
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
        public void actualizarFolioPagosFinanzas(int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE folios SET folioActivo = folioActivo + 1 WHERE idEmpresa = @idSucursal AND pagosFinanzas = 1", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string obtenerFolioPagoVendedor(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioActivo FROM folios WHERE idEmpresa = @idSucursal AND pagosVendedor = 'true'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
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

        public void actualizarFolioPagosVendedor(int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE folios SET folioActivo = folioActivo + 1 WHERE idEmpresa = @idSucursal AND pagosVendedor = 1", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string[] obtenerFolio(string de, int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Serie, anoaprobacion, folioActivo FROM folios WHERE idEmpresa = @idSucursal AND " + de + " = 'true'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return new string[] { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString() };
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void actualizarFolio(string de, int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE folios SET folioActivo = folioActivo + 1 WHERE idEmpresa = @idSucursal AND " + de + " = 1", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
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