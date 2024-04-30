using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;

namespace despacho {
    public partial class ReportesVentasPorVendedor : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        double totalBlocks = 0;
        double totalConcreto = 0, totalDineroBlock = 0, totalDineroConcreto = 0;

        protected void Page_Load(object sender, EventArgs e) {
            //DataTable dtDatosCliente = cContpaq.obtenerDatosCliente(int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]));
            if (!IsPostBack) {
                //Encabezado del reporte
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];
                //LabelNumeroCte.Text += dtDatosCliente.Rows[0]["CCODIGOCLIENTE"].ToString();
                //LabelNombreCte.Text += dtDatosCliente.Rows[0]["CRAZONSOCIAL"].ToString();


                if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null) {
                    idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                }
                else {
                    idSucursal = int.Parse(Request.QueryString["idSucursal"]);
                }

                cSuc.id = idSucursal;
                cSuc.nombre = cSuc.obtenerNombreSucursalByID(idSucursal);

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;";

                llenarLV();

                //if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                //{
                //    //exportarExcel();

                //    if (lvCliente.Items.Count == 0)
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                //    }
                //    Response.Write("<script> window.close(); </script>");
                //}

            }
        }

        protected void llenarLV() {
            //Cadena de conexión para las distintas bases de datos
            string cadena;

            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();

            //Codigio del Vendedor
            dtcuenta.Columns.Add("CCODIGOAGENTE");
            //Nombre del Vendedor
            dtcuenta.Columns.Add("CNOMBREAGENTE");
            //Nombre del cliente
            dtcuenta.Columns.Add("CRAZONSOCIAL");
            //Codigo del Producto
            dtcuenta.Columns.Add("CCODIGOPRODUCTO");
            //Unidades del Producto Concreto
            dtcuenta.Columns.Add("CUnidades");
            //Unidades del producto Block
            dtcuenta.Columns.Add("CUnidadesB");

            dtcuenta.Columns.Add("CNETO");
            dtcuenta.Columns.Add("CDESCUENTO1");
            dtcuenta.Columns.Add("Neto-Desc");
            dtcuenta.Columns.Add("CIMPUESTO1");
            //Total del concreto
            dtcuenta.Columns.Add("CTOTAL");
            //Totala del block
            dtcuenta.Columns.Add("CTOTALB");
            dtcuenta.Columns.Add("Sucursal");


            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            
            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            if (perfil != "1" && perfil != "1007") {
                DataTable dtCTA;
                switch (cSuc.id) {
                    //Saltillo concretos
                    case 1:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                    //Irapuato concretos
                    case 2:
                        //Concretos Irapuato Facturable
                        cadena = cContpaq.BDIraConcretos();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 9);

                        //Concretos Irapuato Ventas General
                        cadena = cContpaq.BDIraConcretosVG();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 10);
                        break;
                    //Saltillo Block
                    case 3:
                        //BLOCK SALTILLO FACTURABLE
                        cadena = cContpaq.BDBlockSalFac();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 3);

                        //BLOCK SALTILLO VENTAS GENERAL
                        cadena = cContpaq.BDBlockSalVG();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 4);
                        break;
                    //Irapuato Block
                    case 1006:
                        //BLOCK IRAPUATO Facturable
                        cadena = cContpaq.BDBlockIra();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 7);

                        //BLOCK IRAPUATO Ventas General
                        cadena = cContpaq.BDBlockIraVG();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 8);
                        break;
                    //Default: saltillo concretos
                    default:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                }
            }
            else {
                //Consultamos según las sucursales que estén activas

                DataTable dtConexiones = cContpaq.obtenerConexiones();
                foreach (DataRow row in dtConexiones.Rows) {
                    cadena = row[5].ToString();
                    int intSucursal = int.Parse(row[0].ToString());
                    DataTable dtCTA = cContpaq.obtenerVentasPorVendedor(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                    llenarDataTable(dtCTA, dtcuenta, intSucursal);
                }

            }
            lblTotalBlock.Text = totalBlocks.ToString("#,##0.0#") + " PZA";
            lblTotalConcreto.Text = totalConcreto.ToString("#,##0.#") + " M3";
            lblTotalBlockD.Text = "$" + totalDineroBlock.ToString("#,##0.00");
            lblTotalConcretoD.Text = "$" + totalDineroConcreto.ToString("#,##0.00");

            llenarAgGrid(dtcuenta);

        }

        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta, int sucursal) {
            string Nombresucursal;

            switch (sucursal) {
                case (1):
                    Nombresucursal = "Concretos Saltillo Facturable";
                    break;
                case (2):
                    Nombresucursal = "Concretos Saltillo Ventas General";
                    break;
                case (3):
                    Nombresucursal = "Block Saltillo Facturable";
                    break;
                case (4):
                    Nombresucursal = "Block Saltillo Ventas General";
                    break;
                case (5):
                    Nombresucursal = "Block Irapuato";
                    break;
                case (6):
                    Nombresucursal = "Irapuato Concretos";
                    break;
                case (7):
                    Nombresucursal = "Block Irapuato Facturable";
                    break;
                case (8):
                    Nombresucursal = "Block Irapuato Ventas General";
                    break;
                case (9):
                    Nombresucursal = "Concretos Irapuato Facturable";
                    break;
                case (10):
                    Nombresucursal = "Concretos Irapuato Ventas General";
                    break;
                default:
                    Nombresucursal = "Concretos Saltillo Facturable";
                    break;
            }

            if (dtCTA == null) {
                //labelError.Text = "NO SE ENCUENTRAN DATOS";
                DataRow data = dtcuenta.NewRow();
                data["CNOMBREAGENTE"] = "#";
                data["CRAZONSOCIAL"] = "No hay datos para mostrar";
                data["CUnidades"] = "0 M3";
                data["CUnidadesB"] = "0 PZA";
                data["CTOTAL"] = "$0";
                data["CTOTALB"] = "$0";
                data["SUCURSAL"] = Nombresucursal;
                dtcuenta.Rows.Add(data);
            }
            else {

                string pivote = dtCTA.Rows[0]["CIDDOCUMENTO"].ToString();

                float tTotal = 0;
                float tCantidad = 0;
                float tNeto = 0;
                float tDescuento = 0;
                float tNetoDesc = 0;
                float tImpuesto = 0;
                

                for (int i = 0; i < (dtCTA.Rows.Count); i++) {
                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                    DataRow rw = dtcuenta.NewRow();

                    rw["CCODIGOAGENTE"] = dtCTA.Rows[i]["CCODIGOAGENTE"].ToString();
                    rw["CNOMBREAGENTE"] = dtCTA.Rows[i]["CNOMBREAGENTE"].ToString();
                    rw["CRAZONSOCIAL"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                    rw["SUCURSAL"] = Nombresucursal;

                    //Validamos las unidades de los productos por la codigo del producto

                    if (sucursal == 3 || sucursal == 4 || sucursal == 5 || sucursal == 7 || sucursal == 8) {
                        rw["CUnidades"] = "-";
                        rw["CUnidadesB"] = dtCTA.Rows[i]["CUnidades"].ToString() + "PZA";
                        totalBlocks += double.Parse(dtCTA.Rows[i]["CUnidades"].ToString().Replace("PZA", ""));
                    }
                    else {
                        rw["CUnidades"] = dtCTA.Rows[i]["CUnidades"].ToString() + " M3";
                        rw["CUnidadesB"] = "-";
                        totalConcreto += double.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
                    }

                    float rwNETO = float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                    rw["CNETO"] = "$" + rwNETO.ToString("#,##0.00");
                    float rwCDESCUENTO1 = float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                    rw["CDESCUENTO1"] = "$" + rwCDESCUENTO1.ToString("#,##0.00");
                    float rwNETODESC = float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                    rw["Neto-Desc"] = "$" + rwNETODESC.ToString("#,##0.00");
                    float rwCIMPUESTO1 = float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
                    rw["CIMPUESTO1"] = "$" + rwCIMPUESTO1.ToString("#,##0.00");

                    //Validamos el total de cada producto por codigo de producto
                    if (sucursal == 3 || sucursal == 4 || sucursal == 5 || sucursal == 7 || sucursal == 8) {
                        rw["CTOTAL"] = "-";
                        float rwCTOTAL = float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        rw["CTOTALB"] = "$" + rwCTOTAL.ToString("#,##0.00");
                        totalDineroBlock += rwCTOTAL;
                    }
                    else {

                        float rwCTOTAL = float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        rw["CTOTAL"] = "$" + rwCTOTAL.ToString("#,##0.00");
                        rw["CTOTALB"] = "-";
                        totalDineroConcreto += rwCTOTAL;

                    }


                    if (pivote == dtCTA.Rows[i]["CCODIGOAGENTE"].ToString()) {
                        //Agregamos la fila a la tabla del reporte
                        dtcuenta.Rows.Add(rw);
                        //
                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
                        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                        tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

                        //if (i == dtCTA.Rows.Count - 1) {

                        //    DataRow rw2 = dtcuenta.NewRow();
                        //    rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong> $";
                        //    rw2["CUnidades"] = "<strong>" + tCantidad + "</strong>" + " M3";
                        //    rw2["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong> $";
                        //    rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong> $";
                        //    rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong> $";
                        //    rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong> $";
                        //    rw2["CRAZONSOCIAL"] = "<strong>TOTAL</strong>";
                        //    dtcuenta.Rows.Add(rw2);
                        //}

                    }
                    else {
                        DataRow rw2 = dtcuenta.NewRow();
                        rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                        rw2["CUnidades"] = "<strong> " + tCantidad + "</strong>" + " M3";
                        rw2["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                        rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
                        rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
                        rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                        rw2["CRAZONSOCIAL"] = "<strong>TOTAL</strong>";

                        //dtcuenta.Rows.Add(rw2);

                        //Agregamos la fila a la tabla del reporte
                        dtcuenta.Rows.Add(rw);
                        tTotal = 0;
                        tCantidad = 0;
                        tNeto = 0;
                        tDescuento = 0;
                        tNetoDesc = 0;
                        tImpuesto = 0;
                        pivote = dtCTA.Rows[i]["CCODIGOAGENTE"].ToString();

                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
                        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                        tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

                        //if (i == dtCTA.Rows.Count - 1) {

                        //    DataRow rw3 = dtcuenta.NewRow();
                        //    rw3["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                        //    rw3["CUnidades"] = "<strong>" + tCantidad + "</strong>";
                        //    rw3["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                        //    rw3["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
                        //    rw3["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
                        //    rw3["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                        //    rw3["CRAZONSOCIAL"] = "<strong>TOTAL</strong>";

                        //    dtcuenta.Rows.Add(rw3);
                        //}

                    }
                }

                lvCliente.DataSource = dtcuenta;
                lvCliente.DataBind();

                
            }

        }

        public void llenarAgGrid(DataTable dt) {
            var reporteVpV = new List<reporteVpV>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporteVpV.Add(new reporteVpV() { sucursal = dt.Rows[i]["Sucursal"].ToString(), vendedor = dt.Rows[i]["CNOMBREAGENTE"].ToString(), cliente = dt.Rows[i]["CRAZONSOCIAL"].ToString(), concretoU = dt.Rows[i]["CUnidades"].ToString(), blockU = dt.Rows[i]["CUnidadesB"].ToString(), concretoD = dt.Rows[i]["CTOTAL"].ToString(), blockD = dt.Rows[i]["CTOTALB"].ToString() });
            }
            
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteVpV);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
    }

    public class reporteVpV {
        public string vendedor { get; set; }
        public string cliente { get; set; }
        public string concretoU { get; set; }
        public string blockU { get; set; }
        public string concretoD { get; set; }
        public string blockD { get; set; }
        public string sucursal { get; set; }
    }
}
