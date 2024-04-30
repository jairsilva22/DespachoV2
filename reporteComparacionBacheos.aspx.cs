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
    public partial class reporteComparacionBacheos : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUDM cUM = new cUDM();
        cClientes cCl = new cClientes();
        cProyectos cPro = new cProyectos();
        cUTransporte cUTrans = new cUTransporte();
        cProductos cProd = new cProductos();
        cDosificacion cDos = new cDosificacion();
        cOrdenes cOrd = new cOrdenes();
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

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvBacheos.Items.Count == 0)
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
            if (Request.QueryString["idClienteInicio"] != "" && Request.QueryString["idClienteInicio"] != null)
            {
                param += " AND s.idCliente BETWEEN " + Request.QueryString["idClienteInicio"] + " AND " + Request.QueryString["idClienteFin"] + " ";
            }
            if (Request.QueryString["TicketInicio"] != "" && Request.QueryString["TicketInicio"] != null)
            {
                param += " AND od.id BETWEEN " + Request.QueryString["TicketInicio"] + " AND " + Request.QueryString["TicketFin"] + " ";
            }
            lvBacheos.DataSource = cOD.obtenerComparacionBacheos(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
               DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvBacheos.DataBind();
        }
        protected string nombreUnidad(int idUnidad)
        {
            return cUTrans.ObtenerNombreUnidad(idUnidad);
        }

        protected string nombreCliente(int idCliente)
        {
            string nombreCliente = cCl.obtenerNombreClienteByID(idCliente);
            return nombreCliente.Trim();
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
        protected string medidaProducto(string cantidad, int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = cantidad + " " + dt.Rows[0]["unidad"].ToString();

            return tipo.Trim();
        }
        protected string formulacionMaterial(int idProducto)
        {
            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string formulacion = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                formulacion += dt.Rows[i]["item"].ToString() + "<p></p>";
            }

            return formulacion;
        }
        protected string descMaterial(int idProducto)
        {
            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string formulacion = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                formulacion += dt.Rows[i]["descripcion"].ToString() + "<p></p>";
            }

            return formulacion;
        }
        protected string medidaMaterial(float cantidad, int idProducto)
        {

            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string formulacion = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float cant;
                cant = cantidad * float.Parse(dt.Rows[i]["cantidad"].ToString());
                cant.ToString("#,##0.00");
                formulacion += cant + " " + dt.Rows[i]["unidad"].ToString() + "<p></p>";
            }

            return formulacion;
        }
        protected string actualTicket(int idOD, int idProducto)
        {
            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string cantidad = "";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    string desc = dt.Rows[i]["descripcion"].ToString().Trim();
                    DataTable dt2 = cDos.obtenerActualBacheos(idOD, desc);
                    if(dt2.Rows.Count > 0)
                    {
                        for (int x = 0; x < dt2.Rows.Count; x++)
                        {
                            cantidad += dt2.Rows[x]["cant"].ToString() + " " + dt2.Rows[x]["unidad"].ToString() + "<p></p>";
                        }
                    }
                    else
                    {
                        cantidad += "0.00" + "<p></p>";
                    }
                }
                return cantidad;
            }
            else
            {
                return "";
            }
        }
        protected string obtenerVarianza(float cantidad, int idProducto, int idOD)
        {
            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string cantidadVarianza = "";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string desc = dt.Rows[i]["descripcion"].ToString().Trim();
                    float cant = cantidad * int.Parse(dt.Rows[i]["cantidad"].ToString());
                    DataTable dt2 = cDos.obtenerActualBacheos(idOD, desc);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int x = 0; x < dt2.Rows.Count; x++)
                        {
                            float restaVarianza = float.Parse(dt2.Rows[x]["cant"].ToString()) - cant;
                            cantidadVarianza += restaVarianza + "<p></p>";
                        }
                    }
                    else
                    {
                        cantidadVarianza += "0.00" + "<p></p>";
                    }
                }
                return cantidadVarianza;
            }
            else
            {
                return "";
            }
        }
        protected string porcentajeVarianza(float cantidad, int idProducto, int idOD)
        {
            DataTable dt = cProd.obtenerFormulacionByIdProductoReporte(idProducto);

            string cantidadVarianza = "";

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string desc = dt.Rows[i]["descripcion"].ToString().Trim();
                    float cant = cantidad * int.Parse(dt.Rows[i]["cantidad"].ToString());
                    DataTable dt2 = cDos.obtenerActualBacheos(idOD, desc);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int x = 0; x < dt2.Rows.Count; x++)
                        {
                            float restaVarianza = float.Parse(dt2.Rows[x]["cant"].ToString()) - cant;
                            float porVar = (restaVarianza / cant) * 100;
                            cantidadVarianza += porVar + "<p></p>";
                        }
                    }
                    else
                    {
                        cantidadVarianza += "0.00" + "<p></p>";
                    }
                }
                return cantidadVarianza;
            }
            else
            {
                return "";
            }
        }
        protected string fechaOrden(int idOrden)
        {
            return cOrd.obtenerFechaOrdenByID(idOrden);
        }
        protected void exportarExcel()
        {
            if (lvBacheos.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteComparacionBacheos" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvBacheos.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvBacheos);
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