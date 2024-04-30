using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class reporteGenralDetallado : System.Web.UI.Page
    {
        int idSucursal = 0;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblFechaInicio.Text = "Fecha de Corte: " + DateTime.Parse(Request.QueryString["FechaInicio"]).ToString("dd/MM/yyyy");

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
                lblReporte.Text = cSuc.nombre + " Reporte de Ventas en General Detallado";

                llenarLV();

            }
            
            
        }

        private void llenarLV()
        {
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("Codigo");
            dtcuenta.Columns.Add("Cliente");
            dtcuenta.Columns.Add("Agente");
            dtcuenta.Columns.Add("Concepto");
            dtcuenta.Columns.Add("Folio");
            dtcuenta.Columns.Add("Fecha");
            dtcuenta.Columns.Add("Total Vencido");
            dtcuenta.Columns.Add("Total por Vencer");
            dtcuenta.Columns.Add("Total");

            //buscamos todos los clientes 
            DataTable cte = cContpaq.obtenerClienteVtaGrl();
            float totalCargos = 0;
            float totalAbonos = 0;
            if(cte.Rows.Count > 0)
            {
                for(int i = 0; i < cte.Rows.Count; i++)
                {
                   
                    // dtcuenta.Rows.Add(rw);
                    //buscamos los cargos por cliente 
                    int idCliente = int.Parse(cte.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString());
                    DataTable cargosCte = cContpaq.obtenerCargosClienteVtsGrl(idCliente, Request.QueryString["FechaInicio"]);
                    for(int j = 0; j < cargosCte.Rows.Count; j++)
                    {

                        DataRow rw = dtcuenta.NewRow();
                        rw["Codigo"] = cte.Rows[i]["CCODIGOCLIENTE"].ToString();
                        rw["Cliente"] = cte.Rows[i]["CRAZONSOCIAL"].ToString();
                        int idAgente = int.Parse(cargosCte.Rows[j]["CIDAGENTE"].ToString());
                        //bsucamos el nombre del agente 
                        rw["Agente"] = cContpaq.obtenerVendedor(idAgente);
                        //mostramos los cargos
                        //buscamos el concepto del documento 
                        String conceptoCargo = cContpaq.obtenerConceptoPorId(int.Parse(cargosCte.Rows[j]["CIDCONCEPTODOCUMENTO"].ToString()));
                        rw["Concepto"] = conceptoCargo;
                        rw["Folio"] = cargosCte.Rows[j]["CFOLIO"].ToString();
                        rw["Fecha"] = DateTime.Parse(cargosCte.Rows[j]["CFECHA"].ToString()).ToString("dd-MMM-yyyy"); //CTOTAL
                        float cargo = 0;
                        if (cargosCte.Rows[j]["CTOTAL"].ToString() != "" && cargosCte.Rows[j]["CTOTAL"].ToString() != null)
                        {
                            totalCargos = totalCargos + float.Parse(cargosCte.Rows[j]["CTOTAL"].ToString());
                        }
                        rw["Total Vencido"] = float.Parse(cargosCte.Rows[j]["CTOTAL"].ToString()).ToString("#,##0.00");
                        rw["Total por Vencer"] = "0.00";
                        rw["Total"] = float.Parse(cargosCte.Rows[j]["CTOTAL"].ToString()).ToString("#,##0.00");
                        dtcuenta.Rows.Add(rw);

                        //buscamos los abonos por cargo
                        DataTable dtAbono = cContpaq.obtenerAbonoPorIdCargo(int.Parse(cargosCte.Rows[j]["CIDDOCUMENTO"].ToString()));
                        if(dtAbono.Rows.Count > 0)
                        {
                            for (int x = 0; x < dtAbono.Rows.Count; x++)
                            {
                                DataTable dtAbonoDet = cContpaq.obtenerAbonoPorId(int.Parse(dtAbono.Rows[x]["CIDDOCUMENTOABONO"].ToString()));

                                for (int y = 0; y < dtAbonoDet.Rows.Count; y++)
                                {
                                    DataRow rw2 = dtcuenta.NewRow();
                                    rw2["Codigo"] = cte.Rows[i]["CCODIGOCLIENTE"].ToString();
                                    rw2["Cliente"] = cte.Rows[i]["CRAZONSOCIAL"].ToString();
                                    int idAgente2 = int.Parse(dtAbonoDet.Rows[y]["CIDAGENTE"].ToString());
                                    //bsucamos el nombre del agente 
                                    rw2["Agente"] = cContpaq.obtenerVendedor(idAgente2);
                                    String conceptoCargo2 = cContpaq.obtenerConceptoPorId(int.Parse(dtAbonoDet.Rows[y]["CIDCONCEPTODOCUMENTO"].ToString()));
                                    rw2["Concepto"] = conceptoCargo2;
                                    rw2["Folio"] = dtAbonoDet.Rows[y]["CFOLIO"].ToString();
                                    rw2["Fecha"] = DateTime.Parse(dtAbonoDet.Rows[y]["CFECHA"].ToString()).ToString("dd-MMM-yyyy"); //CTOTAL
                                    float cargo2 = 0;
                                    if (cargosCte.Rows[y]["CTOTAL"].ToString() != "" && dtAbonoDet.Rows[y]["CTOTAL"].ToString() != null)
                                    {
                                        totalAbonos = totalAbonos + float.Parse(dtAbonoDet.Rows[y]["CTOTAL"].ToString());
                                    }
                                    rw2["Total Vencido"] = "-" + float.Parse(dtAbonoDet.Rows[y]["CTOTAL"].ToString()).ToString("#,##0.00");
                                    rw2["Total por Vencer"] = "0.00";
                                    rw2["Total"] = "-" + float.Parse(dtAbonoDet.Rows[y]["CTOTAL"].ToString()).ToString("#,##0.00");
                                    dtcuenta.Rows.Add(rw2);
                                }
                            }
                        }
                        float total = totalCargos - totalAbonos;
                        DataRow data = dtcuenta.NewRow();
                        data["Codigo"] = "<strong>" + cte.Rows[i]["CCODIGOCLIENTE"].ToString() + "</strong>";
                        data["Cliente"] = "<strong>" + cte.Rows[i]["CRAZONSOCIAL"].ToString() + "</strong>";
                        data["Agente"] = "&nbsp;";
                        data["Concepto"] = "&nbsp;";
                        data["Folio"] = "&nbsp;";
                        data["Fecha"] = "&nbsp;";
                        data["Total Vencido"] = "<strong>" + total.ToString("#,##0.00") + "</strong>";
                        data["Total por Vencer"] = "<strong>0.00 </strong>";
                        data["Total"] = "<strong>" + total.ToString("#,##0.00") + "<strong>";
                        dtcuenta.Rows.Add(data);

                        DataRow data2 = dtcuenta.NewRow();
                        data2["Codigo"] = "&nbsp;";
                        data2["Cliente"] = "&nbsp;";
                        data2["Agente"] = "&nbsp;";
                        data2["Concepto"] = "&nbsp;";
                        data2["Folio"] = "&nbsp;";
                        data2["Fecha"] = "&nbsp;";
                        data2["Total Vencido"] = "&nbsp;";
                        data2["Total por Vencer"] = "&nbsp;";
                        data2["Total"] = "&nbsp;";
                        dtcuenta.Rows.Add(data2);

                        totalAbonos = 0;
                        totalCargos = 0;

                    }

                }
                
            }
            
            lvCliente.DataSource = dtcuenta;
            lvCliente.DataBind();
        }
    }
}