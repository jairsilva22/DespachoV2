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
    public partial class reporteEstatusTicket : System.Web.UI.Page
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

                    if (lvTicket.Items.Count == 0)
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
            if (Request.QueryString["idClienteI"] != "" && Request.QueryString["idClienteI"] != null)
            {
                param += " AND s.idCliente BETWEEN " + Request.QueryString["idClienteI"] + " AND " + Request.QueryString["idClienteF"] + " ";
            }
            if (Request.QueryString["idOrdenI"] != "" && Request.QueryString["idOrdenI"] != null)
            {
                param += " AND o.id BETWEEN " + Request.QueryString["idOrdenI"] + " AND " + Request.QueryString["idOrdenF"] + " ";
            }

            lvTicket.DataSource = cOD.obtenerEstatusTicketC(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
                DateTime.Parse(Request.QueryString["FechaFin"]), param);

            lvTicket.DataBind();
        }

        protected void llenarLVTotal()
        {
            lvTotal.DataSource = "1";
            lvTotal.DataBind();
        }
        protected string medidaProducto(int idUDM)
        {
            DataTable dt = cUM.obtenerUnidadByID(idUDM);
            string tipo = dt.Rows[0]["unidad"].ToString();
            return tipo.Trim();
        }
        protected string nombreCliente(int idCliente)
        {

            return cCl.obtenerNombreClienteByID(idCliente);
        }
        protected string choferUnidad(int idUnidad)
        {
            cUTrans.obtenerIDChoferUnidadByIDUnidad(idUnidad);

            return cUsr.obtenerNombreUsuario(cUTrans.obtenerIDChoferUnidadByIDUnidad(idUnidad));

        }
        protected string codigoProducto(int idProducto)
        {
            return cProd.obtenerCodigoProductoByID(idProducto);
        }
        protected string obtenerTotal()
        {
            string param = "";
            if (Request.QueryString["idCliente"] != "" && Request.QueryString["idCliente"] != null)
            {
                param += " AND s.idCliente = " + Request.QueryString["idCliente"] + " ";
            }

            DataTable dt = cOD.obtenerProduccionTicket(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), DateTime.Parse(Request.QueryString["FechaInicio"]),
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
        protected string ordenesDosificacion(int idOrden)
        {
            string orden = "";
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for(int i = 0; i<dt.Rows.Count; i++)
            {
                orden += dt.Rows[i]["id"].ToString()+ "<p></p>";
            }
            return orden;
        }
        protected string nombreUnidad(int idOrden)
        {
            string orden = "";
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                orden += cUTrans.ObtenerNombreUnidad(int.Parse(dt.Rows[i]["idUnidadTransporte"].ToString())) + "<p></p>";
            }
            return orden;
        }
        protected string impresionOrden(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            float sumProm =0;
            string x = "";
            int cont = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    x = cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString()));
                    timeOnload = DateTime.Parse(cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())));
                    timePrint = DateTime.Parse(cOD.obtenerImpresion(int.Parse(dt.Rows[i]["id"].ToString())));
                    
                    var horas = (timeOnload - timePrint).ToString();
                    int longitudMinutos = horas.IndexOf(":");

                    horas = horas.Substring(longitudMinutos + 1);

                    longitudMinutos = horas.IndexOf(":");
                    horas = horas.Remove(longitudMinutos);

                    sumProm += float.Parse(horas);
                    cont++;
                    if(i+1 == dt.Rows.Count)
                    {
                        txtPromImpresion.Text = (sumProm / (cont)).ToString();
                    }

                    impresion += cOD.obtenerImpresion(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas +")" + "<p></p>";
                }
                else
                {
                    if (i + 1 == dt.Rows.Count)
                    {
                        txtPromImpresion.Text = (sumProm / (cont)).ToString();
                    }

                    impresion += cOD.obtenerImpresion(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                }
            }
            return impresion;
        }
        protected string onLoadEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            string x = "";
            int cont = 0;
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())));
                        timePrint = DateTime.Parse(cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())));

                        var horas = (timeOnload - timePrint).ToString();
                        int longitudMinutos = horas.IndexOf(":");

                        horas = horas.Substring(longitudMinutos + 1);

                        longitudMinutos = horas.IndexOf(":");
                        horas = horas.Remove(longitudMinutos);
                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromOnLoad.Text = (sumProm / (cont)).ToString();
                        }

                        impresion += cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromOnLoad.Text = (sumProm / (cont)).ToString();
                        }
                        impresion += cOD.obtenerAprobado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }
            }
            return impresion;
        }
        protected string toJobEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            int cont = 0;
            string x = "";
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())));
                        timePrint = DateTime.Parse(cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())));

                        var horas = (timeOnload - timePrint).ToString();
                        int longitudMinutos = horas.IndexOf(":");

                        horas = horas.Substring(longitudMinutos + 1);

                        longitudMinutos = horas.IndexOf(":");
                        horas = horas.Remove(longitudMinutos);

                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromToJob.Text = (sumProm / (cont)).ToString();
                        }

                        impresion += cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if (int.Parse(sumProm.ToString()) > 0)
                        {
                            cont++;
                            if (i + 1 == dt.Rows.Count)
                            {
                                txtPromToJob.Text = (sumProm / (cont)).ToString();
                            }
                        }
                        impresion += cOD.obtenerDosificar(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string onJobEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            int cont = 0;
            var horas ="";
            string x = "";
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10));
                        timePrint = DateTime.Parse(cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10));
                        if(timeOnload > timePrint)
                        {
                            horas = (timeOnload - timePrint).ToString();
                            int longitudMinutos = horas.IndexOf(":");

                            horas = horas.Substring(longitudMinutos + 1);

                            longitudMinutos = horas.IndexOf(":");
                            horas = horas.Remove(longitudMinutos);
                        }
                        else
                        {
                            horas = (timePrint - timeOnload).ToString();
                            int longitudMinutos = horas.IndexOf(":");

                            horas = horas.Substring(longitudMinutos + 1);

                            longitudMinutos = horas.IndexOf(":");
                            horas = horas.Remove(longitudMinutos);
                        }

                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromOnJob.Text = (sumProm / (cont)).ToString();
                        }


                        impresion += cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if(int.Parse(sumProm.ToString()) > 0)
                        {
                            cont++;
                            if (i + 1 == dt.Rows.Count)
                            {
                                txtPromOnJob.Text = (sumProm / (cont)).ToString();
                            }
                        }
                        
                        impresion += cOD.obtenerDosificando(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string pouringEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            string x = "";
            int cont = 0;
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())));
                        timePrint = DateTime.Parse(cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())));

                        var horas = (timeOnload - timePrint).ToString();
                        int longitudMinutos = horas.IndexOf(":");

                        horas = horas.Substring(longitudMinutos + 1);

                        longitudMinutos = horas.IndexOf(":");
                        horas = horas.Remove(longitudMinutos);

                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromPouring.Text = (sumProm / (cont)).ToString();
                        }

                        impresion += cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromOnJob.Text = (sumProm / (cont)).ToString();
                        }
                        impresion += cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string washEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            string x = "";
            int cont = 0;
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())));
                        timePrint = DateTime.Parse(cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())));

                        var horas = (timeOnload - timePrint).ToString();
                        int longitudMinutos = horas.IndexOf(":");

                        horas = horas.Substring(longitudMinutos + 1);

                        longitudMinutos = horas.IndexOf(":");
                        horas = horas.Remove(longitudMinutos);

                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromWash.Text = (sumProm / (cont)).ToString();
                        }

                        impresion += cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromWash.Text = (sumProm / (cont)).ToString();
                        }
                        impresion += cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string leaveJobEstatus(int idOrden)
        {
            string impresion = "";
            DateTime timeOnload;
            DateTime timePrint;
            string x = "";
            int cont = 0;
            float sumProm = 0;
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerConCliente(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerConCliente(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    if (cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                    {
                        x = cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString()));
                        timeOnload = DateTime.Parse(cOD.obtenerCamino(int.Parse(dt.Rows[i]["id"].ToString())));
                        timePrint = DateTime.Parse(cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())));

                        var horas = (timeOnload - timePrint).ToString();
                        int longitudMinutos = horas.IndexOf(":");

                        horas = horas.Substring(longitudMinutos + 1);

                        longitudMinutos = horas.IndexOf(":");
                        horas = horas.Remove(longitudMinutos);

                        sumProm += float.Parse(horas);
                        cont++;
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromLeave.Text = (sumProm / (cont)).ToString();
                        }

                        impresion += cOD.obtenerDosificado(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "(" + horas + ")" + "<p></p>";
                    }
                    else
                    {
                        if (i + 1 == dt.Rows.Count)
                        {
                            txtPromLeave.Text = (sumProm / (cont)).ToString();
                        }
                        impresion += cOD.obtenerConCliente(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                    }
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string arrivePlantEstatus(int idOrden)
        {
            string impresion = "";
            DataTable dt = cOD.obtenerODyUnidadByIDOrden(idOrden);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())) != "" && cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())) != null)
                {
                    impresion += cOD.obtenerRegreso(int.Parse(dt.Rows[i]["id"].ToString())).Remove(0, 10) + "<p></p>";
                }
                else
                {
                    impresion += "<p></p>";
                }

            }
            return impresion;
        }
        protected string promedioImpresion()
        {
            return txtPromImpresion.Text;
        }
        protected string promedioOnLoad()
        {
            return txtPromOnLoad.Text;
        }
        protected string promedioOnJob()
        {
            return txtPromOnJob.Text;
        }
        protected string promedioToJob()
        {
            return txtPromToJob.Text;
        }
        protected string promedioPouring()
        {
            return txtPromPouring.Text;
        }
        protected string promedioWash()
        {
            return txtPromWash.Text;
        }
        protected string promedioLeave()
        {
            return txtPromLeave.Text;
        }
        protected void exportarExcel()
        {
            if (lvTicket.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteEstatusTicket" + DateTime.Today.ToShortDateString() + ".xls");
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

                        lvTicket.Parent.Controls.Add(frm);
                        lvTotal.Parent.Controls.Add(frm);
                        frm.Controls.Add(lvTicket);
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