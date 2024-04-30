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
    public partial class reporteProduccionProyecto : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUDM cUM = new cUDM();
        cClientes cCl = new cClientes();
        cProyectos cPro = new cProyectos();
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
                llenarLVTotal();

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvProyecto.Items.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    }
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }

        protected void llenarLV()
        {
            lvProyecto.DataSource = cOD.obtenerProduccionProyecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
               DateTime.Parse(Request.QueryString["FechaFin"]));
            lvProyecto.DataBind();
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString();
            return tipo.Trim();
        }
        protected string nombreCliente(int idCliente)
        { 
            string nombreCliente = cCl.obtenerNombreClienteByID(idCliente);
            return nombreCliente.Trim();
        }
        protected string nombreProyecto(int idProyecto)
        {
            return cPro.obtenerNombreProyectoByID(idProyecto);
        }
        protected void llenarLVTotal()
        {
            lvTotal.DataSource = "1";
            lvTotal.DataBind();
        }
        protected string obtenerTotal()
        {
            DataTable dt = cOD.obtenerProduccionProyecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
                DateTime.Parse(Request.QueryString["FechaFin"]));

            for (int i = 0; i < int.Parse(dt.Rows.Count.ToString()); i++)
            {
                string cantidad = dt.Rows[i]["cantidad"].ToString();

                if (txtTotal.Text == "" || txtTotal.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(cantidad);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtTotal.Text);
                    suma = suma + float.Parse(cantidad);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
            }
            return txtTotal.Text;
        }
        protected void exportarExcel()
        {
            if (lvProyecto.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionProyecto" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvProyecto.Parent.Controls.Add(frm);
                        lvTotal.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvProyecto);
                        frm.Controls.Add(lvTotal);
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