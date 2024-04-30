using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class FoliosAdd : System.Web.UI.Page
    {
        cSucursales sucursales;
        Folio folio;

        protected void Page_Load(object sender, EventArgs e)
        {
            sucursales = new cSucursales();
            folio = new Folio();
            try
            {
                if (!IsPostBack)
                {
                    lblEmpresa.Text = sucursales.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Folios.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            folio.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
            folio.idEmpresa = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
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

            folio.insertar();
            Response.Redirect("Folios.aspx");
        }
    }
}