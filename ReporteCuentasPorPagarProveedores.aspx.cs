using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ReporteCuentasPorPagarProveedores : System.Web.UI.Page
    {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        double subTotal = 0, totalIva = 0, totalIsr = 0, totalGeneral = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Encabezado del reporte
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];

                //LabelNumeroCte.Text += dtDatosCliente.Rows[0]["CCODIGOCLIENTE"].ToString();
                //LabelNombreCte.Text += dtDatosCliente.Rows[0]["CRAZONSOCIAL"].ToString();


                if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null)
                {
                    idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                }
                else
                {
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

        public void llenarLV()
        {
            //Cadena de conexión para las distintas bases de datos
            string cadena;

            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("FECHA");
            dtcuenta.Columns.Add("CFOLIO");
            dtcuenta.Columns.Add("PROVEEDOR");
            dtcuenta.Columns.Add("CONCEPTO");
            dtcuenta.Columns.Add("SUBTOTAL");
            dtcuenta.Columns.Add("IVA");
            dtcuenta.Columns.Add("ISR");
            dtcuenta.Columns.Add("TOTAL");
            dtcuenta.Columns.Add("MONEDA");
            dtcuenta.Columns.Add("FECHAPAGO");
            dtcuenta.Columns.Add("BANCO");
            dtcuenta.Columns.Add("SUCURSAL");


            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);

            string validacion = "AND D.CPENDIENTE > 1";

            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            if (perfil != "1" && perfil != "1007") {
                DataTable dtCTA;
                switch (cSuc.id) {
                    //Saltillo concretos
                    case 1:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                    //Irapuato concretos
                    case 2:
                        //Concretos Irapuato Facturable
                        cadena = cContpaq.BDIraConcretos();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 9);

                        //Concretos Irapuato Ventas General
                        cadena = cContpaq.BDIraConcretosVG();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 10);
                        break;
                    //Saltillo Block
                    case 3:
                        //BLOCK SALTILLO FACTURABLE
                        cadena = cContpaq.BDBlockSalFac();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 3);

                        //BLOCK SALTILLO VENTAS GENERAL
                        cadena = cContpaq.BDBlockSalVG();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 4);
                        break;
                    //Irapuato Block
                    case 1006:
                        //BLOCK IRAPUATO Facturable
                        cadena = cContpaq.BDBlockIra();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 7);

                        //BLOCK IRAPUATO Ventas General
                        cadena = cContpaq.BDBlockIraVG();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 8);
                        break;
                    //Default: saltillo concretos
                    default:
                        //Concretos Saltillo Facturable
                        cadena = cContpaq.BDContpaq();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                }
            }
            else {
                //Obtener conexiones activas
                DataTable dtConexiones = cContpaq.obtenerConexiones();

                foreach (DataRow row in dtConexiones.Rows) {
                    cadena = row[5].ToString();
                    int sucursal = int.Parse(row[0].ToString());
                    
                    DataTable dtCTA = cContpaq.obtenerCuentasProveedores(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"], validacion);

                    llenarDataTable(dtCTA, dtcuenta, sucursal);
                }
            }

            lblSubtotal.Text = "$" + subTotal.ToString("#,##0.00");
            lblIva.Text = "$" + totalIva.ToString("#,##0.00");
            lblIsr.Text = "$" + totalIsr.ToString("#,##0.00");
            lblTotal.Text = "$" + totalGeneral.ToString("#,##0.00");

            llenarAgGrid(dtcuenta);

        }

        public void llenarAgGrid(DataTable dt) {
            var reporteCPagarP = new List<reporteCPagarProveedores>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporteCPagarP.Add(new reporteCPagarProveedores() { sucursal = dt.Rows[i]["Sucursal"].ToString(), proveedor = dt.Rows[i]["PROVEEDOR"].ToString(), fecha = dt.Rows[i]["FECHA"].ToString(), factura = dt.Rows[i]["CFOLIO"].ToString(), concepto = dt.Rows[i]["CONCEPTO"].ToString(), subtotal = dt.Rows[i]["SUBTOTAL"].ToString(), iva = dt.Rows[i]["IVA"].ToString(), isr = dt.Rows[i]["ISR"].ToString(), total = dt.Rows[i]["TOTAL"].ToString(), moneda = dt.Rows[i]["MONEDA"].ToString(), fechaV = dt.Rows[i]["FECHAPAGO"].ToString() });
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            var serializedResult = serializer.Serialize(reporteCPagarP);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }

        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta, int sucursal)
        {
            //Obtenemos los cargos del cliente seleccionado
            //DataTable dtCTA = cContpaq.obtenerCuentasProveedores(Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
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

            if (dtCTA == null)
            {
                DataRow data = dtcuenta.NewRow();
                data["FECHA"] = "#";
                data["CFOLIO"] = "#";
                data["PROVEEDOR"] = "No hay datos";
                data["CONCEPTO"] = "No hay datos";
                data["MONEDA"] = "-";
                data["FECHAPAGO"] = "#";
                data["SUCURSAL"] = Nombresucursal;
                data["SUBTOTAL"] = "$0";
                data["IVA"] = "$0";
                data["ISR"] = "$0";
                data["TOTAL"] = "$0";
                dtcuenta.Rows.Add(data);

            }
            else
            {
                string pivote = dtCTA.Rows[0]["CIDCLIENTEPROVEEDOR"].ToString();
                float tTotal = 0;
                float tTotalSucursal = 0;
                float tNeto = 0;
                float tNetoSucursal = 0;
                float tImpuesto = 0;
                float tImpuestoSucursal = 0;
                float tImpuesto2 = 0;
                float tImpuestoSucursal2 = 0;
                

                for (int i = 0; i < (dtCTA.Rows.Count); i++)
                {
                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                    DataRow rw = dtcuenta.NewRow();

                    DateTime fechaVencimiento = DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString());
                    DateTime fecha = DateTime.Parse(dtCTA.Rows[i]["CFECHA"].ToString());
                    rw["FECHA"] = fecha.ToShortDateString();
                    rw["CFOLIO"] = dtCTA.Rows[i]["CFOLIO"].ToString();
                    rw["PROVEEDOR"] = dtCTA.Rows[i]["PROVEEDOR"].ToString();
                    rw["CONCEPTO"] = dtCTA.Rows[i]["CNOMBREPRODUCTO"].ToString();
                    rw["MONEDA"] = dtCTA.Rows[i]["CCLAVESAT"].ToString();
                    rw["FECHAPAGO"] = fechaVencimiento.ToShortDateString();
                    rw["SUCURSAL"] = Nombresucursal;

                    float rwSubtotal = float.Parse(dtCTA.Rows[i]["SUBTOTAL"].ToString());
                    rw["SUBTOTAL"] = "$" + rwSubtotal.ToString("#,##0.00");
                    float rwIva = float.Parse(dtCTA.Rows[i]["IVA"].ToString());
                    rw["IVA"] = "$" + rwIva.ToString("#,##0.00");
                    float rwIsr = float.Parse(dtCTA.Rows[i]["ISR"].ToString());
                    rw["ISR"] = "$" + rwIsr.ToString("#,##0.00");
                    //float rwCIMPUESTO1 = float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
                    //rw["CIMPUESTO1"] = "$" + rwCIMPUESTO1.ToString("#,##0.00");
                    float rwCTOTAL = float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                    rw["TOTAL"] = "$" + rwCTOTAL.ToString("#,##0.00");

                    subTotal += rwSubtotal;
                    totalIva += rwIva;
                    totalIsr += rwIsr;
                    totalGeneral += rwCTOTAL;

                    if (pivote == dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString())
                    {
                        //Agregamos la fila a la tabla del reporte
                        dtcuenta.Rows.Add(rw);

                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                        tTotalSucursal = tTotalSucursal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());

                        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["SUBTOTAL"].ToString());
                        tNetoSucursal = tNetoSucursal + float.Parse(dtCTA.Rows[i]["SUBTOTAL"].ToString());

                        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["IVA"].ToString());
                        tImpuestoSucursal = tImpuestoSucursal + float.Parse(dtCTA.Rows[i]["IVA"].ToString());

                        tImpuesto2 = tImpuesto2 + float.Parse(dtCTA.Rows[i]["ISR"].ToString());
                        tImpuestoSucursal2 = tImpuestoSucursal2 + float.Parse(dtCTA.Rows[i]["ISR"].ToString());

                        if (i == dtCTA.Rows.Count - 1)
                        {
                            DataRow rw2 = dtcuenta.NewRow();
                            rw2["CONCEPTO"] = "<strong> " + "TOTAL" + "</strong>";
                            rw2["SUBTOTAL"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                            rw2["IVA"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                            rw2["ISR"] = "<strong> $" + tImpuesto2.ToString("#,##0.00") + "</strong>";

                            rw2["TOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                            //rw2["CNOMBREPRODUCTO"] = dtCTA.Rows[i - 1]["descripcion"].ToString();
                            //dtcuenta.Rows.Add(rw2);

                            DataRow rw3 = dtcuenta.NewRow();
                            rw3["CONCEPTO"] = "<strong> " + "TOTAL SUCURSAL" + "</strong>";
                            rw3["TOTAL"] = "<strong> $" + tTotalSucursal.ToString("#,##0.00") + "</strong>";
                            rw3["SUBTOTAL"] = "<strong> $" + tNetoSucursal.ToString("#,##0.00") + "</strong>";
                            rw3["IVA"] = "<strong> $" + tImpuestoSucursal.ToString("#,##0.00") + "</strong>";
                            rw3["ISR"] = "<strong> $" + tImpuestoSucursal2.ToString("#,##0.00") + "</strong>";
                            //dtcuenta.Rows.Add(rw3);
                        }
                    }
                    else
                    {
                        DataRow rw2 = dtcuenta.NewRow();
                        rw2["CONCEPTO"] = "<strong> " + "TOTAL" + "</strong>";
                        rw2["SUBTOTAL"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                        rw2["IVA"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                        rw2["ISR"] = "<strong> $" + tImpuesto2.ToString("#,##0.00") + "</strong>";

                        rw2["TOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                        //rw2["CNOMBREPRODUCTO"] = dtCTA.Rows[i - 1]["descripcion"].ToString();

                        //dtcuenta.Rows.Add(rw2);

                        //Agregamos la fila a la tabla del reporte
                        dtcuenta.Rows.Add(rw);
                        //
                        tTotal = 0;
                        tNeto = 0;

                        tImpuesto = 0;
                        tImpuesto2 = 0;

                        pivote = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();

                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                        tTotalSucursal = tTotalSucursal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["SUBTOTAL"].ToString());
                        tNetoSucursal = tNetoSucursal + float.Parse(dtCTA.Rows[i]["SUBTOTAL"].ToString());
                        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["IVA"].ToString());
                        tImpuestoSucursal = tImpuestoSucursal + float.Parse(dtCTA.Rows[i]["IVA"].ToString());
                        tImpuesto2 = tImpuesto2 + float.Parse(dtCTA.Rows[i]["ISR"].ToString());
                        tImpuestoSucursal2 = tImpuestoSucursal2 + float.Parse(dtCTA.Rows[i]["ISR"].ToString());


                        if (i == dtCTA.Rows.Count - 1)
                        {
                            rw2["CONCEPTO"] = "<strong> " + "TOTAL" + "</strong>";
                            rw2["SUBTOTAL"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                            rw2["IVA"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                            rw2["ISR"] = "<strong> $" + tImpuesto2.ToString("#,##0.00") + "</strong>";
                            rw2["TOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                            
                            DataRow rw3 = dtcuenta.NewRow();
                            rw3["CONCEPTO"] = "<strong> " + "TOTAL SUCURSAL" + "</strong>";
                            rw3["TOTAL"] = "<strong> $" + tTotalSucursal.ToString("#,##0.00") + "</strong>";
                            rw3["SUBTOTAL"] = "<strong> $" + tNetoSucursal.ToString("#,##0.00") + "</strong>";
                            rw3["ISR"] = "<strong> $" + tImpuestoSucursal2.ToString("#,##0.00") + "</strong>";
                            rw3["IVA"] = "<strong> $" + tImpuestoSucursal.ToString("#,##0.00") + "</strong>";
                            //dtcuenta.Rows.Add(rw3);
                        }

                    }
                }
                
            }
            lvCliente.DataSource = dtcuenta;
            lvCliente.DataBind();
        }
    }

    public class reporteCPagarProveedores {
        public string sucursal { get; set; }
        public string proveedor { get; set; }
        public string fecha { get; set; }
        public string factura { get; set; }
        public string concepto { get; set; }
        public string subtotal { get; set; }
        public string iva { get; set; }
        public string isr { get; set; }
        public string total { get; set; }
        public string moneda { get; set; }
        public string fechaV { get; set; }
    }
}