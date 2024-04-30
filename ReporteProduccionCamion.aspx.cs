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
    public partial class ReporteProduccionCamion : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
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
                if (lvCamion.Items.Count != 0)
                {
                    llenarLVTotalTotales();
                }
                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvCamion.Items.Count == 0)
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
            if(Request.QueryString["idCamion"] != "" && Request.QueryString["idCamion"] != null)
            {
                param += " AND ut.id = " + Request.QueryString["idCamion"] + " ";
            } 
            lvCamion.DataSource = cProd.obtenerProduccionCamion(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvCamion.DataBind();
        }
        protected void llenarLVTotales()
        {
            string param = "";
            if (Request.QueryString["idCamion"] != "" && Request.QueryString["idCamion"] != null)
            {
                param += " AND ut.id = " + Request.QueryString["idCamion"] + " ";
            }

            lvTotales.DataSource = cProd.obtenerTotalesProduccionCamion(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvTotales.DataBind();

            DataTable dt = cProd.obtenerTotalesProduccionCamion(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
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
        protected void llenarLVTotalTotales()
        {
            lvTotalTotales.DataSource = "1";
            lvTotalTotales.DataBind();

            string param = "";
            if (Request.QueryString["idCamion"] != "" && Request.QueryString["idCamion"] != null)
            {
                param += " AND ut.id = " + Request.QueryString["idCamion"] + " ";
            }

            DataTable dt = cProd.obtenerTotalesProduccionCamion(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
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
            if (Request.QueryString["idCamion"] != "" && Request.QueryString["idCamion"] != null)
            {
                param += " AND ut.id = " + Request.QueryString["idCamion"] + " ";
            }
            lvPromedioDia.DataSource = cProd.obtenerPromedioDiaProduccionCamion(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), param);
            lvPromedioDia.DataBind();
        }
        protected string nombreUnidad(int idUnidad)
        {
            return cUTrans.ObtenerNombreUnidad(idUnidad);
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
        protected string tipoProducto(int idTipo)
        {
            return cTipo.obtenerTipoProductoByIDTipo(idTipo);
        }
        protected string obtenerPromedio(int idUT, DateTime fecha, float cantidad)
        {

            float cantidadC = cantidad;
            txtViajes.Text = cUTrans.obtenerCantidaViajesByIDUnidad(idUT, fecha).ToString();

            float suma = 0;
            if (int.Parse(txtViajes.Text) > 0)
                {
                suma = cantidadC / int.Parse(txtViajes.Text);
            }
            
            txtPromedio.Text = suma.ToString("#,##0.00");

            return txtPromedio.Text;
        }
        protected string promedioCantidadDia(float cantidad, int idtrans)
        {
            DataTable dt = cProd.obtenerProduccionCamionByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);
            lvCamion.DataBind();

            float cantiDias = dt.Rows.Count;
            float promCant = cantidad/ cantiDias;

            return promCant.ToString("#,##0.00");
        }
        protected string promedioViajesDia(float viajes, int idtrans)
        {
            DataTable dt = cProd.obtenerProduccionCamionByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);
            lvCamion.DataBind();

            float cantiDias = dt.Rows.Count;
            float promViajes = viajes / cantiDias;

            return promViajes.ToString("#,##0.00");
        }
        protected string promedioViajesCantidadDia(float cantidad,float viajes, int idtrans)
        {
            DataTable dt = cProd.obtenerProduccionCamionByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
               DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]), idtrans);
            lvCamion.DataBind();

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
            float totalProm = 0;
            if ((txtTotalCantidad.Text != null && txtTotalViajes.Text != null) && (txtTotalCantidad.Text != "" && txtTotalViajes.Text != ""))
            {
                float varibaleTotalCantidad = float.Parse(txtTotalCantidad.Text);
                float varibaleTotalViajes = float.Parse(txtTotalViajes.Text);
                if(varibaleTotalViajes > 0 && varibaleTotalCantidad > 0)
                {
                    //float totalProm = 0;
                    totalProm = varibaleTotalCantidad / varibaleTotalViajes;
                }
            }
            return totalProm.ToString("#,##0.00");
        }
        protected void exportarExcel()
        {
            if (lvCamion.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionCamion" + DateTime.Today.ToShortDateString() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    //instanciamos un objeto  stringWriter
                    using (StringWriter sw = new StringWriter())
                    {
                        //instanciamos un objeto htmlTextWriter
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        HtmlForm frm = new HtmlForm();

                        sw.WriteLine();

                        lvCamion.Parent.Controls.Add(frm);
                        lvTotales.Parent.Controls.Add(frm);
                        lvPromedioDia.Parent.Controls.Add(frm);
                        lvTotalTotales.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvCamion);
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