using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroProduccionCliente : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cFormasPago cFP = new cFormasPago();
        cClientes cCl = new cClientes();
        cUsuarios cUs = new cUsuarios();
        cRemision cRem = new cRemision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlClienteInicio.DataSource = cCl.obtenerClientesActivos();
                ddlClienteInicio.DataValueField = "id";
                ddlClienteInicio.DataTextField = "nombre";
                ddlClienteInicio.DataBind();
                ddlClienteInicio.Items.Insert(0, new ListItem("Todos", "0"));
                ddlClienteInicio.SelectedValue = "0";

                ddlVendedorInicio.DataSource = cUs.obtenerVendedoresActivosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlVendedorInicio.DataValueField = "id";
                ddlVendedorInicio.DataTextField = "nombre";
                ddlVendedorInicio.DataBind();
                ddlVendedorInicio.Items.Insert(0, new ListItem("Todos", "0"));
                ddlVendedorInicio.SelectedValue = "0";


                ddlOrdenar.Items.Insert(0, new ListItem("Fecha", "1"));
                ddlOrdenar.Items.Insert(0, new ListItem("Cliente", "2"));
                ddlOrdenar.Items.Insert(0, new ListItem("Vendedor", "3"));
                ddlOrdenar.Items.Insert(0, new ListItem("Seleccionar", ""));
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
            else if (ddlOrdenar.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un orden";
            }
            Response.Write("<script>window.open('ReporteProduccionCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + 
                txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Ordenar=" + ddlOrdenar.SelectedValue + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + 
                "&idVendedorInicio=" + ddlVendedorInicio.SelectedValue + "' ,'_blank');</script>");
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
            else if (ddlOrdenar.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un orden";
            }
            Response.Write("<script>window.open('ReporteProduccionCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" +
                txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Ordenar=" + ddlOrdenar.SelectedValue + "&idClienteInicio=" + ddlClienteInicio.SelectedValue +
                "&idVendedorInicio=" + ddlVendedorInicio.SelectedValue + "&Excel=1" + "' ,'_blank');</script>");
        }
    }
}