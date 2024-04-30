using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class Folios
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades
        public int idLogs { get; set; }
        public int idEmpresa { get; set; }
        public long folioInicio { get; set; }
        public long folioFinal { get; set; }
        public long folioActivo { get; set; }
        public string vigencia { get; set; }
        public string serie { get; set; }
        public bool factura { get; set; }
        public bool nCredito { get; set; }
        public bool nCargo { get; set; }
        public int idusuario { get; set; }
        public DateTime fechaAlta { get; set; }
        public long naprobacion { get; set; }
        public bool activo { get; set; }

        //constructor
        public Folios()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        #region Procesos
        //metodo para obtener los datos de los folios
        public void foliosProcesos(string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "Factura":
                        comando = "SELECT * FROM dbo.folios WHERE idEmpresa = @empresa AND factura = 1";
                        break;
                    case "Nota de Credito":
                        comando = "SELECT * FROM dbo.folios WHERE idEmpresa = @empresa AND nCredito = 1";
                        break;
                    case "Nota de Cargo":
                        comando = "SELECT * FROM dbo.folios WHERE idEmpresa = @empresa AND nCargo = 1";
                        break;
                    case "Pago":
                        comando = "SELECT * FROM dbo.folios WHERE idEmpresa = @empresa AND cPago = 1";
                        break;
                    default:
                        break;
                }
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@empresa", idEmpresa));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idLogs = int.Parse(reader["idLogs"].ToString());
                                    idEmpresa = int.Parse(reader["idEmpresa"].ToString());
                                    folioInicio = long.Parse(reader["folioInicio"].ToString());
                                    folioFinal = long.Parse(reader["folioFinal"].ToString());
                                    folioActivo = long.Parse(reader["folioActivo"].ToString());
                                    // vigencia = reader["vigencia"].ToString();
                                    serie = reader["Serie"].ToString();
                                    factura = bool.Parse(reader["factura"].ToString());
                                    nCredito = bool.Parse(reader["nCredito"].ToString());
                                    nCargo = bool.Parse(reader["nCargo"].ToString());
                                    //idusuario = int.Parse(reader["idusuario"].ToString());
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
        #endregion

        //SELLADO---------------------------------------------------------------------------------------------------------------------------------------------------
        //metodo para buscar los folios
        public void buscarFolios()
        {
            try
            {
                comando = "SELECT idLogs, folioInicio, folioFinal, folioActivo, serie FROM dbo.folios WHERE idLogs = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idLogs));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idLogs = int.Parse(reader["idLogs"].ToString());
                                    folioInicio = long.Parse(reader["folioInicio"].ToString());
                                    folioFinal = long.Parse(reader["folioFinal"].ToString());
                                    folioActivo = long.Parse(reader["folioActivo"].ToString());
                                    serie = reader["serie"].ToString();
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

        //metodo para actualizar el folio activo
        public void actualizarFolio(long folio)
        {
            try
            {
                comando = "UPDATE folios SET folioActivo = @folio WHERE idLogs = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@id", idLogs));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        //metodo para actualizar el folio
        public void actualizarFolio(string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "Factura":
                        comando = "UPDATE folios SET folioActivo = (folioActivo + 1) WHERE idEmpresa = @empresa AND factura = 1";
                        break;
                    case "Nota de Credito":
                        comando = "UPDATE folios SET folioActivo = (folioActivo + 1) WHERE idEmpresa = @empresa AND nCredito = 1";
                        break;
                    case "Nota de Cargo":
                        comando = "UPDATE folios SET folioActivo = (folioActivo + 1) WHERE idEmpresa = @empresa AND nCargo = 1";
                        break;
                    case "Pago":
                        comando = "UPDATE folios SET folioActivo = (folioActivo + 1) WHERE idEmpresa = @empresa AND cPago = 1";
                        break;
                    default:
                        break;
                }
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@empresa", idEmpresa));
                        int filas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        //metodo para limpiar variables
        public void limpiar()
        {
            idLogs = 0;
            folioInicio = 0;
            folioFinal = 0;
            folioActivo = 0;
            serie = "";
        }
    }
}