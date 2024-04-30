using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;

namespace despacho {
    public partial class reporteVentasPorProductos : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        double totalGeneral = 0, totalDescuento = 0;
        int entradas = 0;
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //Encabezado del reporte
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];


                //if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null) {
                //    idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                //}
                //else {
                    
                //}
                idSucursal = int.Parse(Request.QueryString["idSucursal"]);
                cSuc.id = idSucursal;
                //cSuc.nombre = cSuc.obtenerNombreSucursalByID(idSucursal);

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;";

                llenarLV();
            }
        }

        protected void llenarLV() {
            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("CCODIGOPRODUCTO");
            dtcuenta.Columns.Add("CNOMBREPRODUCTO");
            dtcuenta.Columns.Add("CUnidades");
            dtcuenta.Columns.Add("CPRECIOPROMEDIO");
            //dtcuenta.Columns.Add("CTIPOPRODUCTO");
            dtcuenta.Columns.Add("CNETO");
            dtcuenta.Columns.Add("CDESCUENTO1");
            dtcuenta.Columns.Add("Neto-Desc");
            dtcuenta.Columns.Add("CIMPUESTO1");
            dtcuenta.Columns.Add("CTOTAL");
            dtcuenta.Columns.Add("SUCURSAL");
            

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            if (perfil != "1" && perfil != "1007") {
                DataTable dtCTA;
                string cadena;
                switch (cSuc.id) {
                    //Saltillo concretos
                    case 1:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Saltillo Facturable", 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Saltillo Ventas General", 2);
                        break;
                    //Irapuato concretos
                    case 2:
                        //Concretos Irapuato Facturable
                        cadena = cContpaq.BDIraConcretos();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Irapuato Facturable", 9);

                        //Concretos Irapuato Ventas General
                        cadena = cContpaq.BDIraConcretosVG();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Irapuato Ventas General", 10);
                        break;
                    //Saltillo Block
                    case 3:
                        //BLOCK SALTILLO FACTURABLE
                        cadena = cContpaq.BDBlockSalFac();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Block Saltillo Facturable", 3);

                        //BLOCK SALTILLO VENTAS GENERAL
                        cadena = cContpaq.BDBlockSalVG();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Block Saltillo Ventas General", 4);
                        break;
                    //Irapuato Block
                    case 1006:
                        //BLOCK IRAPUATO Facturable
                        cadena = cContpaq.BDBlockIra();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Block Irapuato Facturable", 7);

                        //BLOCK IRAPUATO Ventas General
                        cadena = cContpaq.BDBlockIraVG();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Block Irapuato Ventas General", 8);
                        break;
                    //Default: saltillo concretos
                    default:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Saltillo Facturable", 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, "Concretos Saltillo Ventas General", 2);
                        break;
                }
            }
            else {
                //Obtener conexiones de las sucursales activas
                DataTable dtConexiones = cContpaq.obtenerConexiones();
                foreach (DataRow row in dtConexiones.Rows) {
                    string cadena = row[5].ToString(), nombreSucursal = row[1].ToString();
                    int idSucursal = int.Parse(row[0].ToString());
                    //Obtenemos los cargos del cliente seleccionado
                    DataTable dtCTA = cContpaq.obtenerVentasPorProducto(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                    if (dtCTA == null) {
                        labelError.Text = "NO SE ENCUENTRAN DATOS";
                        DataRow data = dtcuenta.NewRow();
                        data["CCODIGOPRODUCTO"] = "#";
                        data["CNOMBREPRODUCTO"] = "No hay datos para mostrar";
                        data["CUnidades"] = "0";
                        data["CPRECIOPROMEDIO"] = "$0";
                        data["CDESCUENTO1"] = "0%";
                        data["CTOTAL"] = "$0";
                        data["SUCURSAL"] = nombreSucursal;
                        dtcuenta.Rows.Add(data);
                    }
                    else {

                        llenarDataTable(dtCTA, dtcuenta, nombreSucursal, idSucursal);

                    }
                }
            }

            
            lvCliente.DataSource = dtcuenta;
            lvCliente.DataBind();
            lblTotal.Text = "$" + totalGeneral.ToString("#,##0.00");
            lblDescuentos.Text = (totalDescuento/entradas).ToString("0.0#") + "%";
            llenarAgGrid(dtcuenta);
        }

        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta, string nombreSucursal, int idSucursal) {
            if (dtCTA == null) {
                labelError.Text = "NO SE ENCUENTRAN DATOS";
                DataRow data = dtcuenta.NewRow();
                data["CCODIGOPRODUCTO"] = "#";
                data["CNOMBREPRODUCTO"] = "No hay datos para mostrar";
                data["CUnidades"] = "0";
                data["CPRECIOPROMEDIO"] = "$0";
                data["CDESCUENTO1"] = "0%";
                data["CTOTAL"] = "$0";
                data["SUCURSAL"] = nombreSucursal;
                dtcuenta.Rows.Add(data);
            }
            else {
                for (int i = 0; i < (dtCTA.Rows.Count); i++) {
                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                    DataRow dataRow = dtcuenta.NewRow();
                    double precioPromedio = 0, descuentoPorcentaje = 0;

                    string tipoUnidad;
                    if (idSucursal == 3 || idSucursal == 4 || idSucursal == 5 || idSucursal == 7 || idSucursal == 8) {
                        tipoUnidad = " PZA";
                    }
                    else {
                        tipoUnidad = " M3";
                    }

                    double neto = double.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                    double netoDesc = double.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                    double unidades = double.Parse(dtCTA.Rows[i]["CUnidades"].ToString());

                    if (unidades != 0) {
                        precioPromedio = netoDesc / unidades;
                    }
                    if (neto != 0) {
                        descuentoPorcentaje = (100 * double.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString()) / neto);
                    }

                    double cTotal = double.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());

                    dataRow["CCODIGOPRODUCTO"] = dtCTA.Rows[i]["CCODIGOPRODUCTO"].ToString();
                    dataRow["CNOMBREPRODUCTO"] = dtCTA.Rows[i]["CNOMBREPRODUCTO"].ToString();
                    dataRow["CUnidades"] = dtCTA.Rows[i]["CUnidades"].ToString() + tipoUnidad;
                    dataRow["CPRECIOPROMEDIO"] = "$" + precioPromedio.ToString("#,##0.00");
                    dataRow["CDESCUENTO1"] = descuentoPorcentaje.ToString("0.0#") + "%";
                    dataRow["CTOTAL"] = cTotal.ToString("#,##0.00");
                    dataRow["SUCURSAL"] = nombreSucursal;

                    dtcuenta.Rows.Add(dataRow);
                    totalGeneral += cTotal;
                    totalDescuento += descuentoPorcentaje;
                    entradas++;
                }
            }
            
        }

        public void llenarAgGrid(DataTable dt) {
            var reporteVpP = new List<reporteVpP>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporteVpP.Add(new reporteVpP() { sucursal = dt.Rows[i]["Sucursal"].ToString(), codigo = dt.Rows[i]["CCODIGOPRODUCTO"].ToString(), nombre = dt.Rows[i]["CNOMBREPRODUCTO"].ToString(), cantidad = dt.Rows[i]["CUnidades"].ToString(), precioPromedio = dt.Rows[i]["CPRECIOPROMEDIO"].ToString(), descuento = dt.Rows[i]["CDESCUENTO1"].ToString(), total = dt.Rows[i]["CTOTAL"].ToString() });
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteVpP);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
    }

    public class reporteVpP {
        public string codigo { get; set; }
        public string sucursal { get; set; }
        public string nombre { get; set; }
        public string cantidad { get; set; }
        public string precioPromedio { get; set; }
        public string descuento { get; set; }
        public string total { get; set; }

    }
}