using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class cvendedores : System.Web.UI.Page
    {
        cSolicitudes cSol;
        cPerfiles cPerfil;

        protected void Page_Load(object sender, EventArgs e)
        {
            cSol = new cSolicitudes();
            cPerfil = new cPerfiles();

            try
            {
                if (!IsPostBack)
                {
                    mostrarSolicitudes();
                }
            }
            catch (Exception)
            {

            }
        }

        private void mostrarSolicitudes()
        {
            cPerfil.id = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cPerfil.obtenerPerfilByID();
            if (cPerfil.descripcion == "VENDEDOR" || cPerfil.descripcion == "Vendedor")
            {
                lvSolicitudes.DataSource = cSol.mostrarSolicitudesV(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), int.Parse(Request.Cookies["ksroc"]["id"]));
                lvSolicitudes.DataBind();
            }
            else
            {
                lvSolicitudes.DataSource = "";
                lvSolicitudes.DataBind();
            }
        }

        protected void btnPago_ItemCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("pagos"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split(';');
                    hfId.Value = arr[0];
                    Response.Redirect("PagosSolicitudV.aspx?idSolicitud=" + arr[0] + "&Vendedor=" + arr[1]);
                }
                if (e.CommandName.Equals("facturar"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfId.Value = arr[0];
                    Response.Redirect("facturarSolicitud.aspx?idSolicitud=" + arr[0] + "&idCliente=" + arr[1]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}