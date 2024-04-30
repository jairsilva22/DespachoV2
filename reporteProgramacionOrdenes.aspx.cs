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
    public partial class reporteProgramacionOrdenes : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cOrdenes cOrd = new cOrdenes();
        cProductos cProd = new cProductos();
        cClientes cCl = new cClientes();
        cTipoUT cTUT = new cTipoUT();
        cUTransporte cUT = new cUTransporte();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
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
                //llenarLVTotales();

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvProgOrdenes.Items.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    }
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }
        protected void llenarLV()
        {
            lvProgOrdenes.DataSource = cOD.obtenerProgramacionOrdenes(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
                DateTime.Parse(Request.QueryString["FechaFin"]));
            lvProgOrdenes.DataBind();
        }
        protected string codigoNombre(int idCliente)
        {
            return cCl.obtenerClaveByID(idCliente) + " " + cCl.obtenerNombreClienteByID(idCliente);
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
        protected string codigoProducto(int idProducto)
        {
            return cProd.obtenerCodigoProductoByID(idProducto);
        }
        protected string descProducto(int idProducto)
        {
            return cProd.obtenerDescripcionProductoByID(idProducto);
        }
        protected string tipoUnidad(int idUnidadTransporte)
        {
            if(cUT.existeIDTUT(idUnidadTransporte))
            {
                int tipoUnidad = cUT.obtenerIDTipoUnidadByIDUnidad(idUnidadTransporte);
                if (tipoUnidad != 0)
                {
                    DataTable dt = cTUT.obtenerTiposUTByID(tipoUnidad);

                    string tipo = dt.Rows[0]["tipo"].ToString();

                    return tipo;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
            
        }
        protected string consultaCantidadEntrega(int idOrden, float cantidadEntregada, int idOD, int idDS)
        {
           float cantOrden =  cOrd.obtenerCantidadTotalByOrden(idOrden);
            float cantOD = cOD.obtenerCantidadByOD(idOD);
            float cantEntregada = cDS.getCantidadEntregada(idDS);

            if (cantOrden == cantidadEntregada)
            {
                return cantOD.ToString();
            }
            else
            {
                if(cantidadEntregada > cantOD)
                {
                    return cantOD.ToString();
                }
                else
                {
                    return cantEntregada.ToString();
                }
                
            }
        }
        protected string cantidadTotalOrden(int idOrden)
        {
            cOrdenes cOrd = new cOrdenes();
            float cantidad = cOrd.obtenerCantidadTotalByOrden(idOrden);

            return cantidad.ToString("#,##0.00");
        }
        protected string fechaOrden(int idOrden)
        {
            return cOrd.obtenerFechaOrdenByID(idOrden);
        }
        protected void exportarExcel()
        {
            if (lvProgOrdenes.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProgramacionOrdenes" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvProgOrdenes.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvProgOrdenes);
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