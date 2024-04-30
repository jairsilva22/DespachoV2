using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class productosFactorMod : System.Web.UI.Page
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
                    cargarDatos();
                }
            }
            catch (Exception)
            {

            }
        }
        public void cargarDatos()
        {
            DataTable dt = cPF.obtenerByID(int.Parse(Request.QueryString["id"]));
            hfIdS.Value = dt.Rows[0]["idSucursal"].ToString();
            hfIdTP.Value = dt.Rows[0]["idTipoProducto"].ToString();
            lblTP.Text = dt.Rows[0]["tipo"].ToString();
            txtFactor.Text = dt.Rows[0]["factor"].ToString();
            txtPorcentaje.Text = dt.Rows[0]["porcentaje"].ToString();
        }
        protected void cargarControles()
        {
        }
        protected void llenarTP(int idS)
        {
            //ddlTipoProducto.Items.Clear();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id");
            //dt.Columns.Add("tipo");

            //dt.Rows.Add("0", "Selecciona un Tipo de Producto");
            //cTP.idSucursal = idS;
            //DataTable dt1 = cTP.obtenerByIdSucursal();
            //if (dt1.Rows.Count.Equals(0))
            //    btnAddTP.Visible = true;
            //else
            //    btnAddTP.Visible = false;
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
            //}

            //ddlTipoProducto.DataSource = dt;
            //ddlTipoProducto.DataValueField = "id";
            //ddlTipoProducto.DataTextField = "tipo";
            //ddlTipoProducto.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cPF.id = int.Parse(Request.QueryString["id"]);
            cPF.idTipoProducto = int.Parse(hfIdTP.Value);
            cPF.idSucursal = int.Parse(hfIdS.Value);
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
            cPF.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("productosFactor.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productosFactor.aspx");
        }
    }
}
