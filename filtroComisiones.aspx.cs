using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroComisiones : System.Web.UI.Page
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
                    ddlVendedores.Items.Insert(0, new ListItem("Seleccionar", ""));
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (ddlVendedores.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un vendedor";
                return;
            }
            else if(txtFechaInicio.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else if (txtFechaFin.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            else
            {
                //Response.Redirect("ReporteComisiones.aspx?idVendedor=" + ddlVendedores.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text);
                Response.Write("<script>window.open('ReporteComisiones.aspx?idVendedor=" + ddlVendedores.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" +txtFechaFin.Text +"' ,'_blank');</script>");
            }
        }
    }
}