using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class reporteVentasPorProductosDespacho : System.Web.UI.Page {
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

        protected void llenarLV() {
            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("CCODIGOPRODUCTO");
            dtcuenta.Columns.Add("CNOMBREPRODUCTO");
            dtcuenta.Columns.Add("CUnidades");
            dtcuenta.Columns.Add("CTIPOPRODUCTO");
            dtcuenta.Columns.Add("CNETO");
            dtcuenta.Columns.Add("CDESCUENTO1");
            dtcuenta.Columns.Add("Neto-Desc");
            dtcuenta.Columns.Add("CIMPUESTO1");
            dtcuenta.Columns.Add("CTOTAL");
            dtcuenta.Columns.Add("CPRECIOU");
            dtcuenta.Columns.Add("SUCURSAL");

            

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            //Obtenemos los cargos del cliente seleccionado
            DataTable dtCTA = cContpaq.obtenerVentasPorProductoDespacho(Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
            if (dtCTA == null) {
                labelError.Text = "NO SE ENCUENTRAN DATOS";

            }
            else {
                string pivote = dtCTA.Rows[0]["codigo"].ToString();
                float tTotal = 0;
                float tCantidad = 0;
                float tNeto = 0;
                float tDescuento = 0;
                float tNetoDesc = 0;
                float tImpuesto = 0;
                float tUnitario = 0;
                float tPorcentaje1 = 0;
                float tPorcentaje2 = 0;
                double descPorcentaje;

                for (int i = 0; i < (dtCTA.Rows.Count); i++) {
                    descPorcentaje = 0;
                    //Nombre de sucursal
                    string sucursal = "";
                    switch (int.Parse(dtCTA.Rows[i]["planta"].ToString())) {
                        case 1: sucursal = "Concretos Saltillo"; break;
                        case 2: sucursal = "Concretos Irapuato"; break;
                        case 3: sucursal = "Block Saltillo"; break;
                        case 4: sucursal = "Concretos Saltillo"; break;
                        case 1006: sucursal = "Block Irapuato"; break;
                    }


                    if (pivote == dtCTA.Rows[i]["codigo"].ToString()) {
                        //Agregamos la fila a la tabla del reporte
                        //dtcuenta.Rows.Add(rw);

                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["total"].ToString());
                        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["cantidad"].ToString());
                        //tUnitario = tUnitario + float.Parse(dtCTA.Rows[i]["precioF"].ToString());
                        //tNeto = tNeto + float.Parse(dtCTA.Rows[i]["subtotal"].ToString());

                        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["subtotal"].ToString());

                        //tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

                        if (i == dtCTA.Rows.Count - 1) {
                            tNeto = tCantidad * float.Parse(dtCTA.Rows[i - 1]["precioU"].ToString());
                            tDescuento = (float)(tNeto - tNetoDesc);
                            tImpuesto = (float)(tNetoDesc * 0.16);
                            tPorcentaje1 = tDescuento * 100;
                            tPorcentaje2 = tPorcentaje1 / tNeto;
                            DataRow rw2 = dtcuenta.NewRow();
                            rw2["CTOTAL"] = " $" + tTotal.ToString("#,##0.00") + "";
                            
                            rw2["CUnidades"] = " " + tCantidad + "";
                            rw2["CNETO"] = " $" + tNeto.ToString("#,##0.00") + "";
                            if (tPorcentaje2 > 0) {
                                //rw2["CDESCUENTO1"] = "<strong>" + tPorcentaje2.ToString() + "%</strong>";
                                //rw2["CDESCUENTO1"] = "<strong> " + Decimal.Round((decimal)tPorcentaje2) + " %</strong>";
                                rw2["CDESCUENTO1"] = " " + Math.Round(tPorcentaje2, 2) + " %";
                                
                            }
                            else {
                                rw2["CDESCUENTO1"] = "0%";
                            }
                            rw2["Neto-Desc"] = " $" + tNetoDesc.ToString("#,##0.00") + "";
                            rw2["CIMPUESTO1"] = "$ " + tImpuesto.ToString() + "";
                            rw2["CCODIGOPRODUCTO"] = dtCTA.Rows[i]["codigo"].ToString();
                            rw2["CNOMBREPRODUCTO"] = dtCTA.Rows[i]["descripcion"].ToString();
                            rw2["CTIPOPRODUCTO"] = dtCTA.Rows[i]["planta"].ToString();

                            if (dtCTA.Rows[i]["planta"].ToString() == "2") {
                                rw2["CTIPOPRODUCTO"] = "M3";
                            }
                            else {
                                rw2["CTIPOPRODUCTO"] = "PZA";
                            }
                            rw2["CPRECIOU"] = float.Parse(dtCTA.Rows[i]["subtotal"].ToString()) / tCantidad;
                            
                            //regla de 3: 100 - ((precioPromedio * 100) / precioU)
                            descPorcentaje = 100 - ((double.Parse(rw2["CPRECIOU"].ToString()) * 100) / double.Parse(dtCTA.Rows[i]["precioU"].ToString()));
                            rw2["CDESCUENTO1"] = descPorcentaje.ToString("0.00") + "%";
                            if (dtCTA.Rows[i]["reqFac"].ToString() == "SI")
                                sucursal += " Facturable";
                            else
                                sucursal += " Ventas General";

                            rw2["SUCURSAL"] = sucursal;
                            
                            dtcuenta.Rows.Add(rw2);
                        }
                    }
                    else {
                        tNeto = tCantidad * float.Parse(dtCTA.Rows[i - 1]["precioU"].ToString());
                        tDescuento = (float)(tNeto - tNetoDesc);
                        tImpuesto = (float)(tNetoDesc * 0.16);
                        tPorcentaje1 = tDescuento * 100;
                        tPorcentaje2 = tPorcentaje1 / tNeto;


                        DataRow rw2 = dtcuenta.NewRow();
                        rw2["CTOTAL"] = " $" + tTotal.ToString("#,##0.00") + "";
                        
                        rw2["CUnidades"] = " " + tCantidad + "";
                        rw2["CNETO"] = " $" + tNeto.ToString("#,##0.00") + "";
                        if (tPorcentaje2 > 0) {
                            //rw2["CDESCUENTO1"] = "<strong>" + tPorcentaje2.ToString() + "%</strong>";
                            rw2["CDESCUENTO1"] = " " + Math.Round(tPorcentaje2, 2) + " %";
                            
                        }
                        else {
                            rw2["CDESCUENTO1"] = "0%";
                        }
                        rw2["Neto-Desc"] = " $" + tNetoDesc.ToString("#,##0.00") + "";
                        rw2["CIMPUESTO1"] = " $" + tImpuesto.ToString("#,##0.00") + "";
                        rw2["CCODIGOPRODUCTO"] = dtCTA.Rows[i - 1]["codigo"].ToString();
                        rw2["CNOMBREPRODUCTO"] = dtCTA.Rows[i - 1]["descripcion"].ToString();
                        rw2["CTIPOPRODUCTO"] = dtCTA.Rows[i]["planta"].ToString();

                        if (dtCTA.Rows[i]["planta"].ToString() == "2") {
                            rw2["CTIPOPRODUCTO"] = "M3";
                        }
                        else {
                            rw2["CTIPOPRODUCTO"] = "PZA";
                        }
                        rw2["CPRECIOU"] = float.Parse(dtCTA.Rows[i]["subtotal"].ToString()) / tCantidad;
                        //regla de 3: 100 - ((precioPromedio * 100) / precioU)
                        descPorcentaje = 100 - ((double.Parse(rw2["CPRECIOU"].ToString()) * 100) / double.Parse(dtCTA.Rows[i]["precioU"].ToString()));
                        rw2["CDESCUENTO1"] = descPorcentaje.ToString("0.00") + "%";
                        if (dtCTA.Rows[i]["reqFac"].ToString() == "SI")
                            sucursal += " Facturable";
                        else
                            sucursal += " Ventas General";

                        rw2["SUCURSAL"] = sucursal;
                        
                        dtcuenta.Rows.Add(rw2);

                        //Agregamos la fila a la tabla del reporte
                        //dtcuenta.Rows.Add(rw);
                        //
                        tTotal = 0;
                        tCantidad = 0;
                        tUnitario = 0;
                        tNeto = 0;
                        tDescuento = 0;
                        tNetoDesc = 0;
                        tImpuesto = 0;
                        tPorcentaje1 = 0;
                        tPorcentaje2 = 0;
                        pivote = dtCTA.Rows[i]["codigo"].ToString();

                        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["total"].ToString());
                        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["cantidad"].ToString());
                        //tUnitario = tUnitario + float.Parse(dtCTA.Rows[i]["precioF"].ToString());

                        //tNeto = tNeto + float.Parse(dtCTA.Rows[i]["subtotal"].ToString());

                        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["subtotal"].ToString());


                        if (i == dtCTA.Rows.Count - 1) {
                            tNeto = tCantidad * float.Parse(dtCTA.Rows[i]["precioU"].ToString());
                            tImpuesto = (float)(tNetoDesc * 0.16);
                            tDescuento = (float)(tNeto - tNetoDesc);
                            tPorcentaje1 = tDescuento * 100;
                            tPorcentaje2 = tPorcentaje1 / tNeto;
                            DataRow rw3 = dtcuenta.NewRow();
                            rw3["CTOTAL"] = " $" + tTotal.ToString("#,##0.00") + "";
                            
                            rw3["CUnidades"] = " " + tCantidad + "";
                            rw3["CNETO"] = " $" + tNeto.ToString("#,##0.00") + "";
                            if (tPorcentaje2 > 0) {
                                //rw3["CDESCUENTO1"] = "<strong>" + tPorcentaje2.ToString() + "%</strong>";
                                //rw3["CDESCUENTO1"] = "<strong> " + Decimal.Round((decimal)tPorcentaje2) + " %</strong>";
                                rw3["CDESCUENTO1"] = " " + Math.Round(tPorcentaje2, 2) + " %";
                                
                            }
                            else {
                                rw3["CDESCUENTO1"] = "0%";
                            }
                            rw3["Neto-Desc"] = " $" + tNetoDesc.ToString("#,##0.00") + "";
                            rw3["CIMPUESTO1"] = " $" + tImpuesto.ToString("#,##0.00") + "";
                            rw3["CCODIGOPRODUCTO"] = dtCTA.Rows[i]["codigo"].ToString();
                            rw3["CNOMBREPRODUCTO"] = dtCTA.Rows[i]["descripcion"].ToString();
                            rw3["CTIPOPRODUCTO"] = dtCTA.Rows[i]["planta"].ToString();

                            if (dtCTA.Rows[i]["planta"].ToString() == "2") {
                                rw3["CTIPOPRODUCTO"] = "M3";
                            }
                            else {
                                rw3["CTIPOPRODUCTO"] = "PZA";
                            }
                            rw3["CPRECIOU"] = float.Parse(dtCTA.Rows[i]["subtotal"].ToString()) / tCantidad;
                            //regla de 3: 100 - ((precioPromedio * 100) / precioU)
                            descPorcentaje = 100 - ((double.Parse(rw2["CPRECIOU"].ToString()) * 100) / double.Parse(dtCTA.Rows[i]["precioU"].ToString()));
                            rw2["CDESCUENTO1"] = descPorcentaje.ToString("0.00") + "%";
                            //if (dtCTA.Rows[i]["reqFac"].ToString() == "SI")
                            //    sucursal += "Facturable";
                            //else
                            //    sucursal += "Ventas General";

                            rw3["SUCURSAL"] = sucursal;
                            
                            dtcuenta.Rows.Add(rw3);
                        }

                    }
                }
                lvCliente.DataSource = dtcuenta;
                lvCliente.DataBind();

                //Llenar AG-GRID
                llenarAgGrid(dtcuenta);
            }

            
        }

        public void llenarAgGrid(DataTable dt) {
            var reporteVpP = new List<reporteVpP>();
            //Variables para los totales
            double totalDescuento = 0, total = 0;
            int cuenta = 0;

            for (int i = 0; i < dt.Rows.Count; i++) {
                reporteVpP.Add(new reporteVpP() { sucursal = dt.Rows[i]["SUCURSAL"].ToString(), codigo = dt.Rows[i]["CCODIGOPRODUCTO"].ToString(), nombre = dt.Rows[i]["CNOMBREPRODUCTO"].ToString(), cantidad = dt.Rows[i]["CUnidades"].ToString(), precioPromedio = dt.Rows[i]["CPRECIOU"].ToString(), descuento = dt.Rows[i]["CDESCUENTO1"].ToString(), total = dt.Rows[i]["CTOTAL"].ToString() });
                cuenta++;
                total += double.Parse(dt.Rows[i]["CTOTAL"].ToString().Replace("$", "").Replace(",", ""));
                totalDescuento += double.Parse(dt.Rows[i]["CDESCUENTO1"].ToString().Replace("%", "").Replace(",", ""));
            }
            
            //Colocar totales
            lblTotal.Text = "$" + total.ToString("#,##0.00");
            lblDescuentos.Text = (totalDescuento / cuenta).ToString("0.00") + "%";

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(reporteVpP);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }
    }
}