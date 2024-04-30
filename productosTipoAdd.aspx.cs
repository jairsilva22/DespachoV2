using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class productosTipoAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cTipoProducto cTipoP = new cTipoProducto();
        cSucursales cSuc = new cSucursales();
        cCategorias cCat = new cCategorias();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void cargarControles()
        {
            //llenarSucursales();
        }
        //protected void llenarSucursales()
        //{
        //    ddlSucursales.Items.Clear();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("nombre");

        //    dt.Rows.Add("0", "Selecciona una Sucursal");

        //    DataTable dt1 = cSuc.obtenerSucursales();
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
        //    }

        //    ddlSucursales.DataSource = dt;
        //    ddlSucursales.DataValueField = "id";
        //    ddlSucursales.DataTextField = "nombre";
        //    ddlSucursales.DataBind();
        //}

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            //if (ddlSucursales.SelectedIndex.Equals(0) || ddlSucursales.SelectedValue.Equals(""))
            //{
            //    lblError.Text = "Favor de seleccionar una Sucursal";
            //    return;
            //}
            //if (txtTipo.Text.Equals(""))
            //{
            //    lblError.Text = "Favor de ingresar el tipo de producto";
            //    return;
            //}
            cTipoP.tipo = txtTipo.Text;
            cTipoP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cTipoP.revenimiento = chbxRev.Checked;

            if (cTipoP.existe())
            {
                cTipoP.actualizarEliminacion();
                lblError.Text = "El producto ya existia se acaba de agregar nuevamente";
            }
            else
            {
                cTipoP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                cCat.idTipoProducto = cTipoP.obtenerIDByTPAndSucursal();
                cCat.idSucursal = cTipoP.idSucursal;
                cCat.nombre = cTipoP.tipo;
                cCat.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("productosTipo.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productosTipo.aspx");
        }
    }
}
