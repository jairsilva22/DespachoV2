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
    public partial class reporteComisiones : System.Web.UI.Page
    {
        cUsuarios cVend = new cUsuarios();
        cOrdenes cOrd = new cOrdenes();
        Cliente cCliente = new Cliente();
        cSucursales cSuc = new cSucursales();
        cSolicitudes cSol = new cSolicitudes();
        cPagos cPag = new cPagos();
        cFormasPago cFP = new cFormasPago();
        cFactura cFac = new cFactura();
        cClientes cCl = new cClientes();
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblVend.Text += cVend.obtenerNombreUsuario(int.Parse(Request.QueryString["idVendedor"]));
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
                    llenarTotales();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void llenarLV()
        {
            //lvComisiones.DataSource = cOrd.obtenerOrdenesComisiones(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idVendedor"]),
            //    DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]));
            lvComisiones.DataSource = "1";
            lvComisiones.DataBind();
        }
        protected string llenarTabla()
        {

            string tabla = "";
            DataTable dt = cOrd.obtenerOrdenesComisiones(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idVendedor"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]));
            string disply;
            int cont=0;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                //se valida si el id de la solicitud existe en la tabla de factura
                if (cFac.existeSolicitudFacturada(int.Parse(dt.Rows[i]["idSolicitud"].ToString())))
                {
                    //En caso de existir se obtiene le idCliente de la factura
                    int idCliente = cFac.obtenerIdClienteFacturaByIDSolicitud(int.Parse(dt.Rows[i]["idSolicitud"].ToString()));

                    //Con el idCliente vamos a la tabla de clientes facturacion y consultamos si cuenta con diasCredito
                    if (cCl.tieneDiasCreditoByID(idCliente) != "" && cCl.tieneDiasCreditoByID(idCliente) != null)
                    {
                        //si contiene diasCredito vamos a realizar las operaciones para conocer si genera comision según las politicas
                        DateTime fechaEntrega = Convert.ToDateTime(cOrd.obtenerFechasDeEntregaODByIDOrden(int.Parse(dt.Rows[i]["id"].ToString())));
                        int diasCredito = cCl.obtenerDiasCreditoByID(idCliente);
                        if (fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) == "---")
                        {

                            disply = " style=\"display:none;\"";
                            cont++;
                        }
                        else
                        {
                            DateTime fechaDePago = Convert.ToDateTime(fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())));

                            // Difference in days, hours, and minutes.
                            TimeSpan ts = fechaEntrega - fechaDePago;

                            // Difference in days.
                            int diasFuera = ts.Days;

                            if (diasFuera < diasCredito)
                            {
                                disply = "";
                            }
                            else
                            {
                                disply = " style=\"display:none;\"";
                                cont++;
                            }
                        }

                    }
                    //Si no contiene diasCredito se generan las operaciones correspondientes con la politica de 5 días  
                    else
                    {
                        DateTime fechaEntrega = Convert.ToDateTime(cOrd.obtenerFechasDeEntregaODByIDOrden(int.Parse(dt.Rows[i]["id"].ToString())));
                        int diasPolitica = 5;
                        if (fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) == "---")
                        {

                            disply = " style=\"display:none;\"";
                            cont++;
                        }
                        else
                        {
                            DateTime fechaDePago = Convert.ToDateTime(fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())));

                            // Difference in days, hours, and minutes.
                            TimeSpan ts = fechaEntrega - fechaDePago;

                            // Difference in days.
                            int diasFuera = ts.Days;

                            if (diasFuera < diasPolitica)
                            {
                                disply = "";
                            }
                            else
                            {
                                disply = " style=\"display:none;\"";
                                cont++;
                            }
                        }
                    }
                }
                else
                //Si no existe en la tabla de facturacion se usa la politica de 5 días
                {
                    DateTime fechaEntrega = Convert.ToDateTime(cOrd.obtenerFechasDeEntregaODByIDOrden(int.Parse(dt.Rows[i]["id"].ToString())));
                    int diasPolitica = 5;
                    if (fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) == "---")
                    {

                        disply = " style=\"display:none;\"";
                        cont++;
                    }
                    else
                    {
                        DateTime fechaDePago = Convert.ToDateTime(fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())));

                        // Difference in days, hours, and minutes.
                        TimeSpan ts = fechaDePago - fechaEntrega;

                        // Difference in days.
                        int diasFuera = ts.Days;

                        if (diasFuera < diasPolitica)
                        {
                            disply = "";
                        }
                        else
                        {
                            disply = " style=\"display:none;\"";
                            cont++;
                        }
                    }
                }
                tabla += "<tr " + disply  + ">";
                tabla += "<td style = \"text-align: center\">" + dt.Rows[i]["id"].ToString() + "</td>";
                tabla += "<td style = \"text-align: center\">" + dt.Rows[i]["fecha"].ToString().Substring(0, 10) + "</td>";
                tabla += "<td style = \"text-align: center\">" + dt.Rows[i]["nombre"].ToString() + "</td>";
                tabla += "<td style = \"text-align: center\">" + tipo(int.Parse(dt.Rows[i]["idCliente"].ToString())) + "</td>";
                if(disply != "")
                {
                    tabla += "<td style = \"text-align: center\">" + 00.00 + "</td>";
                }
                else
                {
                    tabla += "<td style = \"text-align: center\">" + monto(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) + "</td>";
                }
                tabla += "<td style = \"text-align: center\">" + fechaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) + "</td>";
                tabla += "<td style = \"text-align: center\">" + formaPago(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) + "</td>";
                tabla += "<td style = \"text-align: center\">" + factura(int.Parse(dt.Rows[i]["idSolicitud"].ToString())) + "</td>";
                if (disply != "")
                {
                    tabla += "<td style = \"text-align: center\">" + 00.00 + "</td>";
                }
                else
                {
                    tabla += "<td style = \"text-align: center\">" + comision(int.Parse(dt.Rows[i]["idCliente"].ToString()), int.Parse(dt.Rows[i]["idSolicitud"].ToString())) + "</td>";
                }
                tabla += "<td style = \"text-align: center\">" + fueraDeFecha(int.Parse(dt.Rows[i]["idSolicitud"].ToString()), int.Parse(dt.Rows[i]["id"].ToString())) + "</td>";
                tabla += "</tr>";
            }

            if(cont == dt.Rows.Count)
            {
                tabla += "<tr> <td colspan= \"10\" align=\"center\"> Sin Resultados</ td></ tr>";
            }

            return tabla;
        }
        public string tipo(int idCliente)
        {
            string tipoC = cCliente.obtenerTipoComisionByCliente(idCliente);

            if (tipoC == "True")
            {
                return "Directo <p></p>" + cSuc.obtenerComisionDirecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"])) + "%";
            }
            else
            {
                return "Indirecto<p></p>" + cSuc.obtenerComisionIndirecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"])) + "%";
            }
        }
        public string monto(int idSolicitud)
        {
            string monto = cSol.obtenerMontoSolicitud(idSolicitud);
            string subtotal = cSol.obtenerMontoSubtotalSolicitud(idSolicitud);
            if (monto == null || monto == "")
            {
                if(txtTotal.Text == "" || txtTotal.Text== null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(monto);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtTotal.Text);
                    suma = suma + float.Parse(monto);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
                if (txtSubtotal.Text == "" || txtSubtotal.Text == null)
                {
                    float sumaSub = 0;
                    sumaSub = sumaSub + float.Parse(subtotal);
                    txtSubtotal.Text = sumaSub.ToString("#,##0.00");
                }
                else
                {
                    float sumaSub = float.Parse(txtSubtotal.Text);
                    sumaSub = sumaSub + float.Parse(subtotal);
                    txtSubtotal.Text = sumaSub.ToString("#,##0.00");
                }
                return "00.00";
            }
            else
            {
                if (txtTotal.Text == "" || txtTotal.Text == null)
                {
                    float suma = 0;
                    suma = suma + float.Parse(monto);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
                else
                {
                    float suma = float.Parse(txtTotal.Text);
                    suma = suma + float.Parse(monto);
                    txtTotal.Text = suma.ToString("#,##0.00");
                }
                if (txtSubtotal.Text == "" || txtSubtotal.Text == null)
                {
                    float sumaSub = 0;
                    sumaSub = sumaSub + float.Parse(subtotal);
                    txtSubtotal.Text = sumaSub.ToString("#,##0.00");
                }
                else
                {
                    float sumaSub = float.Parse(txtSubtotal.Text);
                    sumaSub = sumaSub + float.Parse(subtotal);
                    txtSubtotal.Text = sumaSub.ToString("#,##0.00");
                }
                return Convert.ToDecimal(float.Parse(monto)).ToString("#,##0.00");
            }
        }

        public string fechaPago(int idSolicitud)
        {
            if (cPag.existeFechaPago(idSolicitud))
            {
                DateTime fecha = Convert.ToDateTime(cPag.obtenerFechaPagoBySolicitud(idSolicitud));
                string cadena = fecha.ToString("dd/MM/yyyy");
                return cadena;
            }
            else
            {
                return "---";
            }
        }
        public string formaPago(int idSolicitud)
        {
            return cFP.obtenerFormaPagoBySolicitud(idSolicitud);
        }
        public string factura(int idSolicitud)
        {
            if (cFac.existeFolioFacturaSolicitud(idSolicitud))
            {
                return cFac.obtenerFolioSerie(idSolicitud);
            }
            else
            {
                return "---";
            }
        }
        public string comision(int idCliente, int idSolicitud)
        {
            string comisionVendedor = "";
            string tipoC = cCliente.obtenerTipoComisionByCliente(idCliente);

            if (tipoC == "True")
            {
                float comisionD = float.Parse(cSuc.obtenerComisionDirecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"])));
                string monto = cSol.obtenerMontoSolicitud(idSolicitud);
                if (monto == null || monto == "")
                {
                    comisionVendedor = "00.00";
                    return comisionVendedor;
                }
                else
                {
                    string montoOrd = Convert.ToDecimal(float.Parse(monto)).ToString("#,##0.00");

                    comisionVendedor = (float.Parse(montoOrd) * comisionD).ToString();
                    comisionVendedor = Math.Floor(float.Parse(comisionVendedor)).ToString();

                    if (sumaCom.Text == "" || sumaCom.Text == null)
                    {
                        float suma = 0;
                        suma = suma + float.Parse(comisionVendedor);
                        sumaCom.Text = suma.ToString("#,##0.00");
                    }
                    else
                    {
                        float suma = float.Parse(sumaCom.Text);
                        suma = suma + float.Parse(comisionVendedor);
                        sumaCom.Text = suma.ToString("#,##0.00");
                    }

                    return Convert.ToDecimal(float.Parse(comisionVendedor)).ToString("#,##0.00");
                }
            }
            else
            {
                float comisionI = float.Parse(cSuc.obtenerComisionIndirecto(int.Parse(Request.Cookies["ksroc"]["idSucursal"])));
                string monto = cSol.obtenerMontoSolicitud(idSolicitud);
                if (monto == null || monto == "")
                {
                    comisionVendedor = "00.00";
                    return comisionVendedor;
                }
                else
                {
                    string montoOrd = Convert.ToDecimal(float.Parse(monto)).ToString("#,##0.00");

                    comisionVendedor = (float.Parse(montoOrd) * comisionI).ToString();
                    comisionVendedor = Math.Floor(float.Parse(comisionVendedor)).ToString();

                    if(sumaCom.Text == "" || sumaCom.Text == null)
                    {
                        float suma = 0;
                        suma = suma + float.Parse(comisionVendedor);
                        sumaCom.Text = suma.ToString("#,##0.00");
                    }
                    else 
                    {
                        float suma = float.Parse(sumaCom.Text);
                        suma = suma + float.Parse(comisionVendedor);
                        sumaCom.Text = suma.ToString("#,##0.00");
                    }
                    return Convert.ToDecimal(float.Parse(comisionVendedor)).ToString("#,##0.00");
                }
            }
        }
        public string fueraDeFecha(int idSolicitud, int idOrden)
        {
            int diasPolitica = 5;
            if (cPag.existeFechaPago(idSolicitud))
            {
                DateTime fechaPago = Convert.ToDateTime(cPag.obtenerFechaPagoBySolicitud(idSolicitud));

                DateTime fechaVenta = Convert.ToDateTime(cOrd.obtenerFechaOrdenByID(idOrden));

                // Difference in days, hours, and minutes.
                TimeSpan ts = fechaPago - fechaVenta;

                // Difference in days.
                int diasFuera = ts.Days;

                if (diasFuera < diasPolitica)
                {
                    return "0";
                }
                else
                {
                    return diasFuera.ToString();
                }
            }
            else
            {
                DateTime fechaPago = DateTime.Now;

                DateTime fechaVenta = Convert.ToDateTime(cOrd.obtenerFechaOrdenByID(idOrden));

                TimeSpan ts = fechaPago - fechaVenta;

                // Difference in days.
                int diasFuera = ts.Days;

                if (diasFuera < diasPolitica)
                {
                    return "0";
                }
                else
                {
                    return diasFuera.ToString();
                }
            }
        }

        protected void llenarTotales()
        {
            lvTotales.DataSource = cOrd.obtenerTotalesOrdenes(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.QueryString["idVendedor"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]));
            lvTotales.DataBind();
        }

        public string mostrarSumaComision()
        {
            if (sumaCom.Text == "")
            {
                sumaCom.Text = "00.00";
            }
            return sumaCom.Text;
        }
        public string obtenerTotal()
        {
            if(txtTotal.Text == "")
            {
                txtTotal.Text = "00.00";
            }
            return txtTotal.Text;
        }
        public string obtenerSubtotal()
        {
            if (txtSubtotal.Text == "")
            {
                txtSubtotal.Text = "00.00";
            }
            return txtSubtotal.Text;
        }
        public string obtenerIVA()
        {
            float totaliva;
            if (txtSubtotal.Text == "")
            {
                txtSubtotal.Text = "00.00";
                float subtotal = float.Parse(txtSubtotal.Text);
                totaliva = float.Parse((subtotal * .16).ToString());
            }
            else
            {
                float subtotal = float.Parse(txtSubtotal.Text);
                totaliva = float.Parse((subtotal * .16).ToString());
            }

            txtIva.Text = totaliva.ToString("#,##0.00");
            return txtSubtotal.Text;
        }

    }
}