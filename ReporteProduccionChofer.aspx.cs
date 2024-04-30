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
    public partial class ReporteProduccionChofer2 : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUDM cUM = new cUDM();
        cUsuarios cUsr = new cUsuarios();
        cClientes cCl = new cClientes();
        cUTransporte cUTrans = new cUTransporte();
        cTipoProducto cTipo = new cTipoProducto();
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
                llenarLVPromedio();
                llenarLVTotalTotales();
                if (lvChofer.Items.Count != 0)
                {
                    llenarLVTotalTotales();
                }
                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvChofer.Items.Count == 0)
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
            if (Request.QueryString["idChofer"] != "" && Request.QueryString["idChofer"] != null)
            {
                param += " AND utc.idChofer = " + Request.QueryString["idChofer"] + " ";
            }
            lvChofer.DataSource = cOD.obtenerProduccionChofer(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvChofer.DataBind();
        }
        protected void llenarLVTotales()
        {
            string param = "";
            if (Request.QueryString["idChofer"] != "" && Request.QueryString["idChofer"] != null)
            {
                param += " AND utc.idChofer = " + Request.QueryString["idChofer"] + " ";
            }
            lvTotales.DataSource = cOD.obtenerTotalesProduccionChofer(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvTotales.DataBind();

            DataTable dt = cOD.obtenerTotalesProduccionChofer(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);

            for (int i = 0; i < int.Parse(dt.Rows.Count.ToString()); i++)
            {
                string cantidad = dt.Rows[i]["cantidad"].ToString();

                if (txtTotalCantidad.Text == "" || txtTotalCantidad.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(cantidad);
                    txtTotalCantidad.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtTotalCantidad.Text);
                    suma = suma + float.Parse(cantidad);
                    txtTotalCantidad.Text = suma.ToString("#,##0.00");
                }

                string viajes = dt.Rows[i]["viajes"].ToString();

                if (txtTotalViajes.Text == "" || txtTotalViajes.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(viajes);
                    txtTotalViajes.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtTotalViajes.Text);
                    suma = suma + float.Parse(viajes);
                    txtTotalViajes.Text = suma.ToString("#,##0.00");
                }
            }
        }
        protected void llenarLVPromedio()
        {
            string param = "";
            if (Request.QueryString["idChofer"] != "" && Request.QueryString["idChofer"] != null)
            {
                param += " AND utc.idChofer = " + Request.QueryString["idChofer"] + " ";
            }
            lvPromedioDia.DataSource = cOD.obtenerPromedioDiaProduccionChofer(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvPromedioDia.DataBind();
        }
        protected void llenarLVTotalTotales()
        {
            lvTotalTotales.DataSource = "1";
            lvTotalTotales.DataBind();

            string param = "";
            if (Request.QueryString["idChofer"] != "" && Request.QueryString["idChofer"] != null)
            {
                param += " AND utc.idChofer = " + Request.QueryString["idChofer"] + " ";
            }

            DataTable dt = cOD.obtenerTotalesProduccionChofer(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);


        }

        protected string nombreChofer(int idChofer)
        {
            return cUsr.obtenerNombreUsuario(idChofer).ToString().Trim();
        }
        protected string tipoProducto(int idTipo)
        {
            return cTipo.obtenerTipoProductoByIDTipo(idTipo);
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString();
            return tipo.Trim();
        }
        protected int cantidadViajes(int idUT, DateTime fecha)
        {
            return cUTrans.obtenerCantidaViajesByIDUnidad(idUT, fecha);
        }
        protected string obtenerPromedio(int idUT, DateTime fecha, float cantidad)
        {

            float cantidadC = cantidad;
            txtViajes.Text = cUTrans.obtenerCantidaViajesByIDUnidad(idUT, fecha).ToString();

            float suma = 0;
            suma = cantidadC / int.Parse(txtViajes.Text);
            txtPromedio.Text = suma.ToString("#,##0.00");

            return txtPromedio.Text;
        }
        protected string promedioCantidadDia(float cantidad, int idtrans)
        {
            DataTable dt = cOD.obtenerProduccionChoferByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);

            float cantiDias = dt.Rows.Count;
            float promCant = cantidad/ cantiDias;

            return promCant.ToString("#,##0.00");
        }
        protected string promedioViajesDia(float viajes, int idtrans)
        {
            DataTable dt = cOD.obtenerProduccionChoferByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);

            float cantiDias = dt.Rows.Count;
            float promViajes = viajes / cantiDias;

            return promViajes.ToString("#,##0.00");
        }
        protected string promedioViajesCantidadDia(float cantidad,float viajes, int idtrans)
        {
            DataTable dt = cOD.obtenerProduccionChoferByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);

            float cantiDias = dt.Rows.Count;
            float promViajes = viajes / cantiDias;
            float promCant = cantidad / cantiDias;
            float total = promCant / promViajes;

            return total.ToString("#,##0.00");
        }
        protected string totalCantidades()
        {
            return txtTotalCantidad.Text;
        }
        protected string totalViajes()
        {
            return txtTotalViajes.Text;
        }
        protected string totalPromedio()
        {
            float totalProm = float.Parse(txtTotalCantidad.Text) / float.Parse(txtTotalViajes.Text);
            return totalProm.ToString("#,##0.00");
        }
        protected string formatoPromedio(float cantidad, float viajes)
        {
            float promedio = cantidad / viajes;
            return promedio.ToString("#,##0.00");
        }
        protected void exportarExcel()
        {
            if (lvChofer.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionChofer" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvChofer.Parent.Controls.Add(frm);
                        lvTotales.Parent.Controls.Add(frm);
                        lvPromedioDia.Parent.Controls.Add(frm);
                        lvTotalTotales.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvChofer);
                        frm.Controls.Add(lvTotales);
                        frm.Controls.Add(lvPromedioDia);
                        frm.Controls.Add(lvTotalTotales);
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