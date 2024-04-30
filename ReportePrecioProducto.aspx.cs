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
    public partial class ReportePrecioProducto : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
        cUDM cUM = new cUDM();
        cClientes cCl = new cClientes();
        cFormasPago cFP = new cFormasPago();
        cProyectos cProy = new cProyectos();
        cCategorias cCat = new cCategorias();
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ClienteI"] != null && Request.QueryString["ClienteI"] != "")
            {
                lblClienteInicio.Visible = true;
                lblClienteFin.Visible = true;

                lblClienteInicio.Text += nombreCliente(int.Parse(Request.QueryString["ClienteI"]));
                lblClienteFin.Text += nombreCliente(int.Parse(Request.QueryString["ClienteF"]));
            }
            else
            {
                lblClienteInicio.Visible = false;
                lblClienteFin.Visible =false;
            }
            if (Request.QueryString["ProyectoInicio"] != null && Request.QueryString["ProyectoInicio"] != "")
            {
                lblProyectoInicio.Visible = true;
                lblProyectoFin.Visible = true;

                lblProyectoInicio.Text += nombreProyecto(int.Parse(Request.QueryString["ProyectoInicio"]));
                lblProyectoFin.Text += nombreProyecto(int.Parse(Request.QueryString["ProyectoFin"]));
            }
            else
            {
                lblProyectoInicio.Visible = false;
                lblProyectoFin.Visible = false;
            }
            if (!IsPostBack)
            {
                

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

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvPrecio.Items.Count == 0)
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
            if (Request.QueryString["ClienteI"] != null && Request.QueryString["ClienteI"] != "")
            {
                param += " AND c.id BETWEEN " + Request.QueryString["ClienteI"] + " AND " + Request.QueryString["ClienteF"] + " ";
            }
            if (Request.QueryString["ProyectoInicio"] != null && Request.QueryString["ProyectoInicio"] != "")
            {
                param += " AND s.idProyecto BETWEEN " + Request.QueryString["ProyectoInicio"] + " AND " + Request.QueryString["ProyectoFin"] + " ";
            }

            lvPrecio.DataSource = cProd.obtenerPrecioProducto(int.Parse(Request.Cookies["ksroc"]["idSucursal"]) , param);
            lvPrecio.DataBind();
        }
        protected string nombreCliente(int idCliente)
        {
            string nombreCliente = cCl.obtenerNombreClienteByID(idCliente);
            return nombreCliente.Trim();
        }
        protected string nombreProyecto(int idProyecto)
        {
            string nombreProyecto = cProy.obtenerNombreProyectoByID(idProyecto);
            return nombreProyecto.Trim();
        }
        protected string categoriaProducto(int idProducto)
        {
            int idCategoria = cProd.obtenerCategoridProducto(idProducto);
            if(idCategoria == 0)
            {
                return "";
            }
            else
            {
                return cCat.obtenerNombreCategoriaByID(idCategoria);
            }
        }
        protected string codigoProducto(int idProducto)
        {
            return cProd.obtenerCodigoProductoByID(idProducto);
        }
        protected string tipoProducto(int idProducto)
        {
            cTipoProducto cTP = new cTipoProducto();
            int idTipoProducto = cProd.obtenerTipoByidProducto(idProducto);

            return cTP.obtenerTipoProductoByIDTipo(idTipoProducto);
        }
        protected string descProducto(int idProducto)
        {
            return cProd.obtenerDescripcionProductoByID(idProducto);
        }
        protected string precioProducto(int idProducto)
        {
            return cProd.obtenerPrecioByIDProducto(idProducto);
        }
        protected void exportarExcel()
        {
            if (lvPrecio.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReportePrecioProducto" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvPrecio.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvPrecio);
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