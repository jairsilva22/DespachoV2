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

namespace despacho
{
    public partial class ReporteProduccionMezcla : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
        cUDM cUM = new cUDM();
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];

                if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null)
                {
                    idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                }
                else
                {
                    idSucursal = 0;
                }

                cSuc.id = idSucursal;

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + cSuc.nombre;

                llenarLV();
                llenarLVTotales();
                lvTotalPorPlantas();
                lvTotalTodasPlantas();
                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvMezcla.Items.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    }
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }

        protected void llenarLV()
        {
            string param = "";

            if(Request.QueryString["Credito"] != "" && Request.QueryString["Credito"] != null)
            {
                param += "AND idFormaPago = " + Request.QueryString["Credito"] + " ";
            }
            if(Request.QueryString["idCategoria"] != "" && Request.QueryString["idCategoria"] != null)
            {
                param += "AND idCategoria = " + Request.QueryString["idCategoria"] + " ";
            }

            lvMezcla.DataSource = cProd.obtenerProduccionMezcla(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvMezcla.DataBind();
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString().Trim();
            tipo = tipo.Replace(" ", "");
            return tipo;
        }
        protected void llenarLVTotales()
        {
            string param = "";

            if (Request.QueryString["Credito"] != "" && Request.QueryString["Credito"] != null)
            {
                param += "AND idFormaPago = " + Request.QueryString["Credito"] + " ";
            }
            if (Request.QueryString["idCategoria"] != "" && Request.QueryString["idCategoria"] != null)
            {
                param += "AND idCategoria = " + Request.QueryString["idCategoria"] + " ";
            }

            lvTotalesMezcla.DataSource = cProd.obtenerProduccionMezclaAgrupaMezcla(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvTotalesMezcla.DataBind();
        }
        protected void lvTotalPorPlantas()
        {
            string param = "";

            if (Request.QueryString["Credito"] != "" && Request.QueryString["Credito"] != null)
            {
                param += "AND idFormaPago = " + Request.QueryString["Credito"] + " ";
            }
            if (Request.QueryString["idCategoria"] != "" && Request.QueryString["idCategoria"] != null)
            {
                param += "AND idCategoria = " + Request.QueryString["idCategoria"] + " ";
            }

            lvTotalPorPlanta.DataSource = cProd.obtenerProduccionMezclaAgrupaPlanta(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvTotalPorPlanta.DataBind();
        }
        protected void lvTotalTodasPlantas()
        {
            string param = "";

            if (Request.QueryString["Credito"] != "" && Request.QueryString["Credito"] != null)
            {
                param += "AND idFormaPago = " + Request.QueryString["Credito"] + " ";
            }
            if (Request.QueryString["idCategoria"] != "" && Request.QueryString["idCategoria"] != null)
            {
                param += "AND idCategoria = " + Request.QueryString["idCategoria"] + " ";
            }
            lvGranTotal.DataSource = cProd.obtenerProduccionMezclaAgrupaPlanta(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvGranTotal.DataBind();
        }
        protected string llenarGranTotal()
        {
            string param = "";

            if (Request.QueryString["Credito"] != "" && Request.QueryString["Credito"] != null)
            {
                param += "AND idFormaPago = " + Request.QueryString["Credito"] + " ";
            }
            if (Request.QueryString["idCategoria"] != "" && Request.QueryString["idCategoria"] != null)
            {
                param += "AND idCategoria = " + Request.QueryString["idCategoria"] + " ";
            }
            DataTable dt = cProd.obtenerProduccionMezclaAgrupaPlanta(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            string tipo = dt.Rows[0]["cantidad"].ToString();
            return txtGranTotal.Text = tipo;
        }

        protected void exportarExcel()
        {
            if (lvMezcla.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionMezcla" + DateTime.Today.ToShortDateString() + ".xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    //instanciamos un objeto  stringWriter
                    using (StringWriter sw = new StringWriter())
                    {
                        //instanciamos un objeto htmlTextWriter
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        HtmlForm frm = new HtmlForm();

                        sw.WriteLine();

                        lvMezcla.Parent.Controls.Add(frm);
                        lvTotalesMezcla.Parent.Controls.Add(frm);
                        lvTotalPorPlanta.Parent.Controls.Add(frm);
                        lvGranTotal.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvMezcla);
                        frm.Controls.Add(lvTotalesMezcla);
                        frm.Controls.Add(lvTotalPorPlanta);
                        frm.Controls.Add(lvGranTotal);
                        frm.RenderControl(hw);

                        Response.Write(sw.ToString());

                    }

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                finally
                {
                    Response.End();
                }
            }
        }
    }
}