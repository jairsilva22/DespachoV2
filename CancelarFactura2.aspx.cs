using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class CancelarFacturas : System.Web.UI.Page
    {
        //objetos
        Factura fac;
        cUtilidades cUtl = new cUtilidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                fac = new Factura();

                if (!IsPostBack)
                {
                    //buscamos los datos de la factura
                    fac.idfactura = long.Parse(Request.QueryString["id"]);
                    fac.datosFactura();

                    //validamos en que estatus está la factura
                    if (fac.estatus == "PCancelada" && (fac.codigoCan != "708" || fac.codigoCan != "N - 708: No se pudo conectar al SAT."))
                    {
                        Response.Redirect("HacerCancelacion2.aspx?id=" + Request.QueryString["id"]);
                    }

                    //mostramos los datos de la factura
                    lblFolio.Text = fac.folio.ToString();
                    lblCliente.Text = fac.vendedor;
                    lblRfc.Text = fac.ordenCompra;
                    string[] uuid = fac.uuid.Split('=');
                    lblUUID.Text = uuid[uuid.Length - 1];
                    lblFecha.Text = fac.fechaAlta.ToString();
                    lblTotal.Text = String.Format("{0:0,0.00}", fac.total);
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            btnAceptar.Visible = false;
            btnCerrar.Visible = false;
            //vamos a la siguiente pagina para hacer el cancelado
            Response.Redirect("HacerCancelacion2.aspx?id=" + Request.QueryString["id"]);
        }
    }
}