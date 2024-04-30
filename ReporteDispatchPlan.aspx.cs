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
    public partial class ReporteDispatchPlan : System.Web.UI.Page
    {
        
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUDM cUM = new cUDM();
        cUTransporte cUTrans = new cUTransporte();
        cClientes cCl = new cClientes();
        cUsuarios cUsr = new cUsuarios();
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFecha.Text += Request.QueryString["Fecha"];

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

                    if (lvDispatch.Items.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    }
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }
        protected void llenarLV()
        {
            lvDispatch.DataSource = cOD.obtenerDispatchPlan(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["Fecha"]));
            lvDispatch.DataBind();
        }

        protected void llenarLVTotal()
        {
            lvTotal.DataSource = "1";
            lvTotal.DataBind();
        }

        protected string nombreUnidad(int idUnidad)
        {
            return cUTrans.ObtenerNombreUnidad(idUnidad);
        }
        protected string codigoProducto(int idProducto)
        {
            return cProd.obtenerCodigoProductoByID(idProducto);
        }
        protected string direccionCliente(int idCliente)
        {
            DataTable dt = cCl.obtenerDireccionClienteByID(idCliente);

            string direccionC = "";

            if (dt.Rows[0]["calle"].ToString() != "" && dt.Rows[0]["calle"].ToString() != "0" && dt.Rows[0]["calle"].ToString() != null)
            {
                direccionC = dt.Rows[0]["calle"].ToString() + " " + dt.Rows[0]["numero"].ToString() + "-" + dt.Rows[0]["interior"].ToString() + " col.  " + dt.Rows[0]["colonia"].ToString();
            }
            else
            {
                direccionC = dt.Rows[0]["calle"].ToString() + " " + dt.Rows[0]["numero"].ToString() + " " + " col. " + dt.Rows[0]["colonia"].ToString();
            }

            return direccionC;
        }
        public string comentarios(int idOrden)
        {
            cOrdenes cOrd = new cOrdenes();
            return cOrd.ObtenerUbicacionByIDOrden(idOrden);
        }
        protected string obtenerTotal()
        {

            DataTable dt = cOD.obtenerDispatchPlan(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["Fecha"]));

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
            if (lvDispatch.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionTicket" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvDispatch.Parent.Controls.Add(frm);
                        lvTotal.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvDispatch);
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