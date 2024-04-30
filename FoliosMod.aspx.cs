using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class FoliosMod : System.Web.UI.Page
    {
        Folio folio;
        cSucursales sucursales;

        protected void Page_Load(object sender, EventArgs e)
        {
            sucursales = new cSucursales();
            folio = new Folio();

            try
            {
                if (!IsPostBack)
                {
                    folio.idLog = int.Parse(Request.QueryString["id"]);
                    folio.obtenerFolio();

                    lblEmpresa.Text = sucursales.obtenerNombreSucursalByID(folio.idEmpresa);

                    txtFolioA.Text = folio.folioActivo.ToString();
                    txtFolioI.Text = folio.folioInicio.ToString();
                    txtFolioF.Text = folio.folioFinal.ToString();
                    txtSerie.Text = folio.serie;
                    txtNoAprobacion.Text = folio.naprobacion;
                    txtAnoAprobacion.Text = folio.anoaprobacion;

                    cbFactura.Checked = folio.factura;
                    cbNotaCargo.Checked = folio.notaCargo;
                    cbNotaCred.Checked = folio.notaCredito;
                    cbComPago.Checked = folio.cPago;
                    cbPagosF.Checked = folio.pagosF;
                    cbPagosV.Checked = folio.pagosV;
                    cbSolicitudes.Checked = folio.solicitudes;
                    cbRemisiones.Checked = folio.remisiones;
                    folio.ordenes = cbOrdenes.Checked;

                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            folio.idLog = int.Parse(Request.QueryString["id"]);
            folio.folioInicio = int.Parse(txtFolioI.Text);
            folio.folioFinal = int.Parse(txtFolioF.Text);
            folio.folioActivo = int.Parse(txtFolioA.Text);
            folio.serie = txtSerie.Text;
            folio.naprobacion = txtNoAprobacion.Text;
            folio.anoaprobacion = txtAnoAprobacion.Text;
            folio.factura = cbFactura.Checked;
            folio.notaCredito = cbNotaCred.Checked;
            folio.notaCargo = cbNotaCargo.Checked;
            folio.cPago = cbComPago.Checked;
            folio.pagosF = cbPagosF.Checked;
            folio.pagosV = cbPagosV.Checked;
            folio.solicitudes = cbSolicitudes.Checked;
            folio.remisiones = cbRemisiones.Checked;
            folio.ordenes = cbOrdenes.Checked;

            folio.modificar();
            Response.Redirect("Folios.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Folios.aspx");
        }
    }
}