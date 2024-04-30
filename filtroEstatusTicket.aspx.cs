using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroEstatusTicket : System.Web.UI.Page
    {
        cClientes cCl = new cClientes();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteI.DataSource = cCl.obtenerClientesActivos();
                ddlClienteI.DataValueField = "id";
                ddlClienteI.DataTextField = "nombre";
                ddlClienteI.DataBind();
                ddlClienteI.Items.Insert(0, new ListItem("Seleccionar", ""));

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteF.DataSource = cCl.obtenerClientesActivos();
                ddlClienteF.DataValueField = "id";
                ddlClienteF.DataTextField = "nombre";
                ddlClienteF.DataBind();
                ddlClienteF.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlOrdenI.DataSource = cOD.obtenerOrdenesBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlOrdenI.DataValueField = "id";
                ddlOrdenI.DataTextField = "id";
                ddlOrdenI.DataBind();
                ddlOrdenI.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlOrdenF.DataSource = cOD.obtenerOrdenesBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlOrdenF.DataValueField = "id";
                ddlOrdenF.DataTextField = "id";
                ddlOrdenF.DataBind();
                ddlOrdenF.Items.Insert(0, new ListItem("Seleccionar", ""));

                //ddlOrdenado.Items.Insert(0, new ListItem("Seleccionar", ""));
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
            else if ((ddlClienteI.SelectedValue.Equals("") && ddlClienteF.SelectedValue != "") || (ddlClienteF.SelectedValue.Equals("") && ddlClienteI.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else if ((ddlOrdenI.SelectedValue.Equals("") && ddlOrdenF.SelectedValue != "") || (ddlOrdenF.SelectedValue.Equals("") && ddlOrdenI.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar una orden de inicio y fin";
            }
            else
            {
                Response.Write("<script>window.open('ReporteEstatusTicket.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                    + "&idClienteI=" + ddlClienteI.SelectedValue + "&idClienteF="+ ddlClienteF.SelectedValue + "&idOrdenI="+ddlOrdenI.SelectedValue + "&idOrdenF="+ ddlOrdenF.SelectedValue 
                   + "' ,'_blank');</script>");

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
            else if ((ddlClienteI.SelectedValue.Equals("") && ddlClienteF.SelectedValue != "") || (ddlClienteF.SelectedValue.Equals("") && ddlClienteI.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else if ((ddlOrdenI.SelectedValue.Equals("") && ddlOrdenF.SelectedValue != "") || (ddlOrdenF.SelectedValue.Equals("") && ddlOrdenI.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else
            {
                Response.Write("<script>window.open('ReporteEstatusTicket.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                    + "&idClienteI=" + ddlClienteI.SelectedValue + "&idClienteF=" + ddlClienteF.SelectedValue + "&idOrdenI=" + ddlOrdenI.SelectedValue + "&idOrdenF=" + ddlOrdenF.SelectedValue
                   + "&Excel=1" + "' ,'_blank');</script>");

            }
        }
    }
}