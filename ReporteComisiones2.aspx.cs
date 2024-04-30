using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ReporteComisiones : System.Web.UI.Page
    {
        cUsuarios cVend = new cUsuarios();
        cOrdenes cOrd = new cOrdenes();
        Cliente cCliente = new Cliente();
        cSucursales cSuc = new cSucursales();
        cSolicitudes cSol = new cSolicitudes();
        cPagos cPag = new cPagos();
        cFormasPago cFP = new cFormasPago();
        cFactura cFac = new cFactura();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblVend.Text += cVend.obtenerNombreUsuario(int.Parse(Request.QueryString["idVendedor"]));
                    lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                    lblFechaFin.Text += Request.QueryString["FechaFin"];

                    llenarLV();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void llenarLV()
        {
            lvComisiones.DataSource = cOrd.obtenerOrdenesComisiones(int.Parse(Request.Cookies["ksroc"]["idSucursal"]),int.Parse(Request.QueryString["idVendedor"]),
                DateTime.Parse(Request.QueryString["FechaInicio"]), DateTime.Parse(Request.QueryString["FechaFin"]));
            lvComisiones.DataBind();
        }

        public string tipo(int idCliente)
        {
           string tipoC = cCliente.obtenerTipoComisionByCliente(idCliente);
            
            if(tipoC == "True")
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
            if(monto == null || monto == "")
            {
                return "00.00";
            }
            else
            {
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
            if(cFac.existeFolioFacturaSolicitud(idSolicitud))
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

                if(diasFuera < diasPolitica)
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
    }
}