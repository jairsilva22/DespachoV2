using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroConsumo : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cClientes cCl = new cClientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSucursal.DataSource = cSuc.obtenerSucursales();
                ddlSucursal.DataValueField = "id";
                ddlSucursal.DataTextField = "nombre";
                ddlSucursal.DataBind();
                ddlSucursal.Items.Insert(0, new ListItem("Seleccionar la sucursal", ""));
                ddlSucursal.SelectedValue = Request.Cookies["ksroc"]["idSucursal"];
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }

            if (txtFechaFin.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            Response.Write("<script>window.open('ReporteConsumo.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "' ,'_blank');</script>");
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            if (txtFechaFin.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            } 
            Response.Write("<script>window.open('ReporteConsumo.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Excel=1" + "' ,'_blank');</script>");
        }
    }
}