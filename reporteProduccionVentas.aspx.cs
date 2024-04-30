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
    public partial class reporteProduccionVentas : System.Web.UI.Page
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

                    if (lvVenta.Items.Count == 0)
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

            if(Request.QueryString["idClienteInicio"] != "" && Request.QueryString["idClienteInicio"] != null)
            {
                param += " AND s.idCliente BETWEEN " + Request.QueryString["idClienteInicio"] + " AND " + Request.QueryString["idClienteFin"] + " ";
            }
            if(Request.QueryString["idProyectoI"] != "" && Request.QueryString["idProyectoI"] != null)
            {
                param += " AND s.idProyecto BETWEEN " + Request.QueryString["idProyectoI"] + " AND " + Request.QueryString["idProyectoF"] + " ";
            }
            if (Request.QueryString["idProductoI"] != "" && Request.QueryString["idProductoI"] != null)
            {
                param += " AND od.idProducto BETWEEN " + Request.QueryString["idProductoI"] + " AND " + Request.QueryString["idProductoF"] + " ";
            }
            if (Request.QueryString["idRemision"] != "" && Request.QueryString["idRemision"] != null)
            {
                param += " AND od.id = " + Request.QueryString["idRemision"] +" ";
            }
            if (Request.QueryString["idCategoriaI"] != "" && Request.QueryString["idCategoriaI"] != null)
            {
                param += " AND p.idCategoria BETWEEN " + Request.QueryString["idCategoriaI"] + " AND " + Request.QueryString["idCategoriaF"] + " ";
            }
            if (Request.QueryString["idTipoProd"] != "" && Request.QueryString["idTipoProd"] != null)
            {
                param += " AND p.idTipoProducto = " + Request.QueryString["idTipoProd"] + " ";
            }

            lvVenta.DataSource = cOD.obtenerProduccionVenta(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
                DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvVenta.DataBind();
        }
        protected void llenarLVTotal()
        {
            lvTotal.DataSource = "1";
            lvTotal.DataBind();
        }

        protected string nombreCliente(int idCliente)
        {
            return cCl.obtenerNombreClienteByID(idCliente);
        }
        protected string codigoProducto(int idProducto)
        {
            return cProd.obtenerCodigoProductoByID(idProducto);
        }
        protected string descripcionProducto(int idProducto)
        {
            return cProd.obtenerDescripcionProductoByID(idProducto);
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString();
            return tipo.Trim();
        }
        protected string rowspan(int idCliente)
        {
            if (int.Parse(txtLastIDCliente.Text) == idCliente)
            {
                txtLastIDCliente.Text = idCliente.ToString();
                return "style=\" display:none\"";
            }
            else
            {
                txtLastIDCliente.Text = idCliente.ToString();
                return "style=\" text-align:center;\"";
            }
            
        }
        protected string obtenerTotal()
        {

            string param = "";
            if (Request.QueryString["idClienteInicio"] != "" && Request.QueryString["idClienteInicio"] != null)
            {
                param += " AND s.idCliente BETWEEN " + Request.QueryString["idClienteInicio"] + " AND " + Request.QueryString["idClienteFin"] + " ";
            }
            if (Request.QueryString["idProyectoI"] != "" && Request.QueryString["idProyectoI"] != null)
            {
                param += " AND s.idProyecto BETWEEN " + Request.QueryString["idProyectoI"] + " AND " + Request.QueryString["idProyectoF"] + " ";
            }
            if (Request.QueryString["idProductoI"] != "" && Request.QueryString["idProductoI"] != null)
            {
                param += " AND od.idProducto BETWEEN " + Request.QueryString["idProductoI"] + " AND " + Request.QueryString["idProductoI"] + " ";
            }
            if (Request.QueryString["idRemision"] != "" && Request.QueryString["idRemision"] != null)
            {
                param += " AND od.id BETWEEN " + Request.QueryString["idRemision"] + " AND " + Request.QueryString["idRemision"] + " ";
            }

            DataTable dt = cOD.obtenerProduccionVenta(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
                DateTime.Parse(Request.QueryString["FechaFin"]), param);

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
            if (lvVenta.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionVentas" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvVenta.Parent.Controls.Add(frm);
                        lvTotal.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvVenta);
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