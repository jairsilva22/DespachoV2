using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class MetodoPago : System.Web.UI.Page
    {
        MetodoPagos metodo;
        cUtilidades cUtl = new cUtilidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            metodo = new MetodoPagos();
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    mostrarMetodosPago();
                }
            }
            catch (Exception)
            {

            }
        }

        private void mostrarMetodosPago()
        {
            lvMetodoPago.DataSource = metodo.metodos();
            lvMetodoPago.DataBind();
        }

        protected void btnMod_ItemCommand(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            string id = args[0];
            string clave = args[1];
            if(e.CommandName == "modificar")
            {
                Response.Redirect("MetodoPagoMod.aspx?id=" +id);
            }
            if(e.CommandName == "eliminar")
            {
                hfId.Value = id;
                mlblMessage.Text = "¿Estás seguro que deseas eliminar el método de Pago <strong> "+clave+" </strong> del sistema?";
                ScriptManager.RegisterStartupScript(Page, GetType(), "eliminar", "$('#myModal').modal('show');", true);
            }
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            metodo.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
            metodo.idPago = int.Parse(hfId.Value);
            metodo.eliminar();
            mostrarMetodosPago();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }
    }
}