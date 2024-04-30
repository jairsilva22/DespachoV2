using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class productosFactorAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cProductosFactor cPF = new cProductosFactor();
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
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
            llenarTP(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
        }
        protected void llenarTP(int idS)
        {
            ddlTipoProducto.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Producto");
            cTP.idSucursal = idS;
            DataTable dt1 = cTP.obtenerByIdSucursal();
            if (dt1.Rows.Count.Equals(0))
                btnAddTP.Visible = true;
            else
                btnAddTP.Visible = false;
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
            }

            ddlTipoProducto.DataSource = dt;
            ddlTipoProducto.DataValueField = "id";
            ddlTipoProducto.DataTextField = "tipo";
            ddlTipoProducto.DataBind();
        }
        private void cleanInfo()
        {
            ddlTipoProducto.SelectedIndex = 0;
            txtFactor.Text = "";
            txtPorcentaje.Text = "";
            lblError.Text = "";
            txtFactor.Focus();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlTipoProducto.SelectedIndex.Equals(0))
            {
                lblError.Text = "Favor de seleccionar el tipo de producto";
                ddlTipoProducto.Focus();
            }
            else
            {
                cPF.idTipoProducto = int.Parse(ddlTipoProducto.SelectedValue);
                if (txtFactor.Text.Equals(""))
                {
                    lblError.Text = "Favor de ingresar el factor";
                    return;
                }
                cPF.factor = txtFactor.Text;
                if (txtPorcentaje.Text.Equals(""))
                {
                    lblError.Text = "Favor de ingresar el porcentaje";
                    return;
                }
                cPF.porcentaje = txtPorcentaje.Text;
                cPF.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (cPF.existeByTPAndFactorAndSucursal())
                {
                    cPF.actualizarEliminacion();
                    lblError.Text = "ya existia un factor con ese nombre se acaba de agregar nuevamente al sistema";
                    cleanInfo();
                }
                else
                {
                    cPF.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    Response.Redirect("productosFactor.aspx");
                }
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productosFactor.aspx");
        }
    }
}
