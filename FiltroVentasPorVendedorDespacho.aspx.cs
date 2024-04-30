using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class FiltroVentasPorVendedorDespacho : System.Web.UI.Page {
        cClientes cCliente = new cClientes();
        cContpaq cContpaq = new cContpaq();
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (Request.Cookies.Count > 0) {
                    cCliente.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                }
                else {
                    Response.Redirect(ResolveUrl("login.aspx"));
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e) {
            if (txtFechaInicio.Text.Equals("")) {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else if (txtFechaFin.Text.Equals("")) {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            
            Response.Write("<script>window.open('ReporteVentasPorVendedorDespacho.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" +
                txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "' ,'_blank');</script>");

        }

        protected void btnExcel_Click(object sender, EventArgs e) {
            if (txtFechaInicio.Text.Equals("")) {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else if (txtFechaFin.Text.Equals("")) {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            Response.Write("<script>window.open('ReportesVentasPorVendedor.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" +
                txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&CIDCLIENTEPROVEEDOR=" + "&Excel=1" + "' ,'_blank');</script>");

        }

    }
}