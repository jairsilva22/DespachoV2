using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace despacho
{
    public partial class filtroProduccionChofer : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cFormasPago cFP = new cFormasPago();
        cCategorias cCat = new cCategorias();
        cUsuarios cUs = new cUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);


                cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlTipo.DataSource = cTP.obtenerByIdSucursal();
                ddlTipo.DataValueField = "id";
                ddlTipo.DataTextField = "tipo";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlChofer.DataSource = cUs.obtenerChoferesActivosBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlChofer.DataValueField = "id";
                ddlChofer.DataTextField = "nombre";
                ddlChofer.DataBind();
                ddlChofer.Items.Insert(0, new ListItem("Seleccionar", ""));
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
            else if (ddlTipo.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un tipo de producto";
                return;
            }
            else
            {
                Response.Write("<script>window.open('ReporteProduccionChofer.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue
                    + "&idChofer=" + ddlChofer.SelectedValue + "' ,'_blank');</script>");

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
            else if (ddlTipo.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un tipo de producto";
                return;
            }
            else
            {
                Response.Write("<script>window.open('ReporteProduccionChofer.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Excel=1"
                    + "&idChofer=" + ddlChofer.SelectedValue + "' ,'_blank');</script>");
            }
        }
    }
}