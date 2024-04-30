using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroCobranza : System.Web.UI.Page
    {
        cUsuarios cVendedores = new cUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlVendedores.DataSource = cVendedores.obtenerVendedoresActivosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    ddlVendedores.DataValueField = "id";
                    ddlVendedores.DataTextField = "nombre";
                    ddlVendedores.DataBind();
                    ddlVendedores.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.open('reporteCobranza.aspx?idVendedor=" + ddlVendedores.SelectedValue + "&idSucursal=" + Request.Cookies["ksroc"]["idSucursal"] + "&fact=" + rbtnFacturada.SelectedValue + "' ,'_blank');</script>");
        }

        protected void btnEnviarExcel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.open('reporteCobranza.aspx?idVendedor=" + ddlVendedores.SelectedValue + "&idSucursal=" + Request.Cookies["ksroc"]["idSucursal"] + "&fact=" + rbtnFacturada.SelectedValue + "&Excel=1' ,'_blank');</script>");
        }
    }
}