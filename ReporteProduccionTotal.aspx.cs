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
    public partial class ReporteProduccionTotal : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
        cUDM cUM = new cUDM();
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
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

                if(Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel(); 

                    if (lvProduccion.Items.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    }
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }

        protected void llenarLV()
        {
            DataTable dtRPT = new DataTable();

            dtRPT.Columns.Add("fecha");
            dtRPT.Columns.Add("planta");
            dtRPT.Columns.Add("tipo");
            dtRPT.Columns.Add("cantidad");
            dtRPT.Columns.Add("unidad");
            dtRPT.Columns.Add("idUDM");
            dtRPT.Columns.Add("subtotal");
            dtRPT.Columns.Add("iva");
            dtRPT.Columns.Add("total");

            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            int dias = (fechaFin - fechaInicio).Days + 1;
            
            DateTime rFecha = fechaInicio;
            int rPlanta = 0;
            string rTipo = "";
            float rCantidad = 0;
            float _rCantidad = 0;
            float _rPrecioF = 0;
            float rSubtotal = 0;
            float _rSubtotal = 0;
            float rIva = 0;
            float _rIva = 0;
            float rMontoTotal = 0;
            float _rMontoTotal = 0;
            int rUdm = 0;


            for (int i = 0; i < dias; i++)
            {
                rFecha = fechaInicio.AddDays(i);

                DataTable dt = cProd.obtenerProduccionTotalByDia(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]), rFecha);
                DataRow rw = dtRPT.NewRow();

                rw["fecha"] = rFecha.ToString();
                rw["planta"] = Request.Cookies["ksroc"]["idSucursal"];

                rCantidad = 0;
                rSubtotal = 0;
                rIva = 0;
                rMontoTotal = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    rTipo = dr["tipo"].ToString();
                    _rCantidad = float.Parse(dr["cantidadEntregada"].ToString());
                    rCantidad += float.Parse(dr["cantidadEntregada"].ToString());
                    _rPrecioF = float.Parse(dr["precioF"].ToString());
                    _rSubtotal = _rCantidad * _rPrecioF;
                    rSubtotal += _rSubtotal;
                    _rIva = _rSubtotal * float.Parse(dr["iva"].ToString());
                    rIva += _rIva;
                    _rMontoTotal = _rSubtotal + _rIva;
                    rMontoTotal += _rMontoTotal;
                    rUdm = int.Parse(dr["idUDM"].ToString());
                }

                rw["tipo"] = rTipo;
                rw["cantidad"] = rCantidad;
                rw["idUDM"] = rUdm;
                rw["subtotal"] = rSubtotal.ToString("#,##0.00");
                rw["iva"] = rIva.ToString("#,##0.00");
                rw["total"] = rMontoTotal.ToString("#,##0.00");

                dtRPT.Rows.Add(rw);
            }


            lvProduccion.DataSource = dtRPT;
            lvProduccion.DataBind();

            for (int i = 0; i < int.Parse(dtRPT.Rows.Count.ToString()); i++)
            {
                string cantidad = dtRPT.Rows[i]["cantidad"].ToString();

                if (txtPlantasCantidad.Text == "" || txtPlantasCantidad.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(cantidad);
                    txtPlantasCantidad.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtPlantasCantidad.Text);
                    suma = suma + float.Parse(cantidad);
                    txtPlantasCantidad.Text = suma.ToString("#,##0.00");
                }

                string subtotal = dtRPT.Rows[i]["subtotal"].ToString();

                if (txtPlantasSubtotal.Text == "" || txtPlantasSubtotal.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(subtotal);
                    txtPlantasSubtotal.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtPlantasSubtotal.Text);
                    suma = suma + float.Parse(subtotal);
                    txtPlantasSubtotal.Text = suma.ToString("#,##0.00");
                }

                string iva = dtRPT.Rows[i]["iva"].ToString();

                if (txtPlantasIva.Text == "" || txtPlantasIva.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(iva);
                    txtPlantasIva.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtPlantasIva.Text);
                    suma = suma + float.Parse(iva);
                    txtPlantasIva.Text = suma.ToString("#,##0.00");
                }

                string Total = dtRPT.Rows[i]["total"].ToString();

                if (txtPlantasTotal.Text == "" || txtPlantasTotal.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(Total);
                    txtPlantasTotal.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtPlantasTotal.Text);
                    suma = suma + float.Parse(Total);
                    txtPlantasTotal.Text = suma.ToString("#,##0.00");
                }
            }
        }
        //Optimized - Luis Moctezuma 11-05-2022
        protected string medidaProducto(int idUDM)
        {   
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo="-";
            if (dt.Rows.Count > 0)
            {
                //tipo = dt.Rows[0]["unidad"].ToString();
                tipo = dt.Rows[0]["unidad"].ToString().Trim();
                //tipo = tipo.Replace(" ", "");
            }
            
            return tipo;
        }
        protected void llenarLVTotales()
        {
            lvTotales.DataSource = cProd.obtenerTotalesProduccionTotal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idTipo"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]));
            lvTotales.DataBind();
        }
        protected string sumaCantidad()
        {
            return txtPlantasCantidad.Text;
        }
        protected string sumaSubtotal()
        {
            return txtPlantasSubtotal.Text;
        }
        protected string sumaIva()
        {
            return txtPlantasIva.Text;
        }
        protected string sumaTotal()
        {
            return txtPlantasTotal.Text;
        }
        protected string sumaGranTotal()
        {
            return txtGranTotal.Text = txtPlantasTotal.Text;
        }
        protected void exportarExcel()
        {
            if (lvProduccion.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionTotal" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvProduccion.Parent.Controls.Add(frm);
                        lvTotales.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvProduccion);
                        frm.Controls.Add(lvTotales);
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