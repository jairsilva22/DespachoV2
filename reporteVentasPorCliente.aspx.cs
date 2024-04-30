using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace despacho
{
    public partial class reporteVentasPorCliente : System.Web.UI.Page
    {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();

        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable dtDatosCliente = cContpaq.obtenerDatosCliente(int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]));
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

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + cSuc.nombre;

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

        protected void llenarLV()
        {
            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            //dtcuenta.Columns.Add("CIDDOCUMENTO");
            dtcuenta.Columns.Add("CCODIGOCLIENTE");
            dtcuenta.Columns.Add("CRAZONSOCIAL");
            //dtcuenta.Columns.Add("CIDPRODUCTO");
            dtcuenta.Columns.Add("CCODIGOPRODUCTO");
            dtcuenta.Columns.Add("CNOMBREPRODUCTO");
            dtcuenta.Columns.Add("CUnidades");
            dtcuenta.Columns.Add("CNETO");
            dtcuenta.Columns.Add("CDESCUENTO1");
            dtcuenta.Columns.Add("Neto-Desc");
            dtcuenta.Columns.Add("CIMPUESTO1");
            dtcuenta.Columns.Add("CTOTAL");


            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            //Obtenemos los cargos del cliente seleccionado
            DataTable dtCTA = cContpaq.obtenerVentasPorCliente( Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
            string pivote = dtCTA.Rows[0]["CCODIGOCLIENTE"].ToString();
            float tTotal = 0;
            float tCantidad = 0;
            float tNeto = 0; 
            float tDescuento = 0;
            float tNetoDesc = 0;
            float tImpuesto = 0;

            for (int i = 0; i < (dtCTA.Rows.Count); i++)
            {
                //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                DataRow rw = dtcuenta.NewRow();
                
                //rw["CIDDOCUMENTO"] = dtCTA.Rows[i]["CIDDOCUMENTO"].ToString();
                rw["CCODIGOCLIENTE"] = dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString();
                rw["CRAZONSOCIAL"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                //rw["CIDPRODUCTO"] = dtCTA.Rows[i]["CIDPRODUCTO"].ToString();
                rw["CCODIGOPRODUCTO"] = dtCTA.Rows[i]["CCODIGOPRODUCTO"].ToString();
                rw["CNOMBREPRODUCTO"] = dtCTA.Rows[i]["CNOMBREPRODUCTO"].ToString();
                rw["CUnidades"] = dtCTA.Rows[i]["CUnidades"].ToString();
                float rwNETO = float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                rw["CNETO"] = "$" + rwNETO.ToString("#,##0.00");
                float rwCDESCUENTO1 = float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                rw["CDESCUENTO1"] = "$" + rwCDESCUENTO1.ToString("#,##0.00");
                float rwNETODESC = float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                rw["Neto-Desc"] = "$" + rwNETODESC.ToString("#,##0.00");
                float rwCIMPUESTO1 = float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
                rw["CIMPUESTO1"] = "$" + rwCIMPUESTO1.ToString("#,##0.00");
                float rwCTOTAL = float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                rw["CTOTAL"] = "$" + rwCTOTAL.ToString("#,##0.00");


                 

                if (pivote == dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString())
                {
                    //Agregamos la fila a la tabla del reporte
                    dtcuenta.Rows.Add(rw);
                    //
                    tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                    tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
                    tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                    tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                    tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                    tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

                    if (i == dtCTA.Rows.Count -1 )
                    {

                        DataRow rw2 = dtcuenta.NewRow();
                        rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong> $";
                        rw2["CUnidades"] = "<strong>" + tCantidad + "</strong>";
                        rw2["CNETO"] = "<strong> $"+ tNeto.ToString("#,##0.00") + "</strong> $";
                        rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong> $";
                        rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong> $";
                        rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong> $";
                        rw2["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";
                        dtcuenta.Rows.Add(rw2);
                    }

                }
                else
                {
                    DataRow rw2 = dtcuenta.NewRow();
                    rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                    rw2["CUnidades"] = "<strong> " + tCantidad + "</strong>";
                    rw2["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                    rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
                    rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
                    rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                    rw2["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";

                    dtcuenta.Rows.Add(rw2);

                    //Agregamos la fila a la tabla del reporte
                    dtcuenta.Rows.Add(rw);
                    //
                    tTotal = 0;
                    tCantidad = 0;
                    tNeto = 0;
                    tDescuento = 0;
                    tNetoDesc = 0;
                    tImpuesto = 0;
                    pivote = dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString();

                    tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                    tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
                    tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
                    tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
                    tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
                    tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

                    if (i == dtCTA.Rows.Count - 1)
                    {

                        DataRow rw3 = dtcuenta.NewRow();
                        rw3["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
                        rw3["CUnidades"] = "<strong>"+ tCantidad + "</strong>";
                        rw3["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
                        rw3["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
                        rw3["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
                        rw3["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
                        rw3["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";

                        dtcuenta.Rows.Add(rw3);
                    }

                }
            }



            lvCliente.DataSource = dtcuenta;
            lvCliente.DataBind();
        }
    }
}