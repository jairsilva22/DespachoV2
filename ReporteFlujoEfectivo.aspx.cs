using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class ReporteFlujoEfectivo : System.Web.UI.Page {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        double[,] totales = new double[5,4];
        double[,] totalesEfec = new double[5,4];

        protected void Page_Load(object sender, EventArgs e) {
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

            consulta();
        }

        public void consulta() {
            string cadena;

            DataTable dtcuenta = new DataTable();

            //Columnas del reporte según la plantilla (mismas que en AG Grid)
            dtcuenta.Columns.Add("Concepto");
            //Sucursal Irapuato
            dtcuenta.Columns.Add("ConcretoI");
            dtcuenta.Columns.Add("BlockI");
            dtcuenta.Columns.Add("TotalI");
            //Sucursal Saltillo
            dtcuenta.Columns.Add("ConcretoS");
            dtcuenta.Columns.Add("BlockS");
            dtcuenta.Columns.Add("TotalS");
            //Empresa
            dtcuenta.Columns.Add("ConcretoE");
            dtcuenta.Columns.Add("BlockE");
            dtcuenta.Columns.Add("GrupoE");

            //Agregar las filas del reporte
            DataRow row = dtcuenta.NewRow();
            DataRow row1 = dtcuenta.NewRow();
            DataRow row2 = dtcuenta.NewRow();
            DataRow row3 = dtcuenta.NewRow();
            DataRow row4 = dtcuenta.NewRow();

            row[0] ="Saldo Inicial Efectivo";
            dtcuenta.Rows.Add(row);
            row1[0] = "Ingresos Efectivo (+)";
            dtcuenta.Rows.Add(row1);
            row2[0] = "Traspasos Concreto <> Block";
            dtcuenta.Rows.Add(row2);
            row3[0] = "Egresos Efectivo (-)";
            dtcuenta.Rows.Add(row3);
            row4[0] = "Saldo Final Efectivo";
            dtcuenta.Rows.Add(row4);

            //Filas de Bancos
            DataRow rowB = dtcuenta.NewRow();
            DataRow rowB1 = dtcuenta.NewRow();
            DataRow rowB2 = dtcuenta.NewRow();
            DataRow rowB3 = dtcuenta.NewRow();
            DataRow rowB4 = dtcuenta.NewRow();

            rowB[0] = "Saldo Inicial Bancos";
            dtcuenta.Rows.Add(rowB);
            rowB1[0] = "Ingresos Bancos (+)";
            dtcuenta.Rows.Add(rowB1);
            rowB2[0] = "Traspasos Concreto <> Block";
            dtcuenta.Rows.Add(rowB2);
            rowB3[0] = "Egresos Bancos (-)";
            dtcuenta.Rows.Add(rowB3);
            rowB4[0] = "Saldo Final Bancos";
            dtcuenta.Rows.Add(rowB4);

            //Filas Bancos + Efectivo
            DataRow rowBE = dtcuenta.NewRow();
            DataRow rowBE1 = dtcuenta.NewRow();
            DataRow rowBE2 = dtcuenta.NewRow();
            DataRow rowBE3 = dtcuenta.NewRow();
            DataRow rowBE4 = dtcuenta.NewRow();

            rowBE[0] = "Saldo Inicial Efvo y Bancos";
            dtcuenta.Rows.Add(rowBE);
            rowBE1[0] = "Ingresos Efvo y Bancos (+)";
            dtcuenta.Rows.Add(rowBE1);
            rowBE2[0] = "Traspasos Concreto <> Block";
            dtcuenta.Rows.Add(rowBE2);
            rowBE3[0] = "Egresos Efvo y Bancos (-)";
            dtcuenta.Rows.Add(rowBE3);
            rowBE4[0] = "Saldo Final Efvo y Bancos";
            dtcuenta.Rows.Add(rowBE4);

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);

            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            if (perfil != "1" && perfil != "1007") {
                
            }
            else {
                DataTable dtConexiones = cContpaq.obtenerConexionesContabilidad();
                foreach (DataRow row10 in dtConexiones.Rows) {
                    cadena = row10[5].ToString();
                    int intSucursal = int.Parse(row10[0].ToString());
                    //Bancos
                    DataTable dtCTA = cContpaq.obtenerFlujoBancos(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                    llenarDataTable(dtCTA, dtcuenta, intSucursal, new int[] { 5, 6, 7, 8, 9 }, false);
                    //Efectivo
                    dtCTA = cContpaq.obtenerFlujoBancos(cadena, Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
                    llenarDataTable(dtCTA, dtcuenta, intSucursal, new int[] { 0, 1, 2, 3, 4 }, true);
                    //Total Efectivo + Bancos
                }

                for (int i = 1; i < dtcuenta.Columns.Count; i++) {
                    //Saldo Inicial
                    //dtcuenta.Rows[10][i] = (double.Parse(dtcuenta.Rows[0][i].ToString().Replace(",", "")) + double.Parse(dtcuenta.Rows[5][i].ToString().Replace(",", ""))).ToString("#,##0.00");
                    //Ingresos
                    //dtcuenta.Rows[11][i] = (double.Parse(dtcuenta.Rows[1][i].ToString().Replace(",", "")) + double.Parse(dtcuenta.Rows[6][i].ToString().Replace(",", ""))).ToString("#,##0.00");
                    //Traspasos
                    //dtcuenta.Rows[12][i] = (double.Parse(dtcuenta.Rows[2][i].ToString().Replace(",", "")) + double.Parse(dtcuenta.Rows[7][i].ToString().Replace(",", ""))).ToString("#,##0.00");
                    //Egresos
                    //dtcuenta.Rows[13][i] = (double.Parse(dtcuenta.Rows[3][i].ToString().Replace(",", "")) + double.Parse(dtcuenta.Rows[8][i].ToString().Replace(",", ""))).ToString("#,##0.00");
                    //Saldo Final
                    //dtcuenta.Rows[14][i] = (double.Parse(dtcuenta.Rows[4][i].ToString().Replace(",", "")) + double.Parse(dtcuenta.Rows[9][i].ToString().Replace(",", ""))).ToString("#,##0.00");
                }
            }
            llenarAgGrid(dtcuenta);
        }

        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta, int intsucursal, int[] posiciones, bool esEfectivo) {
            if (dtCTA == null) {
                labelError.Text = "No se pudo conectar a la base de datos";
            }
            else {
                for (int i = 0; i < (dtCTA.Rows.Count); i++) {
                    double ingresos, egresos, saldoinicial, traspasos;
                    ingresos = double.Parse(dtCTA.Rows[i]["Ingresos"].ToString()); //Ingresos
                    egresos = double.Parse(dtCTA.Rows[i]["Egresos"].ToString()); //Egresos
                    saldoinicial = double.Parse(dtCTA.Rows[i]["IngresosInicial"].ToString()) - double.Parse(dtCTA.Rows[i]["EgresosInicial"].ToString()); //Saldo Inicial
                    traspasos = 0; //traspasos
                    switch (intsucursal) {
                        case 11:
                            //Block Pepi Irapuato
                            if (esEfectivo) {
                                ingresos = 0; egresos = 0; saldoinicial = 0;
                                totalesEfec[0, 1] = ingresos;
                                totalesEfec[1, 1] = egresos;
                                totalesEfec[2, 1] = traspasos;
                                totalesEfec[3, 1] = saldoinicial;
                                totalesEfec[4, 1] = (saldoinicial + ingresos) - egresos;
                            }
                            else {
                                totales[0, 1] = ingresos;
                                totales[1, 1] = egresos;
                                totales[2, 1] = traspasos;
                                totales[3, 1] = saldoinicial;
                                totales[4, 1] = (saldoinicial + ingresos) - egresos;
                            }
                            
                            dtcuenta.Rows[posiciones[0]]["BlockI"] = saldoinicial.ToString("#,##0.00"); //saldo inicial
                            dtcuenta.Rows[posiciones[1]]["BlockI"] = ingresos.ToString("#,##0.00"); //Ingresos
                            dtcuenta.Rows[posiciones[2]]["BlockI"] = traspasos.ToString("#,##0.00"); //Traspasos
                            dtcuenta.Rows[posiciones[3]]["BlockI"] = egresos.ToString("#,##0.00"); //Egresos
                            dtcuenta.Rows[posiciones[4]]["BlockI"] = (saldoinicial + ingresos - egresos).ToString("#,##0.00");  //Saldo Final
                           
                            break;
                        case 12:
                            //Block Pepi Saltillo
                            if (esEfectivo) {
                                ingresos = 0; egresos = 0; saldoinicial = 0;
                                totalesEfec[0, 3] = ingresos;
                                totalesEfec[1, 3] = egresos;
                                totalesEfec[2, 3] = traspasos;
                                totalesEfec[3, 3] = saldoinicial;
                                totalesEfec[4, 3] = (saldoinicial + ingresos) - egresos;
                            }
                            else {
                                totales[0, 3] = ingresos;
                                totales[1, 3] = egresos;
                                totales[2, 3] = traspasos;
                                totales[3, 3] = saldoinicial;
                                totales[4, 3] = (saldoinicial + ingresos) - egresos;
                            }
                            
                            dtcuenta.Rows[posiciones[0]]["BlockS"] = saldoinicial.ToString("#,##0.00"); //saldo inicial
                            dtcuenta.Rows[posiciones[1]]["BlockS"] = ingresos.ToString("#,##0.00"); //Ingresos
                            dtcuenta.Rows[posiciones[2]]["BlockS"] = traspasos.ToString("#,##0.00"); //Traspasos
                            dtcuenta.Rows[posiciones[3]]["BlockS"] = egresos.ToString("#,##0.00"); //Egresos
                            dtcuenta.Rows[posiciones[4]]["BlockS"] = (saldoinicial + ingresos - egresos).ToString("#,##0.00");  //Saldo Final
                            break;
                        case 13:
                            //Concretos Pepi Saltillo
                            if (esEfectivo) {
                                ingresos = 0; egresos = 0; saldoinicial = 0;
                                totalesEfec[0, 2] = ingresos;
                                totalesEfec[1, 2] = egresos;
                                totalesEfec[2, 2] = traspasos;
                                totalesEfec[3, 2] = saldoinicial;
                                totalesEfec[4, 2] = (saldoinicial + ingresos) - egresos;
                            }
                            else {
                                totales[0, 2] = ingresos;
                                totales[1, 2] = egresos;
                                totales[2, 2] = traspasos;
                                totales[3, 2] = saldoinicial;
                                totales[4, 2] = (saldoinicial + ingresos) - egresos;
                            }

                            dtcuenta.Rows[posiciones[0]]["ConcretoS"] = saldoinicial.ToString("#,##0.00"); //saldo inicial
                            dtcuenta.Rows[posiciones[1]]["ConcretoS"] = ingresos.ToString("#,##0.00"); //Ingresos
                            dtcuenta.Rows[posiciones[2]]["ConcretoS"] = traspasos.ToString("#,##0.00"); //Traspasos
                            dtcuenta.Rows[posiciones[3]]["ConcretoS"] = egresos.ToString("#,##0.00"); //Egresos
                            dtcuenta.Rows[posiciones[4]]["ConcretoS"] = (saldoinicial + ingresos - egresos).ToString("#,##0.00");  //Saldo Final
                            break;
                        case 14:
                            //Concretos Pepi Irapuato
                            if (esEfectivo) {
                                ingresos = 0; egresos = 0; saldoinicial = 0;
                                totalesEfec[0, 0] = ingresos;
                                totalesEfec[1, 0] = egresos;
                                totalesEfec[2, 0] = traspasos;
                                totalesEfec[3, 0] = saldoinicial;
                                totalesEfec[4, 0] = (saldoinicial + ingresos) - egresos;
                            }
                            else {
                                totales[0, 0] = ingresos;
                                totales[1, 0] = egresos;
                                totales[2, 0] = traspasos;
                                totales[3, 0] = saldoinicial;
                                totales[4, 0] = (saldoinicial + ingresos) - egresos;
                            }
                            
                            dtcuenta.Rows[posiciones[0]]["ConcretoI"] = saldoinicial.ToString("#,##0.00"); //saldo inicial
                            dtcuenta.Rows[posiciones[1]]["ConcretoI"] = ingresos.ToString("#,##0.00"); //Ingresos
                            dtcuenta.Rows[posiciones[2]]["ConcretoI"] = traspasos.ToString("#,##0.00"); //Traspasos
                            dtcuenta.Rows[posiciones[3]]["ConcretoI"] = egresos.ToString("#,##0.00"); //Egresos
                            dtcuenta.Rows[posiciones[4]]["ConcretoI"] = (saldoinicial + ingresos - egresos).ToString("#,##0.00");  //Saldo Final
                            break;
                        default:
                            break;
                    }
                    
                }

                //Totales sucursal Irapuato Efectivo
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i]["TotalI"] = (totalesEfec[i, 0] + totalesEfec[i, 1]).ToString("#,##0.00");
                }
                
                //totales sucursal Saltillo Efectivo
                for(int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i]["TotalS"] = (totalesEfec[i, 2] + totalesEfec[i, 3]).ToString("#,##0.00");
                }

                //Totales sucursal Saltillo Bancos
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i + 5]["TotalS"] = (totales[i, 2] + totales[i, 3]).ToString("#,##0.00");
                }

                //Totales sucursal Irapuato Bancos
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i + 5]["TotalI"] = (totales[i, 0] + totales[i, 1]).ToString("#,##0.00"); 
                }

                //Total Empresas
                //Concreto Efectivo
                for(int i = 0; i<5; i++) {
                    dtcuenta.Rows[i]["ConcretoE"] = (totalesEfec[i, 0] + totalesEfec[i, 2]).ToString("#,##0.00");
                }
                //Block Efectivo
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i]["BlockE"] = (totalesEfec[i, 1] + totalesEfec[i, 3]).ToString("#,##0.00");
                }
                //Grupo Efectivo
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i]["GrupoE"] = (totalesEfec[i, 1] + totalesEfec[i, 2] + totalesEfec[i, 3] + totalesEfec[i, 0]).ToString("#,##0.00");
                }
                //Concreto Bancos
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i + 5]["ConcretoE"] = (totales[i, 0] + totales[i, 2]).ToString("#,##0.00");
                }
                //Block Bancos
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i + 5]["BlockE"] = (totales[i, 1] + totales[i, 3]).ToString("#,##0.00");
                }
                //Grupo Bancos
                for (int i = 0; i < 5; i++) {
                    dtcuenta.Rows[i + 5]["GrupoE"] = (totales[i, 0] + totales[i, 2] + totales[i, 1] + totales[i, 3]).ToString("#,##0.00");
                }

            }
        }
        
        //Para el AG-Grid
        public void llenarAgGrid(DataTable dt) {
            var reporte = new List<reporteFlujoEfectivo>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporte.Add(new reporteFlujoEfectivo() { concepto = dt.Rows[i]["Concepto"].ToString(), 
                    concretoI = dt.Rows[i]["ConcretoI"].ToString(), blockI = dt.Rows[i]["BlockI"].ToString(), totalI = dt.Rows[i]["TotalI"].ToString(),
                    concretoS = dt.Rows[i]["ConcretoS"].ToString(), blockS = dt.Rows[i]["BlockS"].ToString(), totalS = dt.Rows[i]["TotalS"].ToString(),
                    concretoE = dt.Rows[i]["ConcretoE"].ToString(), blockE = dt.Rows[i]["BlockE"].ToString(), grupoE = dt.Rows[i]["GrupoE"].ToString()
                });
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporte);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
    }

    public class reporteFlujoEfectivo {
        public string concepto { get; set; }
        public string concretoI { get; set; }
        public string blockI { get; set; }
        public string totalI { get; set; }
        public string concretoS { get; set; }
        public string blockS { get; set; }
        public string totalS { get; set; }
        public string concretoE { get; set; }
        public string blockE { get; set; }
        public string grupoE { get; set; }
    }
}