using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroProduccionVenta : System.Web.UI.Page
    {
        cClientes cCl = new cClientes();
        cProyectos cPro = new cProyectos();
        cProductos cProd = new cProductos();
        cCategorias cCat = new cCategorias();
        cTipoProducto cTP = new cTipoProducto();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cRemision cRem = new cRemision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

                ddlProyectoInicio.DataSource = cPro.obtenerProyectosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProyectoInicio.DataValueField = "id";
                ddlProyectoInicio.DataTextField = "nombre";
                ddlProyectoInicio.DataBind();
                ddlProyectoInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlProyectoFin.DataSource = cPro.obtenerProyectosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProyectoFin.DataValueField = "id";
                ddlProyectoFin.DataTextField = "nombre";
                ddlProyectoFin.DataBind();
                ddlProyectoFin.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlProductoInicio.DataSource = cProd.obtenerProductosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProductoInicio.DataValueField = "id";
                ddlProductoInicio.DataTextField = "codigo";
                ddlProductoInicio.DataBind();
                ddlProductoInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlProductoFin.DataSource = cProd.obtenerProductosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlProductoFin.DataValueField = "id";
                ddlProductoFin.DataTextField = "codigo";
                ddlProductoFin.DataBind();
                ddlProductoFin.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlCategoriaInicio.DataSource = cCat.obtenerCategoriasBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlCategoriaInicio.DataValueField = "id";
                ddlCategoriaInicio.DataTextField = "nombre";
                ddlCategoriaInicio.DataBind();
                ddlCategoriaInicio.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlCategoriaFin.DataSource = cCat.obtenerCategoriasBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlCategoriaFin.DataValueField = "id";
                ddlCategoriaFin.DataTextField = "nombre";
                ddlCategoriaFin.DataBind();
                ddlCategoriaFin.Items.Insert(0, new ListItem("Seleccionar", ""));

                cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                ddlTipo.DataSource= cTP.obtenerByIdSucursal();
                ddlTipo.DataValueField = "id";
                ddlTipo.DataTextField = "tipo";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("Seleccionar", ""));

                ddlRemision.DataSource = cRem.obtenerRemisiones(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                ddlRemision.DataValueField = "id";
                ddlRemision.DataTextField = "folio";
                ddlRemision.DataBind();
                ddlRemision.Items.Insert(0, new ListItem("Seleccionar", ""));
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
            else if ((ddlProyectoInicio.SelectedValue.Equals("") && ddlProyectoFin.SelectedValue != "") || (ddlProyectoFin.SelectedValue.Equals("") && ddlProyectoInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un proyecto de inicio y fin";
            }
            else if ((ddlProductoInicio.SelectedValue.Equals("") && ddlProductoFin.SelectedValue != "") || (ddlProductoFin.SelectedValue.Equals("") && ddlProductoInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar un producto de inicio y fin";
            }
            else if ((ddlCategoriaInicio.SelectedValue.Equals("") && ddlCategoriaFin.SelectedValue != "") || (ddlCategoriaFin.SelectedValue.Equals("") && ddlCategoriaInicio.SelectedValue != ""))
            {
                lblError.Text = "Debe seleccionar una categoría de inicio y fin";
            }
            //else if (ddlOrdenar.SelectedValue.Equals(""))
            //{
            //    lblError.Text = "Favor de seleccionar un orden";
            //}
            else
            {
                Response.Write("<script>window.open('ReporteProduccionVentas.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text 
                         + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "&idProyectoI=" + ddlProyectoInicio.SelectedValue + "&idProyectoF=" + ddlProyectoFin.SelectedValue
                        + "&idProductoI=" + ddlProductoInicio.SelectedValue + "&idProductoF=" + ddlProductoFin.SelectedValue + "&idCategoriaI=" + ddlCategoriaInicio.SelectedValue + "&idCategoriaF="+ ddlCategoriaFin.SelectedValue 
                        + "&idTipoProd=" + ddlTipo.SelectedValue + "&idRemision=" + ddlRemision.SelectedValue + "' ,'_blank');</script>");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
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
                else if ((ddlProyectoInicio.SelectedValue.Equals("") && ddlProyectoFin.SelectedValue != "") || (ddlProyectoFin.SelectedValue.Equals("") && ddlProyectoInicio.SelectedValue != ""))
                {
                    lblError.Text = "Debe seleccionar un proyecto de inicio y fin";
                }
                else if ((ddlProductoInicio.SelectedValue.Equals("") && ddlProductoFin.SelectedValue != "") || (ddlProductoFin.SelectedValue.Equals("") && ddlProductoInicio.SelectedValue != ""))
                {
                    lblError.Text = "Debe seleccionar un producto de inicio y fin";
                }
                else if ((ddlCategoriaInicio.SelectedValue.Equals("") && ddlCategoriaFin.SelectedValue != "") || (ddlCategoriaFin.SelectedValue.Equals("") && ddlCategoriaInicio.SelectedValue != ""))
                {
                    lblError.Text = "Debe seleccionar una categoría de inicio y fin";
                }
                //else if (ddlOrdenar.SelectedValue.Equals(""))
                //{
                //    lblError.Text = "Favor de seleccionar un orden";
                //}
                else
                {
                    Response.Write("<script>window.open('ReporteProduccionVentas.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text
                            + "&idClienteInicio=" + ddlClienteInicio.SelectedValue + "&idClienteFin=" + ddlClienteFin.SelectedValue + "&idProyectoI=" + ddlProyectoInicio.SelectedValue + "&idProyectoF=" + ddlProyectoFin.SelectedValue
                            + "&idProductoI=" + ddlProductoInicio.SelectedValue + "&idProductoF=" + ddlProductoFin.SelectedValue + "&idCategoriaI=" + ddlCategoriaInicio.SelectedValue + "&idCategoriaF=" + ddlCategoriaFin.SelectedValue
                            + "&idTipoProd=" + ddlTipo.SelectedValue + "&idRemision=" + ddlRemision.SelectedValue + "&Excel=1" + "' ,'_blank');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, you can log it or display a user-friendly message.
                lblError.Text = "An error occurred: " + ex.Message;
            }
        }

    }
}