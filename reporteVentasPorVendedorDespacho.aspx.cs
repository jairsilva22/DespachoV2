using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class reporteVentasPorVendedorDespacho : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                //Encabezado del reporte
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];


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
            }
        }

        public void llenarLV() {
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("SUCURSAL");
            dtcuenta.Columns.Add("VENDEDOR");
            dtcuenta.Columns.Add("CLIENTE");
            dtcuenta.Columns.Add("UNIDADESC");
            dtcuenta.Columns.Add("UNIDADESB");
            dtcuenta.Columns.Add("TOTALC");
            dtcuenta.Columns.Add("TOTALB");

            double unidadesc = 0, unidadesb = 0, totalc = 0, totalb = 0;

            DataTable dtCTA = cContpaq.obtenerVentasPorVendedorDespacho(Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
            if (dtCTA == null) {
                //No hay datos
                DataRow dr = dtcuenta.NewRow();
                dr["SUCURSAL"] = "No hay datos";
                dr["VENDEDOR"] = "#";
                dr["CLIENTE"] = "#";
                dr["UNIDADESC"] = "0";
                dr["UNIDADESB"] = "0";
                dr["TOTALC"] = "0";
                dr["TOTALB"] = "0";

            }
            else {
                for (int i = 0; i < dtCTA.Rows.Count; i++) {
                    //Row para guardar los datos en dtcuenta
                    DataRow dr = dtcuenta.NewRow();

                    //Nombre de sucursal
                    string sucursal = "";
                    switch (int.Parse(dtCTA.Rows[i]["planta"].ToString())) {
                        case 1: sucursal = "Concretos Saltillo"; break;
                        case 2: sucursal = "Concretos Irapuato"; break;
                        case 3: sucursal = "Block Saltillo"; break;
                        case 4: sucursal = "Concretos Saltillo"; break;
                        case 1006: sucursal = "Block Irapuato"; break;
                        default: sucursal = "Concretos Saltillo"; break;
                    }
                    string facturable = dtCTA.Rows[i]["reqFac"].ToString();
                    if (facturable.Equals("SI")) {
                        sucursal += " Facturable";
                    }
                    else {
                        sucursal += " Ventas General";
                    }
                    dr["SUCURSAL"] = sucursal;

                    //Nombre del vendedor - id vendedor de la tabla ordenes
                    dr["VENDEDOR"] = dtCTA.Rows[i]["vendedor"].ToString();

                    //Nombre del cliente - id cliente de la tabla solicitudes
                    dr["CLIENTE"] = dtCTA.Rows[i]["cliente"].ToString();

                    //Cantidad
                    string cantidad = dtCTA.Rows[i]["cantidad"].ToString();

                    //Tipo de material y asignar valores en 0 según la sucursal
                    string tipo =dtCTA.Rows[i]["tipo"].ToString().ToUpper();
                    if (tipo == "BLOCK") {
                        //Ventas en pesos
                        double venta = double.Parse(cantidad) * double.Parse(dtCTA.Rows[i]["precioU"].ToString());
                        dr["TOTALB"] = "$" + venta.ToString("#,##0.00");
                        //línea de totales
                        unidadesb += double.Parse(cantidad);
                        totalb += venta;

                        cantidad += " PZA";
                        dr["UNIDADESC"] = "-";
                        dr["UNIDADESB"] = cantidad;
                        dr["TOTALC"] = "-";
                    }
                    else {
                        //Ventas en pesos
                        double venta = double.Parse(cantidad) * double.Parse(dtCTA.Rows[i]["precioU"].ToString());
                        dr["TOTALC"] = "$" + venta.ToString("#,##0.00");
                        //línea de totales
                        unidadesc += double.Parse(cantidad);
                        totalc += venta;

                        cantidad += "M3";
                        dr["UNIDADESB"] = "-";
                        dr["UNIDADESC"] = cantidad;
                        dr["TOTALB"] = "-";
                    }
                    dtcuenta.Rows.Add(dr);
                }
            }

            //Llenar AGGrid
            llenarAgGrid(dtcuenta);

            //Colocar totales
            lblTotalBlock.Text = unidadesb.ToString("#,##0.0") + " PZA";
            lblTotalBlockD.Text = "$" + totalb.ToString("#,##0.00");
            lblTotalConcreto.Text = unidadesc.ToString("#,##0.0") + " M3";
            lblTotalConcretoD.Text = "$" + totalc.ToString("#,##0.00");
        }

        public void llenarAgGrid(DataTable dt) {
            var reporteVpV = new List<reporteVpV>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporteVpV.Add(new reporteVpV() { sucursal = dt.Rows[i]["SUCURSAL"].ToString(), vendedor = dt.Rows[i]["VENDEDOR"].ToString(), cliente = dt.Rows[i]["CLIENTE"].ToString(), concretoU = dt.Rows[i]["UNIDADESC"].ToString(), blockU = dt.Rows[i]["UNIDADESB"].ToString(), concretoD = dt.Rows[i]["TOTALC"].ToString(), blockD = dt.Rows[i]["TOTALB"].ToString() });
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteVpV);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
    }
}