using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroComparacionBacheos : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cClientes cCl = new cClientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTicketInicio.DataSource = cOD.obtenerTicketsBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlTicketInicio.DataValueField = "id";
                ddlTicketInicio.DataTextField = "id";
                ddlTicketInicio.DataBind();
                ddlTicketInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlTicketFin.DataSource = cOD.obtenerTicketsBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlTicketFin.DataValueField = "id";
                ddlTicketFin.DataTextField = "id";
                ddlTicketFin.DataBind();
                ddlTicketFin.Items.Insert(0, new ListItem("Seleccionar", ""));

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteInicio.DataSource = cCl.obtenerClientesBySuc();
                ddlClienteInicio.DataValueField = "id";
                ddlClienteInicio.DataTextField = "clave";
                ddlClienteInicio.DataBind();
                ddlClienteInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteFin.DataSource = cCl.obtenerClientesBySuc();
                ddlClienteFin.DataValueField = "id";
                ddlClienteFin.DataTextField = "clave";
                ddlClienteFin.DataBind();
                ddlClienteFin.Items.Insert(0, new ListItem("Seleccionar", ""));

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
            else if ((ddlClienteInicio.SelectedValue.Equals("") && ddlClienteFin.SelectedValue != "") || (ddlClienteFin.SelectedValue.Equals("") && ddlClienteInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else if ((ddlTicketInicio.SelectedValue.Equals("") && ddlTicketFin.SelectedValue != "") || (ddlTicketFin.SelectedValue.Equals("") && ddlTicketInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un ticket de inicio y fin";
            }
            else if (ddlClienteInicio.SelectedValue != "" && ddlClienteFin.SelectedValue != "")
            {
                if (ddlTicketInicio.SelectedValue != "" && ddlTicketFin.SelectedValue != "")
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&TicketInicio=" + ddlTicketInicio.SelectedValue 
                        + "&TicketFin=" + ddlTicketFin.SelectedValue + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");

                }
                else if ((ddlTicketInicio.SelectedValue.Equals("") && ddlTicketFin.SelectedValue != "") || (ddlTicketFin.SelectedValue.Equals("") && ddlTicketInicio.SelectedValue != ""))
                {
                    lblError.Text = "Debe seleccionar un vendedor de inicio y fin";
                }
                else
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                     + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");
                }
            }
            else if (ddlTicketInicio.SelectedValue != "" && ddlTicketFin.SelectedValue != "")
            {
                if (ddlClienteInicio.SelectedValue != "" && ddlClienteFin.SelectedValue != "")
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text 
                    + "&TicketInicio=" + ddlTicketInicio.SelectedValue + "&TicketFin=" + ddlTicketFin.SelectedValue + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin="
                    + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");

                }
                else
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                    + "&TicketInicio=" + ddlTicketInicio.SelectedValue + "&TicketFin=" + ddlTicketFin.SelectedValue + "' ,'_blank');</script>");
                }
            }
            else
            {
                Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "' ,'_blank');</script>");
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
            else if ((ddlClienteInicio.SelectedValue.Equals("") && ddlClienteFin.SelectedValue != "") || (ddlClienteFin.SelectedValue.Equals("") && ddlClienteInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else if ((ddlTicketInicio.SelectedValue.Equals("") && ddlTicketFin.SelectedValue != "") || (ddlTicketFin.SelectedValue.Equals("") && ddlTicketInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un ticket de inicio y fin";
            }
            else if (ddlClienteInicio.SelectedValue != "" && ddlClienteFin.SelectedValue != "")
            {
                if (ddlTicketInicio.SelectedValue != "" && ddlTicketFin.SelectedValue != "")
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&TicketInicio=" + ddlTicketInicio.SelectedValue
                        + "&Excel=1" + "&TicketFin=" + ddlTicketFin.SelectedValue + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");

                }
                else if ((ddlTicketInicio.SelectedValue.Equals("") && ddlTicketFin.SelectedValue != "") || (ddlTicketFin.SelectedValue.Equals("") && ddlTicketInicio.SelectedValue != ""))
                {
                    lblError.Text = "Debe seleccionar un vendedor de inicio y fin";
                }
                else
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                     + "&Excel=1" + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");
                }
            }
            else if (ddlTicketInicio.SelectedValue != "" && ddlTicketFin.SelectedValue != "")
            {
                if (ddlClienteInicio.SelectedValue != "" && ddlClienteFin.SelectedValue != "")
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                    + "&TicketInicio=" + ddlTicketInicio.SelectedValue + "&TicketFin=" + ddlTicketFin + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin="
                    + "&Excel=1" + ddlClienteFin.SelectedValue + "' ,'_blank');</script>");

                }
                else
                {
                    Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                     + "&Excel=1" + "&TicketInicio=" + ddlTicketInicio.SelectedValue + "&TicketFin=" + ddlTicketFin.SelectedValue + "' ,'_blank');</script>");
                }
            }
            else
            {
                Response.Write("<script>window.open('ReporteComparacionBacheos.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Excel=1" + "' ,'_blank');</script>");
            }
        }
    }
}