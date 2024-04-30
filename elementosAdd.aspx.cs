using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class elementosAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cElementos cEl = new cElementos();
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
            llenarTP();
        }
        protected void llenarTP()
        {
            ddlTP.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Producto");

            cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTP.obtenerByIdSucursal();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
            }

            ddlTP.DataSource = dt;
            ddlTP.DataValueField = "id";
            ddlTP.DataTextField = "tipo";
            ddlTP.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlTP.SelectedIndex.Equals(0) || ddlTP.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un Tipo de Producto";
                return;
            }
            if (txtElemento.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el Elemento";
                return;
            }
            cEl.elemento = txtElemento.Text;
            cEl.idTipoProducto = int.Parse(ddlTP.SelectedValue);
            cEl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            if (cEl.existe())
            {
                cEl.actualizarEliminacion();
                lblError.Text = "Ya existia un elemento con ese nombre se agrego nuevamente al sistema";
            }
            else
            {
                cEl.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("elementos.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("elementos.aspx");
        }
    }
}
