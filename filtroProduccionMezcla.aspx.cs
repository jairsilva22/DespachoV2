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
    public partial class filtroProduccionMezcla : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cFormasPago cFP = new cFormasPago();
        cCategorias cCat = new cCategorias();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategoria.DataSource = cCat.obtenerCategoriasBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlCategoria.DataValueField = "id";
                ddlCategoria.DataTextField = "nombre";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccionar", ""));

                int idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);


                cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlTipo.DataSource = cTP.obtenerByIdSucursal();
                ddlTipo.DataValueField = "id";
                ddlTipo.DataTextField = "tipo";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("Seleccionar", ""));


                ddlCodigoCredito.Items.Clear();
                DataTable dt = cFP.obtenerFormasPago();

                ddlCodigoCredito.DataSource = dt;
                ddlCodigoCredito.DataValueField = "id";
                ddlCodigoCredito.DataTextField = "nombre";
                ddlCodigoCredito.DataBind();
                ddlCodigoCredito.Items.Insert(0, new ListItem("Seleccionar", ""));
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
                if (CuentaPedidosChk.Checked == true)
                {
                    Response.Write("<script>window.open('ReporteProduccionMezcla.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Credito=" + ddlCodigoCredito.SelectedValue + 
                        "&idCategoria=" + ddlCategoria.SelectedValue + "&Pedidos=true" + "' ,'_blank');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('ReporteProduccionMezcla.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Credito=" + ddlCodigoCredito.SelectedValue 
                        + "&idCategoria=" + ddlCategoria.SelectedValue +"' ,'_blank');</script>");
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
            else if (ddlTipo.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un tipo de producto";
                return;
            }
            else
            {
                if (CuentaPedidosChk.Checked == true)
                {
                    Response.Write("<script>window.open('ReporteProduccionMezcla.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Excel=1" + "&Credito=" + ddlCodigoCredito.SelectedValue + "&Pedidos=true" 
                        + "&idCategoria=" + ddlCategoria.SelectedValue + "' ,'_blank');</script>");
                }
                else
                {
                    Response.Write("<script>window.open('ReporteProduccionMezcla.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&idTipo=" + ddlTipo.SelectedValue + "&Excel=1" + "&Credito=" + ddlCodigoCredito.SelectedValue 
                        + "&idCategoria=" + ddlCategoria.SelectedValue + "' ,'_blank');</script>");
                }
                
            }
        }
    }
}