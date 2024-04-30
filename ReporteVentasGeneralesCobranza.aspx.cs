using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class ReporteVentasGeneralesCobranza : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cSucursales cSuc = new cSucursales();
        cContpaq cContpaq = new cContpaq();
        int ndtCuenta = 0;
        double TotalVencido = 0;
        double PorVencer = 0;

        protected void Page_Load(object sender, EventArgs e) {
            lblFechaFin.Text += Request.QueryString["FechaInicio"];

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

        public string ToFormat24h(DateTime dt) {
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public void llenarAgGrid(DataTable dt) {
            var reporteVGC = new List<reporteVGC>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                //Añadir movimiento a la lista
                reporteVGC.Add(new reporteVGC() { codigo = dt.Rows[i]["Codigo"].ToString(), nomCliente = dt.Rows[i]["Nombre"].ToString(), nomAgente = dt.Rows[i]["Agente"].ToString(), totVencido = dt.Rows[i]["TotalVencido"].ToString(), totAVencer = dt.Rows[i]["TotalporVencer"].ToString(), total = dt.Rows[i]["Total"].ToString(), sucursal = dt.Rows[i]["Sucursal"].ToString() });

            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteVGC);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }


        protected void llenarLV() {
            //Cadena de conexión para las distintas bases de datos
            string cadena;

            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("Codigo");
            dtcuenta.Columns.Add("Nombre");
            dtcuenta.Columns.Add("Agente");
            dtcuenta.Columns.Add("TotalVencido");
            dtcuenta.Columns.Add("TotalporVencer");
            dtcuenta.Columns.Add("Total");
            dtcuenta.Columns.Add("Sucursal");

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);

            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            
            if (perfil != "1" && perfil != "1007") {
                //lblError.Text = "No debe ver todo" + perfil;
                DataTable dtCTA;
                switch (cSuc.id) {
                    //Saltillo concretos
                    case 1:
                        //Concretos Saltillo Facturable
                        dtCTA = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                    //Irapuato concretos
                    case 2:
                        //Concretos Irapuato Facturable
                        cadena = cContpaq.BDIraConcretos();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 9);

                        //Concretos Irapuato Ventas General
                        cadena = cContpaq.BDIraConcretosVG();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 10);
                        break;
                    //Saltillo Block
                    case 3:
                        //BLOCK SALTILLO FACTURABLE
                        cadena = cContpaq.BDBlockSalFac();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 3);

                        //BLOCK SALTILLO VENTAS GENERAL
                        cadena = cContpaq.BDBlockSalVG();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 4);
                        break;
                    //Irapuato Block
                    case 1006:
                        //BLOCK IRAPUATO Facturable
                        cadena = cContpaq.BDBlockIra();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 7);

                        //BLOCK IRAPUATO Ventas General
                        cadena = cContpaq.BDBlockIraVG();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 8);
                        break;
                    //Default: saltillo concretos
                    default:
                        //Concretos Saltillo Facturable
                        dtCTA = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 1);

                        //Concretos Saltillo Ventas General
                        cadena = cContpaq.BDConcrSalVG();
                        dtCTA = cContpaq.obtenerVentasGenerales(cadena, Request.QueryString["FechaInicio"]);
                        //DataTable dtCTAporVencer = cContpaq.obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
                        llenarDataTable(dtCTA, dtcuenta, 2);
                        break;
                }

            }
            else {

                busquedaAutomatizada(Request.QueryString["FechaInicio"], dtcuenta);
                
            }

            double totalEmpresa = TotalVencido + PorVencer;
            lblTotal.Text = "$" + totalEmpresa.ToString("#,##0.00");
            lblTotalVencer.Text = "$" + PorVencer.ToString("#,##0.00");
            lblTotalVencido.Text = "$" + TotalVencido.ToString("#,##0.00");

            llenarAgGrid(dtcuenta);
        }

        //Búsqueda en otras sucursales de forma automatizada
        public void busquedaAutomatizada(string fechaInicio, DataTable dtcuenta) {
            string cadena2 = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            DataTable dtCTA;
            int i;
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.configContpaq WHERE (activo = 1)", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            foreach (DataRow dr in dt.Rows) {
                                i = int.Parse(dr[0].ToString());
                                string cadena = dr[5].ToString();
                                dtCTA = cContpaq.obtenerVentasGenerales(cadena, fechaInicio);
                                llenarDataTable(dtCTA, dtcuenta, i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Llenar datatable con un método aplicable a todas las sucursales
        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta, int sucursal) {
            
            string pivoteCliente = "";
            string Nombresucursal;
            //string pivote2 = dtCTAporVencer.Rows[0]["CIDAGENTE"].ToString();

           
            double vencer = 0, vencido = 0;
            

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
                DataRow rw = dtcuenta.NewRow();
                rw["Codigo"] = "#";
                rw["Nombre"] = "No hay datos";
                rw["Agente"] = "No hay datos";
                float rwTotalVencido = 0;
                float rwTotal = 0;
                rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                rw["TotalporVencer"] = "$" + rwTotalVencido.ToString("#,##0.00");
                rw["TotalVencido"] = "$0";
                rw["Sucursal"] = Nombresucursal;
                dtcuenta.Rows.Add(rw);
                ndtCuenta++;
                
            }
            else {
                for (int i = 0; i < (dtCTA.Rows.Count); i++) {

                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta
                    DataRow rw = dtcuenta.NewRow();


                    // Por Vencer
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                        rw["Codigo"] = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();
                        rw["Nombre"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                        rw["Agente"] = dtCTA.Rows[i]["CNOMBREAGENTE"].ToString();
                        float rwTotalVencido = float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                        float rwTotal = float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                        rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                        rw["TotalporVencer"] = "$" + rwTotalVencido.ToString("#,##0.00");
                        rw["TotalVencido"] = "$0";
                        vencer = double.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                    }

                    //Vencidos
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                        rw["Codigo"] = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();
                        rw["Nombre"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                        rw["Agente"] = dtCTA.Rows[i]["CNOMBREAGENTE"].ToString();
                        float rwTotal = float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                        rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                        float rwTotalVencido = float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                        rw["TotalVencido"] = "$" + rwTotalVencido.ToString("#,##0.00");
                        rw["TotalporVencer"] = "$0";
                        vencido = double.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                    }

                    //vencido
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                        TotalVencido += float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                    }

                    //Por Vencer
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                        PorVencer += float.Parse(dtCTA.Rows[i]["CPENDIENTE"].ToString());
                    }



                    //No mostrar filas del mismo cliente con el mismo agente
                    if (pivoteCliente == dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()) {
                        // Por Vencer
                        if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                            string acumulado = dtcuenta.Rows[ndtCuenta - 1]["TotalporVencer"].ToString().Replace(",", "");
                            acumulado = acumulado.Replace("$", "");
                            double acumuladoNumero = double.Parse(acumulado);
                            acumuladoNumero += vencer;
                            //Total cliente
                            string acumuladoTotal = dtcuenta.Rows[ndtCuenta - 1]["Total"].ToString().Replace(",", ""); ;
                            acumuladoTotal = acumuladoTotal.Replace("$", "");
                            double totalClientePro = double.Parse(acumuladoTotal);
                            totalClientePro += vencer;

                            dtcuenta.Rows[ndtCuenta - 1]["TotalporVencer"] = "$" + acumuladoNumero.ToString("#,##0.00");
                            dtcuenta.Rows[ndtCuenta - 1]["Total"] = "$" + totalClientePro.ToString("#,##0.00");
                        }

                        //Vencidos
                        if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["CFECHAVENCIMIENTO"].ToString())) {
                            string acumulado = dtcuenta.Rows[ndtCuenta - 1]["TotalVencido"].ToString().Replace(",", "");
                            acumulado = acumulado.Replace("$", "");
                            double acumuladoNumero = double.Parse(acumulado);
                            acumuladoNumero += vencido;

                            //Total cliente
                            string acumuladoTotal = dtcuenta.Rows[ndtCuenta - 1]["Total"].ToString().Replace(",", ""); ;
                            acumuladoTotal = acumuladoTotal.Replace("$", "");
                            double totalClientePro = double.Parse(acumuladoTotal);
                            totalClientePro += vencido;

                            dtcuenta.Rows[ndtCuenta - 1]["TotalVencido"] = "$" + acumuladoNumero.ToString("#,##0.00");
                            dtcuenta.Rows[ndtCuenta - 1]["Total"] = "$" + totalClientePro.ToString("#,##0.00");
                        }
                    }
                    else {
                        //Agregamos la fila a la tabla del reporte
                        rw["Sucursal"] = Nombresucursal;
                        dtcuenta.Rows.Add(rw);
                        ndtCuenta++;
                        pivoteCliente = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();
                    }


                    //lvCliente.DataSource = dtcuenta;
                    //lvCliente.DataBind();
                }
            }
            //DataRow rw4 = dtcuenta.NewRow();
            //rw4["TotalVencido"] = " $" + SumaVencido.ToString("#,##0.00") + "";
            //rw4["TotalporVencer"] = " $" + SumaporVencer.ToString("#,##0.00") + "";
            //rw4["Total"] = " $" + SumaTotal.ToString("#,##0.00") + "";
            //rw4["Agente"] = "TOTAL SUCURSAL";
            //dtcuenta.Rows.Add(rw4);
        }


    }
}

public class reporteVGC {
    public string codigo { get; set; }
    public string nomCliente { get; set; }
    public string nomAgente { get; set; }
    public string totVencido { get; set; }
    public string totAVencer { get; set; }
    public string total { get; set; }
    public string sucursal { get; set; }
}