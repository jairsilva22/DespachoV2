using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public partial class tiposUnidadesTransporteMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cTipoUT cTut = new cTipoUT();
        cUDM cUdm = new cUDM();
        cTipoProducto cTP = new cTipoProducto();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                    cargarDatos();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarDatos()
        {
            DataTable dt = cTut.obtenerTiposUTByID(int.Parse(Request.QueryString["id"]));
            txtTipo.Text = dt.Rows[0]["tipo"].ToString();
            if (dt.Rows[0]["carga"].ToString().Equals("True"))
                chbxCarga.Checked = true;
            else
                chbxCarga.Checked = false;
            txtCapacidad.Text = dt.Rows[0]["capacidad"].ToString();
            ddlUDM.SelectedValue = dt.Rows[0]["idUDM"].ToString();
            ddlTP.SelectedValue = dt.Rows[0]["idTipoProducto"].ToString();
            txtPeso.Text = dt.Rows[0]["peso"].ToString();
        }
        protected void cargarControles()
        {
            llenarUDM();
            llenarTP();
        }
        protected void llenarUDM()
        {
            ddlUDM.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("unidad");

            dt.Rows.Add("0", "Selecciona una Unidad");

            DataTable dt1 = cUdm.obtenerUDMActivas();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["unidad"].ToString());
            }

            ddlUDM.DataSource = dt;
            ddlUDM.DataValueField = "id";
            ddlUDM.DataTextField = "unidad";
            ddlUDM.DataBind();
        }
        protected void llenarTP()
        {
            ddlTP.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Producto");

            cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTP.obtenerTiposProducto();
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
            cTut.tipo = txtTipo.Text;
            if (txtCapacidad.Text.Equals(""))
            {
                lblError.Text = "Por favor de ingresar la capacidad de la unidad";
                return;
            }
            cTut.capacidad = float.Parse(txtCapacidad.Text);
            if (ddlUDM.SelectedIndex.Equals(0))
            { 
                lblError.Text = "Por favor selecciona una unidad de medida";
                return;
            }
            cTut.idUDM = int.Parse(ddlUDM.SelectedValue);
            if (ddlTP.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona un tipo de producto";
                return;
            }
            cTut.idTipoProducto = int.Parse(ddlTP.SelectedValue);
            cTut.carga = chbxCarga.Checked;
            if (txtPeso.Text.Equals(""))
                txtPeso.Text = "0";
            cTut.peso = float.Parse(txtPeso.Text);
            cTut.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cTut.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("tiposUnidadesTransporte.aspx");
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tiposUnidadesTransporte.aspx");
        }

    }
}
