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
    public partial class ReporteProduccionCliente : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cProductos cProd = new cProductos();
        cUDM cUM = new cUDM();
        cUsuarios cUsr = new cUsuarios();
        cClientes cCl = new cClientes();
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        float tCantidad = 0;
        float tMonto = 0;

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

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    if (lvCliente.Items.Count == 0)
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
            dtRPT.Columns.Add("cliente");
            dtRPT.Columns.Add("vendedor");
            dtRPT.Columns.Add("cantidad");
            dtRPT.Columns.Add("idUDM");
            dtRPT.Columns.Add("total");

            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            int dias = (fechaFin - fechaInicio).Days + 1;

            DateTime rFecha = fechaInicio;
            int rPlanta = 0;
            string rCliente = "";
            string rVendedor = "";
            float _rCantidad = 0;
            int rUdm = 0;
            float _rPrecioF = 0;
            float _rSubtotal = 0;
            float _rIva = 0;
            float _rMonto = 0;
            string param = "";
            string orden = "";

            if (int.Parse(Request.QueryString["Ordenar"]) == 1)
            {
                orden += " ORDER BY o.fecha ";
            }
            else if (int.Parse(Request.QueryString["Ordenar"]) == 2)
            {
                orden += " ORDER BY s.idCliente ";
            }
            else if (int.Parse(Request.QueryString["Ordenar"]) == 3)
            {
                orden += " ORDER BY s.idVendedor ";
            }

            if (!Request.QueryString["idClienteInicio"].Equals("0"))
            {
                param += " AND s.idCliente = " + Request.QueryString["idClienteInicio"];
            }
            if (!Request.QueryString["idVendedorInicio"].Equals("0"))
            {
                param += " AND s.idVendedor = " + Request.QueryString["idVendedorInicio"];
            }



            for (int i = 0; i < dias; i++)
            {
                rFecha = fechaInicio.AddDays(i);

                DataTable dtPCD = cProd.obtenerProduccionClienteDetalleByDia(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), rFecha, orden, param);

                foreach (DataRow dr in dtPCD.Rows)
                {
                    _rCantidad = 0;
                    _rPrecioF = 0;
                    _rSubtotal = 0;
                    _rIva = 0;
                    _rMonto = 0;
                    DataRow rw = dtRPT.NewRow();
                    rw["fecha"] = rFecha.ToString();
                    rw["planta"] = Request.Cookies["ksroc"]["idSucursal"];
                    rCliente = dr["cliente"].ToString();
                    rVendedor = dr["vendedor"].ToString();
                    _rCantidad = float.Parse(dr["cantidadEntregada"].ToString());
                    tCantidad += _rCantidad;
                    rUdm = int.Parse(dr["idUDM"].ToString());
                    _rPrecioF = float.Parse(dr["precioF"].ToString());
                    _rSubtotal = _rCantidad * _rPrecioF;
                    _rIva = _rSubtotal * float.Parse(dr["iva"].ToString());
                    _rMonto = _rSubtotal + _rIva;
                    tMonto += _rMonto;
                    rw["cliente"] = rCliente;
                    rw["vendedor"] = rVendedor;
                    rw["cantidad"] = _rCantidad;
                    rw["idUDM"] = rUdm;
                    rw["total"] = _rMonto.ToString("#,##0.00");

                    dtRPT.Rows.Add(rw);
                }
            }


            lvCliente.DataSource = dtRPT;
            lvCliente.DataBind();

            //for (int i = 0; i < int.Parse(dtPCD.Rows.Count.ToString()); i++)
            //{
            //    string cantidad = dtPCD.Rows[i]["cantidad"].ToString();

            //    if (txtCantidadTotal.Text == "" || txtCantidadTotal.Text == null)
            //    {
            //        float suma = 0;
            //        suma = suma + float.Parse(cantidad);
            //        txtCantidadTotal.Text = suma.ToString("#,##0.00");
            //    }
            //    else
            //    {
            //        float suma = float.Parse(txtCantidadTotal.Text);
            //        suma = suma + float.Parse(cantidad);
            //        txtCantidadTotal.Text = suma.ToString("#,##0.00");
            //    }

            //    string total = dtPCD.Rows[i]["total"].ToString();

            //    if (txtMontoTotal.Text == "" || txtMontoTotal.Text == null)
            //    {
            //        float suma = 0;
            //        suma = suma + float.Parse(total);
            //        txtMontoTotal.Text = suma.ToString("#,##0.00");
            //    }
            //    else
            //    {
            //        float suma = float.Parse(txtMontoTotal.Text);
            //        suma = suma + float.Parse(total);
            //        txtMontoTotal.Text = suma.ToString("#,##0.00");
            //    }

            //    string prom = dtPCD.Rows[i]["prom2"].ToString();

            //    if (txtPromedioTotal.Text == "" || txtPromedioTotal.Text == null)
            //    {
            //        float suma = 0;
            //        suma = suma + float.Parse(prom);
            //        txtPromedioTotal.Text = suma.ToString("#,##0.00");
            //    }
            //    else
            //    {
            //        float suma = float.Parse(txtPromedioTotal.Text);
            //        suma = suma + float.Parse(prom);
            //        txtPromedioTotal.Text = suma.ToString("#,##0.00");
            //    }
            //}

            //string prom = dtPC.Rows[i]["prom2"].ToString();

            //if (txtPromedioTotal.Text == "" || txtPromedioTotal.Text == null)
            //{
            //    float suma = 0;
            //    suma = suma + float.Parse(prom);
            //    txtPromedioTotal.Text = suma.ToString("#,##0.00");
            //}
            //else
            //{
            //    float suma = float.Parse(txtPromedioTotal.Text);
            //    suma = suma + float.Parse(prom);
            //    txtPromedioTotal.Text = suma.ToString("#,##0.00");
            //}
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString();
            tipo = tipo.Replace(" ", "");
            return tipo;
        }
        protected string nombreCliente(int idCliente)
        {
            return cCl.obtenerNombreClienteByID(idCliente);
        }
        protected string nombreVendedor(int idVendedor)
        {
            return cUsr.obtenerNombreUsuario(idVendedor);
        }
        protected void llenarLVTotales()
        {
            lvTotales.DataSource = "1";
            lvTotales.DataBind();
        }
        protected string totalCantidad()
        {
            return tCantidad.ToString();
        }
        protected string totalMonto()
        {
            return tMonto.ToString("#,##0.00");
        }
        protected string totalPromedio()
        {
            return txtPromedioTotal.Text;
        }
        protected void exportarExcel()
        {
            if (lvCliente.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProduccionCliente" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvCliente.Parent.Controls.Add(frm);
                        lvTotales.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvCliente);
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