using System;
using System.Data;

namespace despacho
{
    public partial class unidadesTransporteMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUTransporte cUT = new cUTransporte();
        cTipoUT cTut = new cTipoUT();
        cUDM cUdm = new cUDM();
        cEstadosUnidad cEU = new cEstadosUnidad();
        cTipoProducto cTP = new cTipoProducto();
        cSucursales cSuc = new cSucursales();
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
            DataTable dt = cUT.obtenerUTByID(int.Parse(Request.QueryString["id"]));
            txtNombre.Text = dt.Rows[0]["nombre"].ToString();
            txtMatricula.Text = dt.Rows[0]["matricula"].ToString();
            ddlTU.SelectedValue = dt.Rows[0]["idTipoUT"].ToString();
            txtCapacidad.Text = dt.Rows[0]["capacidad"].ToString();
            txtCapacidadMax.Text = dt.Rows[0]["capacidadMax"].ToString();
            ddlEstado.SelectedValue = dt.Rows[0]["idEstadoUnidad"].ToString();
            ddlUDM.SelectedValue = dt.Rows[0]["idUDM"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["combustible"].ToString()))
                ddlCombustible.SelectedValue = dt.Rows[0]["combustible"].ToString();
            txtColor.Text = dt.Rows[0]["color"].ToString();
            txtColor.Text = txtColor.Text.Substring(1, txtColor.Text.Length - 1);
        }
        protected void cargarControles()
        {
            llenarUDM();
            llenarEstadosUnidad();
            llenarTUT();
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
        protected void llenarEstadosUnidad()
        {
            ddlEstado.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("estado");

            dt.Rows.Add("0", "Selecciona el estado de la Unidad");

            DataTable dt1 = cEU.obtenerEstados();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["estado"].ToString());
            }

            ddlEstado.DataSource = dt;
            ddlEstado.DataValueField = "id";
            ddlEstado.DataTextField = "estado";
            ddlEstado.DataBind();
        }
        protected void llenarTUT()
        {
            ddlTU.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Unidad");

            cTut.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTut.obtenerUT();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString() + " - " + dr["capacidad"].ToString() + " - " + dr["unidad"].ToString());
            }

            ddlTU.DataSource = dt;
            ddlTU.DataValueField = "id";
            ddlTU.DataTextField = "tipo";
            ddlTU.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cUT.nombre = txtNombre.Text;
            cUT.matricula = txtMatricula.Text;
            if (txtCapacidad.Text.Equals(""))
            {
                lblError.Text = "Por favor de introducir la capacidad de carga de la unidad";
                return;
            }
            cUT.capacidad = float.Parse(txtCapacidad.Text);
            if (txtCapacidadMax.Text.Equals(""))
            {
                lblError.Text = "Por favor de introducir la capacidad máxima de carga de la unidad";
                return;
            }
            cUT.capacidadMax = float.Parse(txtCapacidadMax.Text);
            cUT.combustible = ddlCombustible.SelectedValue;
            cUT.color = "#" + txtColor.Text;
            if (ddlEstado.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona el Estado de la unidad";
                return;
            }
            cUT.idEstadoUnidad = int.Parse(ddlEstado.SelectedValue);
            if (ddlUDM.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona una unidad de medida";
                return;
            }
            cUT.idUDM = int.Parse(ddlUDM.SelectedValue);
            if (ddlTU.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona un tipo de Unidad";
                return;
            }
            cUT.idTipoUT = int.Parse(ddlTU.SelectedValue);
            cUT.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            cUT.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("unidadesTransporte.aspx");
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("unidadesTransporte.aspx");
        }
    }
}
