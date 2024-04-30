using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroProduccionTicket : System.Web.UI.Page
    {
        cClientes cCl = new cClientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlCliente.DataSource = cCl.obtenerClientesBySuc();
                ddlCliente.DataValueField = "id";
                ddlCliente.DataTextField = "nombre";
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("Seleccionar", ""));
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
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
                Response.Write("<script>window.open('ReporteProduccionTicket.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text 
                    + "&idCliente=" + ddlCliente.SelectedValue + "' ,'_blank');</script>");

            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
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
                Response.Write("<script>window.open('ReporteProduccionTicket.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Excel=1"
                    + "&idCLiente=" + ddlCliente.SelectedValue + "' ,'_blank');</script>");
            }
        }
    }
}