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
    public partial class filtroProduccionCamion : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cUTransporte cUT = new cUTransporte();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSucursal.DataSource = cSuc.obtenerSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlSucursal.DataValueField = "id";
                ddlSucursal.DataTextField = "nombre";
                ddlSucursal.DataBind();
                ddlSucursal.Items.Insert(0, new ListItem("Seleccionar", ""));

                cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlTipo.DataSource = cTP.obtenerByIdSucursal();
                ddlTipo.DataValueField = "id";
                ddlTipo.DataTextField = "tipo";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("Seleccionar", ""));


                ddlCodigoCamion.Items.Clear();
                DataTable dt = cUT.obtenerUTBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

                ddlCodigoCamion.DataSource = dt;
                ddlCodigoCamion.DataValueField = "id";
                ddlCodigoCamion.DataTextField = "nombre";
                ddlCodigoCamion.DataBind();
                ddlCodigoCamion.Items.Insert(0, new ListItem("Seleccionar", ""));
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
            else if (ddlSucursal.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una sucursal";
                return;
            }
            else if (ddlTipo.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un tipo de producto";
                return;
            }
            else
            {
                if (TransNegativasChk.Checked == true)
                {
                    Response.Write("<script>window.open('ReporteProduccionCamion.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&idCamion=" + ddlCodigoCamion.SelectedValue + "&Pedidos=true" + "' ,'_blank');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('ReporteProduccionCamion.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&idCamion=" + ddlCodigoCamion.SelectedValue + "' ,'_blank');</script>");
                }
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
            else if (ddlSucursal.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una sucursal";
                return;
            }
            else if (ddlTipo.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un tipo de producto";
                return;
            }
            else
            {
                if (TransNegativasChk.Checked == true)
                {
                    Response.Write("<script>window.open('ReporteProduccionCamion.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Excel=1" + "&idCamion=" + ddlCodigoCamion.SelectedValue + "&Pedidos=true" + "' ,'_blank');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('ReporteProduccionCamion.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Excel=1" + "&idCamion=" + ddlCodigoCamion.SelectedValue + "' ,'_blank');</script>");
                }

            }
        }
    }
}