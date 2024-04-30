using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace despacho {
    public partial class reporteCuentaCliente : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        double sumaAbonos = 0;

        protected void Page_Load(object sender, EventArgs e) {
            int sucursal = int.Parse(Request.QueryString["idSucursal"]);
            string cadena;
            switch (sucursal) {
                case (1):
                    cadena = cContpaq.BDContpaq();
                    break;
                case (2):
                    cadena = cContpaq.BDConcrSalVG();
                    break;
                case (3):
                    cadena = cContpaq.BDBlockSalFac();
                    break;
                case (4):
                    cadena = cContpaq.BDBlockSalVG();
                    break;
                case (5):
                    cadena = cContpaq.BDBlockIra();
                    break;
                case (6):
                    cadena = cContpaq.BDIraConcretos();
                    break;
                case (7):
                    cadena = cContpaq.BDBlockIra();
                    break;
                case (8):
                    cadena = cContpaq.BDBlockIraVG();
                    break;
                case (9):
                    cadena = cContpaq.BDIraConcretos();
                    break;
                case (10):
                    cadena = cContpaq.BDIraConcretosVG();
                    break;
                default:
                    cadena = cContpaq.BDContpaq();
                    break;
            }
            DataTable dtDatosCliente = cContpaq.obtenerDatosCliente(cadena, int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]));
            if (!IsPostBack) {
                //Encabezado del reporte
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];
                LabelNumeroCte.Text += dtDatosCliente.Rows[0]["CCODIGOCLIENTE"].ToString();
                LabelNombreCte.Text += dtDatosCliente.Rows[0]["CRAZONSOCIAL"].ToString();



                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;";

                llenarLV();


            }
        }

        protected void llenarLV() {
            //Lista para abonos
            var listaAbonos = new List<String>();

            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("Fecha");
            dtcuenta.Columns.Add("Serie");
            dtcuenta.Columns.Add("Folio");
            dtcuenta.Columns.Add("Concepto");
            dtcuenta.Columns.Add("Cargos");
            dtcuenta.Columns.Add("Abonos");
            dtcuenta.Columns.Add("Documento");
            dtcuenta.Columns.Add("Vence");
            dtcuenta.Columns.Add("Ref");
            dtcuenta.Columns.Add("Sucursal");
            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);

            //Seleccionamos cuál cadena de conexión usar según la sucursal
            int sucursal = int.Parse(Request.QueryString["idSucursal"]);
            string cadena;
            switch (sucursal) {
                case (1):
                    cadena = cContpaq.BDContpaq();
                    break;
                case (2):
                    cadena = cContpaq.BDConcrSalVG();
                    break;
                case (3):
                    cadena = cContpaq.BDBlockSalFac();
                    break;
                case (4):
                    cadena = cContpaq.BDBlockSalVG();
                    break;
                case (5):
                    cadena = cContpaq.BDBlockIra();
                    break;
                case (6):
                    cadena = cContpaq.BDIraConcretos();
                    break;
                case (7):
                    cadena = cContpaq.BDBlockIra();
                    break;
                case (8):
                    cadena = cContpaq.BDBlockIraVG();
                    break;
                case (9):
                    cadena = cContpaq.BDIraConcretos();
                    break;
                case (10):
                    cadena = cContpaq.BDIraConcretosVG();
                    break;
                default:
                    cadena = cContpaq.BDContpaq();
                    break;
            }

            //Obtenemos los cargos del cliente seleccionado
            //DataTable dtCTA = cContpaq.obtenerCargosCliente(int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]), Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
            //DataTable dtCTA = cContpaq.obtenerCargosCliente(cadena, int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]), Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);

            //Buscar en otras sucursales
            string ccodigo = cContpaq.obtenerCCodigoCliente(cadena, int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]));
            if (ccodigo != null) {
                obtenerCargos(ccodigo, dtcuenta);

                double saldoInicial = obtenerSaldoInicial(ccodigo);

                lvCliente.DataSource = dtcuenta;
                lvCliente.DataBind();

                //llenar agGrid
                llenarAgGrid(dtcuenta);

                float totalitoAux = 0;
                float totalitoAux2 = 0;
                float totalitoAbonoAux = 0;
                float totalitoAbonoAux2 = 0;
                string referenciaAux = "";

                //ciclo para sumar el total de los cargos del cliente
                for (int i = 0; i < (dtcuenta.Rows.Count); i++) {
                    if (dtcuenta.Rows[i]["Cargos"].ToString().Replace(",", "") != null && dtcuenta.Rows[i]["Cargos"].ToString().Replace(",", "") != "") {
                        totalitoAux = float.Parse(dtcuenta.Rows[i]["Cargos"].ToString().Replace(",", ""));
                        totalitoAux2 = totalitoAux + totalitoAux2;
                    }

                }
                //Ciclo para sumar los abonos del cliente
                for (int i = 0; i < (dtcuenta.Rows.Count); i++) {
                    if (dtcuenta.Rows[i]["Abonos"].ToString() != null && dtcuenta.Rows[i]["Abonos"].ToString() != "") {
                        //validamos que la referencia no sea igual a la referencia del pago anterior para comprobar que sea un abono distinto
                        if (dtcuenta.Rows[i]["Ref"].ToString() != referenciaAux) {
                            totalitoAbonoAux = float.Parse(dtcuenta.Rows[i]["Abonos"].ToString());
                            totalitoAbonoAux2 = totalitoAbonoAux + totalitoAbonoAux2;
                            referenciaAux = dtcuenta.Rows[i]["Ref"].ToString();
                        }

                    }
                }
                saldoInicial *= (-1);
                double SaldoFinal = totalitoAux2 - sumaAbonos + saldoInicial;
                ////mostramos el total de cargos y abonos
                lblTotalCargos.Text = "$" + totalitoAux2.ToString("#,##0.00");
                lblTotalAbonos.Text = "$" + sumaAbonos.ToString("#,##0.00");
                //Resumen
                lblSaldoInicial.Text = "$" + saldoInicial.ToString("#,##0.00");
                lblCargos.Text = "$" + totalitoAux2.ToString("#,##0.00");
                lblAbonos.Text = "$" + sumaAbonos.ToString("#,##0.00");
                lblSaldoFinal.Text = "$" + SaldoFinal.ToString("#,##0.00");
            }



            //lblSumaAbonos.Text = "$" + sumaAbonos.ToString("#,##0.00");

        }

        public double obtenerSaldoInicial(string ccodigo) {
            DataTable dtConexiones = cContpaq.obtenerConexiones();
            double saldo = 0;
            foreach (DataRow dataRow in dtConexiones.Rows) {
                var listaAbonos = new List<String>();
                string cadena = dataRow[5].ToString();
                string nombreSucursal = dataRow[1].ToString();
                DataTable dtCTA = cContpaq.obtenerCargosClienteTodos(cadena, ccodigo, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                if (dtCTA == null) {
                    saldo += 0;
                }
                else {
                    for (int i = 0; i < (dtCTA.Rows.Count); i++) {

                        saldo += double.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());


                        //Obtenemos los abonos relacionados para el cargo encontrado
                        DataTable dtAbono = cContpaq.obtenerAbonoPorIdCargo(cadena, int.Parse(dtCTA.Rows[i]["CIDDOCUMENTO"].ToString()));
                        if (dtAbono != null) {
                            if (dtAbono.Rows.Count > 0) {
                                //ciclo para obtener los detalles de los abonos encontrados
                                for (int j = 0; j < dtAbono.Rows.Count; j++) {
                                    //Obtenemos los detalles del Abono relacionado al cargo
                                    DataTable dtAbonoDet = cContpaq.obtenerAbonoPorId(cadena, int.Parse(dtAbono.Rows[j]["CIDDOCUMENTOABONO"].ToString()));
                                    if (dtAbonoDet != null) {
                                        for (int k = 0; k < dtAbonoDet.Rows.Count; k++) {

                                            //Comparar para la suma de abonos

                                            if (listaAbonos.Contains(dtAbonoDet.Rows[k]["CFOLIO"].ToString())) {

                                            }
                                            else {
                                                listaAbonos.Add(dtAbonoDet.Rows[k]["CFOLIO"].ToString());
                                                saldo -= double.Parse(dtAbonoDet.Rows[k]["CTOTAL"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }


            }

            return saldo;
        }

        public void obtenerCargos(string ccodigo, DataTable dtcuenta) {
            string cadena2 = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

            //Buscar en otras sucursales
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.configContpaq WHERE activo = 1", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            foreach (DataRow dr in dt.Rows) {
                                var listaAbonos = new List<String>();
                                string cadena = dr[5].ToString();
                                string nombreSucursal = dr[1].ToString();
                                DataTable dtCTA = cContpaq.obtenerCargosClienteTodos(cadena, ccodigo, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                                if (dtCTA == null) {
                                    DataRow rw = dtcuenta.NewRow();
                                    rw["Fecha"] = "#";
                                    rw["Serie"] = "#";
                                    rw["Folio"] = "#";
                                    String conceptoCargo = "No hay datos";
                                    rw["Concepto"] = conceptoCargo;
                                    float ctotal = 0;
                                    rw["Cargos"] = ctotal.ToString("#,##0.00");
                                    rw["Abonos"] = "0";
                                    rw["Documento"] = "No hay datos";
                                    rw["Vence"] = "#";
                                    rw["Ref"] = "#";
                                    rw["Sucursal"] = nombreSucursal;
                                    //Agregamos la fila a la tabla del reporte
                                    dtcuenta.Rows.Add(rw);
                                }
                                else {
                                    for (int i = 0; i < (dtCTA.Rows.Count); i++) {
                                        //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                                        DataRow rw = dtcuenta.NewRow();
                                        rw["Fecha"] = dtCTA.Rows[i]["CFECHA"].ToString();
                                        rw["Serie"] = dtCTA.Rows[i]["CSERIEDOCUMENTO"].ToString();
                                        rw["Folio"] = dtCTA.Rows[i]["CFOLIO"].ToString();
                                        String conceptoCargo = cContpaq.obtenerConceptoPorId(cadena, int.Parse(dtCTA.Rows[i]["CIDCONCEPTODOCUMENTO"].ToString()));
                                        if (conceptoCargo != null) {
                                            rw["Concepto"] = conceptoCargo;
                                            float ctotal = float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                                            rw["Cargos"] = ctotal.ToString("#,##0.00");
                                            rw["Abonos"] = "";
                                            rw["Documento"] = dtCTA.Rows[i]["CPENDIENTE"].ToString();
                                            rw["Vence"] = DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString()).ToShortDateString();
                                            rw["Ref"] = "";
                                            rw["Sucursal"] = nombreSucursal;
                                            //Agregamos la fila a la tabla del reporte
                                            dtcuenta.Rows.Add(rw);

                                            //Obtenemos los abonos relacionados para el cargo encontrado
                                            DataTable dtAbono = cContpaq.obtenerAbonoPorIdCargo(cadena, int.Parse(dtCTA.Rows[i]["CIDDOCUMENTO"].ToString()));
                                            if (dtAbono != null) {
                                                if (dtAbono.Rows.Count > 0) {
                                                    //ciclo para obtener los detalles de los abonos encontrados
                                                    for (int j = 0; j < dtAbono.Rows.Count; j++) {
                                                        //Obtenemos los detalles del Abono relacionado al cargo
                                                        DataTable dtAbonoDet = cContpaq.obtenerAbonoPorId(cadena, int.Parse(dtAbono.Rows[j]["CIDDOCUMENTOABONO"].ToString()));
                                                        if (dtAbonoDet != null) {
                                                            for (int k = 0; k < dtAbonoDet.Rows.Count; k++) {
                                                                //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del abono
                                                                DataRow rw2 = dtcuenta.NewRow();
                                                                rw2["Fecha"] = dtAbonoDet.Rows[k]["CFECHA"].ToString();
                                                                rw2["Serie"] = dtAbonoDet.Rows[k]["CSERIEDOCUMENTO"].ToString();
                                                                rw2["Folio"] = dtAbonoDet.Rows[k]["CFOLIO"].ToString();
                                                                String conceptoAbono = cContpaq.obtenerConceptoPorId(cadena, int.Parse(dtAbonoDet.Rows[k]["CIDCONCEPTODOCUMENTO"].ToString()));
                                                                if (conceptoAbono != null) {
                                                                    rw2["Concepto"] = conceptoAbono;
                                                                    float ctotalAbono = float.Parse(dtAbonoDet.Rows[k]["CTOTAL"].ToString());
                                                                    rw2["Cargos"] = "";
                                                                    rw2["Abonos"] = ctotalAbono.ToString("#,##0.00");
                                                                    rw2["Documento"] = "";
                                                                    rw2["Vence"] = "";
                                                                    rw2["Ref"] = dtAbonoDet.Rows[k]["CREFERENCIA"].ToString();
                                                                    rw2["Sucursal"] = nombreSucursal;
                                                                    //Agregamos la fila a la tabla del reporte
                                                                    dtcuenta.Rows.Add(rw2);

                                                                    //Comparar para la suma de abonos

                                                                    if (listaAbonos.Contains(dtAbonoDet.Rows[k]["CFOLIO"].ToString())) {

                                                                    }
                                                                    else {
                                                                        listaAbonos.Add(dtAbonoDet.Rows[k]["CFOLIO"].ToString());
                                                                        sumaAbonos += double.Parse(dtAbonoDet.Rows[k]["CTOTAL"].ToString());
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
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

        public void llenarAgGrid(DataTable dt) {
            var reporteECC = new List<reporteECC>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                //Añadir movimiento a la lista
                reporteECC.Add(new reporteECC() { fecha = dt.Rows[i]["Fecha"].ToString(), serie = dt.Rows[i]["Serie"].ToString(), folio = dt.Rows[i]["Folio"].ToString(), concepto = dt.Rows[i]["Concepto"].ToString(), cargos = dt.Rows[i]["Cargos"].ToString(), abonos = dt.Rows[i]["Abonos"].ToString(), documento = dt.Rows[i]["Documento"].ToString(), referencia = dt.Rows[i]["Ref"].ToString(), vence = dt.Rows[i]["Vence"].ToString(), sucursal = dt.Rows[i]["Sucursal"].ToString() });

            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteECC);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
        //protected void exportarExcel()
        //{
        //    if (lvCliente.Items.Count == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            añadimos las cabeceras para la generacion del archivo
        //            Response.Clear();
        //            Response.AddHeader("content-disposition", "attachment;filename=ReporteCuentaCliente" + DateTime.Today.ToShortDateString() + ".xls");
        //            Response.Charset = "UTF-8";
        //            Response.ContentEncoding = System.Text.Encoding.Default;
        //            Response.ContentType = "application/vnd.ms-excel";
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //            instanciamos un objeto stringWriter
        //            using (StringWriter sw = new StringWriter())
        //            {
        //                instanciamos un objeto htmlTextWriter
        //                HtmlTextWriter hw = new HtmlTextWriter(sw);
        //                HtmlForm frm = new HtmlForm();

        //                sw.WriteLine();

        //                lvCliente.Parent.Controls.Add(frm);
        //                lvTotales.Parent.Controls.Add(frm);
        //                frm.Controls.Add(lvCliente);
        //                frm.Controls.Add(lvTotales);
        //                frm.RenderControl(hw);

        //                Response.Write(sw.ToString());

        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw (ex);
        //        }
        //        finally
        //        {
        //            Response.End();
        //        }
        //    }
        //}

    }
}

public class reporteECC {
    public string fecha { get; set; }
    public string serie { get; set; }
    public string folio { get; set; }
    public string concepto { get; set; }
    public string cargos { get; set; }
    public string abonos { get; set; }
    public string documento { get; set; }
    public string vence { get; set; }
    public string referencia { get; set; }
    public string sucursal { get; set; }
}

