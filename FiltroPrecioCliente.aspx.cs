using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class FiltroPrecioCliente : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cClientes cCl = new cClientes();
        cProyectos cProy = new cProyectos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteInicio.DataSource = cCl.obtenerClientesActivos();
                ddlClienteInicio.DataValueField = "id";
                ddlClienteInicio.DataTextField = "nombre";
                ddlClienteInicio.DataBind();
                ddlClienteInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteFin.DataSource = cCl.obtenerClientesActivos();
                ddlClienteFin.DataValueField = "id";
                ddlClienteFin.DataTextField = "nombre";
                ddlClienteFin.DataBind();
                ddlClienteFin.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlProyectoInicio.DataSource = cProy.obtenerProyectosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProyectoInicio.DataValueField = "id";
                ddlProyectoInicio.DataTextField = "nombre";
                ddlProyectoInicio.DataBind();
                ddlProyectoInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlProyectoFin.DataSource = cProy.obtenerProyectosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProyectoFin.DataValueField = "id";
                ddlProyectoFin.DataTextField = "nombre";
                ddlProyectoFin.DataBind();
                ddlProyectoFin.Items.Insert(0, new ListItem("Seleccionar", ""));
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if ((ddlClienteInicio.SelectedValue.Equals("") && ddlClienteFin.SelectedValue != "") || (ddlClienteFin.SelectedValue.Equals("") && ddlClienteInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
                return;
            }
            if ((ddlProyectoInicio.SelectedValue.Equals("") && ddlProyectoFin.SelectedValue != "") || (ddlProyectoFin.SelectedValue.Equals("") && ddlProyectoInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un proyecto de inicio y fin";
                return;
            }
            else
            {
                Response.Write("<script>window.open('ReportePrecioCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&ClienteI=" + ddlClienteInicio.SelectedValue
                    + "&ClienteF=" + ddlClienteFin.SelectedValue + "&ProyectoInicio=" + ddlProyectoInicio.SelectedValue + "&ProyectoFin=" + ddlProyectoFin.SelectedValue + "' ,'_blank');</script>");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if ((ddlClienteInicio.SelectedValue.Equals("") && ddlClienteFin.SelectedValue != "") || (ddlClienteFin.SelectedValue.Equals("") && ddlClienteInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            if ((ddlProyectoInicio.SelectedValue.Equals("") && ddlProyectoFin.SelectedValue != "") || (ddlProyectoFin.SelectedValue.Equals("") && ddlProyectoInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un cliente de inicio y fin";
            }
            else
            {
                Response.Write("<script>window.open('ReportePrecioCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&ClienteI=" + ddlClienteInicio.SelectedValue
                    + "&ClienteF=" + ddlClienteFin.SelectedValue + "&ProyectoInicio=" + ddlProyectoInicio.SelectedValue + "&ProyectoFin=" + ddlProyectoFin.SelectedValue + "&Excel=1" + "' ,'_blank');</script>");
            }
        }
    }
}