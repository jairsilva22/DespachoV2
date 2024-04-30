using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public partial class productosTipoMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cTipoProducto cTipoP = new cTipoProducto();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarTipoProducto();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarTipoProducto()
        {
            DataTable dt = cTipoP.obtenerTiposProductoByID(int.Parse(Request.QueryString["id"]));
            hfIdSucursal.Value = dt.Rows[0]["idSucursal"].ToString();
            //lblSucursal.Text = cSuc.obtenerNombreSucursalByID(int.Parse(hfIdSucursal.Value));
            txtTipo.Text = dt.Rows[0]["tipo"].ToString();
            
            if (dt.Rows[0]["revenimiento"].ToString().Equals("True"))
                chbxRev.Checked = true;
            else
                chbxRev.Checked =false;

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTipo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el tipo de producto";
                return;
            }
            cTipoP.tipo = txtTipo.Text;
            //cTipoP.idSucursal = int.Parse(hfIdSucursal.Value);
            cTipoP.revenimiento = chbxRev.Checked;

            cTipoP.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("productosTipo.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productosTipo.aspx");
        }
    }
}
