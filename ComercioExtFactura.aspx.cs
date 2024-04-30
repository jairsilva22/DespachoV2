using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ComercioExtFactura : System.Web.UI.Page
    {
        //variables de la pagina
        Factura fact;
        Cliente clnt;

        protected void Page_Load(object sender, EventArgs e)
        {
            fact = new Factura();
            clnt = new Cliente();

            //buscamos los datos de la factura si existen
            fact.idfactura = long.Parse(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                //buscamos los datos del cliente
                clnt.idCliente = long.Parse(Request.QueryString["idcliente"]);
                clnt.buscarCliente();

                //obtenemos el codigo del país
                //Paises pais = new Paises();
                //pais.id = clnt.paisCliente;
                //pais.obtenerPais();
                hdfPais.Value = "México";

                fact.camposComercioExterior();
                tipoOperacion.SelectedValue = fact.tipoOperacion.ToString();
                if (fact.claveDePedimento != "")
                {
                    clavePedimento.Text = fact.claveDePedimento;
                }
                certificadoOrigen.SelectedValue = fact.certificadoOrigen.ToString();
                if (fact.certificadoOrigen == "0")
                {
                    numCertificadoOrigen.Enabled = false;
                }
                numCertificadoOrigen.Text = fact.numCertificadoOrigen;
                numeroExportadorConfiable.Text = fact.numExportadorConfiable;
                incoterm.Text = fact.incoterm;
                if (fact.subdivision != "")
                {
                    subdivision.Text = fact.subdivision.ToString();
                }
                observaciones.Text = fact.observaciones;
                tipoCambio.Text = fact.tipoCambioUsd.ToString();
                totalUsd.Text = fact.totalUsd.ToString();
                hdfTotal.Value = fact.total.ToString();
                if (fact.abreviatura == "T")
                {
                    motivoTras.Text = "03->Envío de mercancías objeto de contrato de consignación";
                    hdfMotivo.Value = "03";
                }
                else
                {
                    motivoTras.Text = "99->Otros";
                    hdfMotivo.Value = "";
                }
                if (fact.numRegIdTrib != "")
                {
                    txtNumRegIdTrib.Text = fact.numRegIdTrib;
                }
                else
                {
                    txtNumRegIdTrib.Text = clnt.numRegIdTrib;
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //obtenemos los datos para llenar la factura
            fact.tipoOperacion = (tipoOperacion.SelectedValue);
            fact.claveDePedimento = clavePedimento.Text;
            fact.certificadoOrigen = (certificadoOrigen.SelectedValue);
            fact.numCertificadoOrigen = numCertificadoOrigen.Text;
            fact.numExportadorConfiable = numeroExportadorConfiable.Text;
            fact.incoterm = incoterm.Text;
            fact.subdivision = (subdivision.Text);
            fact.observaciones = observaciones.Text;
            fact.tipoCambioUsd = (tipoCambio.Text);
            fact.totalUsd = (totalUsd.Text);
            fact.numRegIdTrib = txtNumRegIdTrib.Text;
            fact.idfactura = long.Parse(Request.QueryString["id"]);
            if (hdfMotivo.Value == "03")
            {
                fact.motivoTraslado = "03";
            }
            else
            {
                fact.motivoTraslado = "";
            }
            //realizamos la actualizacion de los datos de la factura
            fact.actualizarComercioExterior();

            if (hdfMotivo.Value == "03")
            {
                //redireccionamos a la pagina para mostrar el proceso de timbrado
                Response.Redirect("destinoComExt.aspx?id=" + Request.QueryString["id"] + "&idempresa=" + Request.QueryString["idempresa"] + "&idcliente=" + Request.QueryString["idcliente"]);
            }
            else
            {
                //redireccionamos a la pagina para mostrar el proceso de timbrado
                Response.Redirect("detallesComExt.aspx?id=" + Request.QueryString["id"] + "&idempresa=" + Request.QueryString["idempresa"] + "&idcliente=" + Request.QueryString["idcliente"]);
            }
        }
    }
}