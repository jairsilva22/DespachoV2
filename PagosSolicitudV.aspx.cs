using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class PagosSolicitudV : System.Web.UI.Page
    {
        cPagos cPag = new cPagos();
        cPagos cPagVendedor = new cPagos();
        cSolicitudes cSol = new cSolicitudes();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //pagos de vendedor 
                    lblVendedor.Text = "<strong>Vendedor: </strong>" + Request.QueryString["vendedor"];
                    cPag.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                    LvPagosVendedor.DataSource = cPag.obtenerPagosSolicitudVendedor();
                    LvPagosVendedor.DataBind();

                    //pagos de finanzas
                    lblFolio.Text = "Folio de la solicitud: " + cSol.obtenerFolioSolicitudByID(Request.QueryString["idSolicitud"]).ToString();
                    cPag.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                    lvPagos.DataSource = cPag.obtenerPagosSolicitud(); ;
                    lvPagos.DataBind();

                }
            }
            catch (Exception)
            {

            }
        }

        protected void lkCancelar_ItemCommand(object sender, CommandEventArgs e)
        {
            hdIdPago.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(Page, GetType(), "cancelarpago", "$('#myModal').modal('show');", true);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            cPag.id = int.Parse(hdIdPago.Value);
            cPag.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cPag.modificarEstatusV();

            Response.Redirect("PagosSolicitudV.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"]);
        }

        public string mostrarCancelar(string estatus)
        {
            string param = "";

            if (estatus == "Cancelado")
            {
                param = "style='display:none'";
            }

            return param;
        }

        public string convertir(string total)
        {
            string param = "";
            param = Convert.ToDecimal(total).ToString("#,##0.00");

            return param;
        }
    }
}