using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ReporteCuentasPorPagarAProvedores : System.Web.UI.Page
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
            dtcuenta.Columns.Add("CIDCLIENTEPROVEEDOR");
            dtcuenta.Columns.Add("CFECHA");
            dtcuenta.Columns.Add("CFOLIO");
            dtcuenta.Columns.Add("CRAZONSOCIAL");
            dtcuenta.Columns.Add("CDENCOMERCIAL");
            dtcuenta.Columns.Add("CNETO");
            dtcuenta.Columns.Add("CIMPUESTO1");
            dtcuenta.Columns.Add("isr");
            dtcuenta.Columns.Add("CTOTAL");
            dtcuenta.Columns.Add("CCLAVESAT");
            dtcuenta.Columns.Add("CFECHAULTIMOINTERES");
            dtcuenta.Columns.Add("CNOMBRECUENTA");



            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            ////Obtenemos los cargos del cliente seleccionado
            DataTable dtCTA = cContpaq.obtenerCuentasPorPagarAProveedores(Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);

            if (dtCTA == null)
            {
                labelError.Text = "NO SE ENCUENTRAN DATOS";

            }
            else
            {

                string pivote = dtCTA.Rows[0]["CIDCLIENTEPROVEEDOR"].ToString();
                float total = 0;
                float iva = 0;
                float subtotal = 0;


                for (int i = 0; i < dtCTA.Rows.Count; i++)
                {
                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
                    DataRow rw = dtcuenta.NewRow();

                    rw["CIDCLIENTEPROVEEDOR"] = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();

                    DateTime fechita = DateTime.Parse(dtCTA.Rows[i]["CFECHA"].ToString());
                    rw["CFECHA"] = fechita.ToString("dd-MM-yyyy");

                    //rw["CFECHA"] = dtCTA.Rows[i]["CFECHA"].ToString();
                    rw["CFOLIO"] = dtCTA.Rows[i]["CFOLIO"].ToString();
                    rw["CRAZONSOCIAL"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                    rw["CDENCOMERCIAL"] = dtCTA.Rows[i]["CDENCOMERCIAL"].ToString();
                    rw["CNETO"] = dtCTA.Rows[i]["CNETO"].ToString();
                    rw["CIMPUESTO1"] = dtCTA.Rows[i]["CIMPUESTO1"].ToString();
                    //rw["isr"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
                    rw["CTOTAL"] = dtCTA.Rows[i]["CTOTAL"].ToString();
                    rw["CCLAVESAT"] = dtCTA.Rows[i]["CCLAVESAT"].ToString();

                    //DateTime fechitaf = DateTime.Parse(dtCTA.Rows[i]["CFECHAULTIMOINTERES"].ToString());
                    //rw["CFECHAULTIMOINTERES"] = fechitaf.ToString("dd-MM-yyyy");

                    rw["CNOMBRECUENTA"] = dtCTA.Rows[i]["CNOMBRECUENTA"].ToString();


                    if (pivote == dtCTA.Rows[0]["CIDCLIENTEPROVEEDOR"].ToString())
                    {
                        dtcuenta.Rows.Add(rw);
                        total = total + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                        iva = iva + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
                        subtotal = subtotal + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());


                        if (i == dtCTA.Rows.Count - 1)
                        {

                            DataRow rw2 = dtcuenta.NewRow();
                            rw2["CTOTAL"] = "<strong> $" + total.ToString("#,##0.00") + "</strong> $";
                            rw2["CIMPUESTO1"] = "<strong>" + iva.ToString("#,##0.00") + "</strong> $";
                            rw2["CNETO"] = "<strong> $" + subtotal.ToString("#,##0.00") + "</strong> $";

                            rw2["CDENCOMERCIAL"] = "<strong>TOTAL</strong>";
                            dtcuenta.Rows.Add(rw2);
                        }

                    }
                    else
                    {
                        DataRow rw2 = dtcuenta.NewRow();
                        rw2["CTOTAL"] = "<strong> $" + total.ToString("#,##0.00") + "</strong>";
                        rw2["CIMPUESTO1"] = "<strong>" + iva.ToString("#,##0.00") + "</strong> $";
                        rw2["CNETO"] = "<strong> $" + subtotal.ToString("#,##0.00") + "</strong>";

                        rw2["CDENCOMERCIAL"] = "<strong>TOTAL</strong>";

                        dtcuenta.Rows.Add(rw2);

                        //Agregamos la fila a la tabla del reporte
                        dtcuenta.Rows.Add(rw);
                        total = 0;
                        iva = 0;
                        subtotal = 0;

                        pivote = dtCTA.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString();

                        total = total + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
                        iva = iva + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
                        subtotal = subtotal + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());


                        if (i == dtCTA.Rows.Count - 1)
                        {

                            DataRow rw3 = dtcuenta.NewRow();
                            rw3["CTOTAL"] = "<strong> $" + total.ToString("#,##0.00") + "</strong>";
                            rw3["CIMPUESTO1"] = "<strong>" + iva.ToString("#,##0.00") + "</strong> $";
                            rw3["CNETO"] = "<strong> $" + subtotal.ToString("#,##0.00") + "</strong>";

                            rw3["CRAZONSOCIAL"] = "<strong>TOTAL</strong>";

                            dtcuenta.Rows.Add(rw3);
                        }

                    }
                }

                lvCliente.DataSource = dtcuenta;
                lvCliente.DataBind();

            }

        }

    }
}